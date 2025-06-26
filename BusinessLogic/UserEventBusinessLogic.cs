using BusinessLogic.Contracts;
using Repository.Contracts;

namespace BusinessLogic;
public class UserEventBusinessLogic : IUserEventBusinessLogic
{
    private readonly IUserEventRepository userEventRepository;

    public UserEventBusinessLogic(IUserEventRepository userEventRepository)
    {
        this.userEventRepository = userEventRepository;
    }

    public async Task AddInvites(IList<Guid> userIds, Guid eventId, Guid ownerId)
    {
        if (userIds == null)
        {
            throw new ArgumentNullException(nameof(userIds));
        }

        await userEventRepository.AddInvites(userIds, eventId, ownerId);
    }
}
