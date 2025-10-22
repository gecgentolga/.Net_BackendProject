using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<OperationClaim>> GetClaims(int id);
    IDataResult<User> GetByMail(string email);
    IResult Add(User user);
}