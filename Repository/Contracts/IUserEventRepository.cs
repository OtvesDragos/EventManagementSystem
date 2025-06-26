using Domain.Entities;

namespace Repository.Contracts;
public interface IUserEventRepository
{
    Task AddInvites(IList<Guid> userIds, Guid eventId, Guid ownerId);
}
