using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventResponse> EventResponses { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EmailHash).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code)
            .ValueGeneratedOnAdd();
            entity.HasOne(e => e.Owner)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.CreatedBy);
        });

        modelBuilder.Entity<EventResponse>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Event)
            .WithMany(e => e.Responses)
            .HasForeignKey(e => e.EventCode)
            .HasPrincipalKey(e => e.Code);
        });

        modelBuilder.Entity<UserEvent>()
            .HasKey(ue => new { ue.UserId, ue.EventId });

        modelBuilder.Entity<UserEvent>()
            .HasOne(ue => ue.User)
            .WithMany(u => u.UserEvents)
            .HasForeignKey(ue => ue.UserId);

        modelBuilder.Entity<UserEvent>()
            .HasOne(ue => ue.Event)
            .WithMany(e => e.UserEvents)
            .HasForeignKey(ue => ue.EventId);
    }
}
