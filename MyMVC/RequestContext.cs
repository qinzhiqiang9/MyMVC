using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class RequestContext
    {
        public HttpContextBase HttpContext { get; set; }
        public RouteData RouteData { get; set; }
    }
}