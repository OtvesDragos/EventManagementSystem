using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("email")]
    public string EmailHash { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    public IList<Event> Events { get; set; }
}
