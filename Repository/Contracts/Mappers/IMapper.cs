using DataAccess.Entities;

namespace Repository.Contracts.Mappers;
public interface IMapper<TDomain, TDataAccess>
{
    TDataAccess GetDataAccess(TDomain user);
    TDomain GetDomain(TDataAccess user);
}
