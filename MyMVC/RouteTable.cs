using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class RouteTable
    {
        public static RouteDictionary Routes { get; set; }
        static RouteTable()
        {
            Routes = new RouteDictionary();
        }
    }
}