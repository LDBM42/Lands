namespace Lands.Backend
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Helpers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            this.CheckRolesAndSuperUser();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // al arrancar verifica si está el Admin y el User
        // si no existe lo creará.
        //tambien crea el super usuario que es el que
        //se introdujo en el Web.config.
        private void CheckRolesAndSuperUser()
        {
            UsersHelper.CheckRole("Admin");
            UsersHelper.CheckRole("User");
            UsersHelper.CheckSuperUser();
        }
    }
}
