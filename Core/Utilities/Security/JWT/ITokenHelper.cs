using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Core.Utilities.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IDataResult<List<OperationClaim>> operationClaims);
}