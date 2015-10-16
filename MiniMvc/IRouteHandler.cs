using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace YunYing.MiniMvc
{
    public interface IRouteHandler
    {
        IHttpHandler GetHttpHandler(RequestContext requestContext);
    }
}