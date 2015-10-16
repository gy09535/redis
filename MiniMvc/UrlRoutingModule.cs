using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace YunYing.MiniMvc
{
    public class UrlRoutingModule : IHttpModule
    {
        public void Dispose()
        { }
        public void Init(HttpApplication context)
        {
            context.PostResolveRequestCache += OnPostResolveRequestCache;
        }
        protected virtual void OnPostResolveRequestCache(object sender, EventArgs e)
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            if (null == routeData)
            {
                return;
            }
            var requestContext = new RequestContext { RouteData = routeData, HttpContext = httpContext };
            var handler = routeData.RouteHandler.GetHttpHandler(requestContext);
            httpContext.RemapHandler(handler);
        }
    }
}