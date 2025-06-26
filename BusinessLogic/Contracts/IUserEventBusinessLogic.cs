namespace BusinessLogic.Contracts;
public interface IUserEventBusinessLogic
{
    Task AddInvites(IList<Guid> userIds, Guid eventId, Guid ownerId);
}
