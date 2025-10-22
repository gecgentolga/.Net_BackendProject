using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Build.Logging;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EfOrderDetailDal>().As<IOrderDetailDal>();
        builder.RegisterType<EfOrderDal>().As<IOrderDal>();
        builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>();  
        builder.RegisterType<OrderManager>().As<IOrderService>();       
        builder.RegisterType<ProductManager>().As<IProductService>();
        builder.RegisterType<EfProductDal>().As<IProductDal>();
        builder.RegisterType<CategoryManager>().As<ICategoryService>();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();
        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).InstancePerLifetimeScope();//her istekde bir tane instance oluştur
    }
}