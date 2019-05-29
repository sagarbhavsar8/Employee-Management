using System.Web.Http;
using Unity;
using Unity.WebApi;
using AddToMyCart.ServiceLayer;
using System.Web.Mvc;

namespace AddToMyWebCart
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<ICalendarsService, CalendarsService>();
            container.RegisterType<ISetPrioritiesService, SetPrioritiesService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}