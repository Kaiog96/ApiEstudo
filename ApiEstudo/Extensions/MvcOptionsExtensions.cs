namespace ApiEstudo.Extensions
{
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc;
    using ApiEstudo.Configuration;

    public static class MvcOptionsExtensions
    {
        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
            => opts.Conventions.Insert(0, new RouteConvention(routeAttribute));
    }
}
