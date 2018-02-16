using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class RouteData
    {
        public IRouteHandler RouteHandler { get; set; }
        public IDictionary<string,object> Values { get; set; }
        public IDictionary<string,object> DataTokens { get; set; }
        public RouteBase Route { get; set; }
        public RouteData()
        {
            this.Values = new Dictionary<string, object>();
            this.DataTokens = new Dictionary<string, object>();
            this.DataTokens.Add("namespace", new List<string>());
        }
        public string Controller
        {
            get
            {
                object controller = String.Empty;
                Values.TryGetValue("controller", out controller);
                return controller.ToString();
            }
        }
        public string Action
        {
            get
            {
                object action = String.Empty;
                Values.TryGetValue("action", out action);
                return action.ToString();
            }
        }
    }
}