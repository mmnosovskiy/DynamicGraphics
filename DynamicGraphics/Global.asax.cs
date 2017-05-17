using System;
using System.Web.Routing;

namespace DynamicGraphics
{
    /// <summary>
    /// Глобальный класс веб-приложения.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Метод, вызывающийся при запуске веб-приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        /// <summary>
        /// Метод для регистрации роутов.
        /// </summary>
        /// <param name="routes"></param>
        static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("R1", "{id}", "~/WebForm1.aspx");
        }
    }
}