using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SecuredOperation:MethodInterception
{
    //JWT'den gelen claimleri alır
    
    private string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;

    public SecuredOperation(string roles)
    {
        _roles = roles.Split(',');
        _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

    }

    protected override void OnBefore(IInvocation invocation)
    {
        //kullanıcının claimlerini al
        //kullanıcının claimleri arasında rolü var mı kontrol et
        //varsa geç yoksa hata fırlat
        var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
            {
                return;
            }
        }
        throw new Exception(Messages.AuthorizationDenied);
    }
}