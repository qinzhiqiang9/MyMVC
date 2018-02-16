﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class Route : RouteBase
    {
        public string RouteTemplate { get; set; }
        public IRouteHandler RouteHandler { get; set; }
        public IDictionary<string,object> DataTokens { get; set; }

        public Route()
        {
            this.DataTokens = new Dictionary<string, object>();
            this.RouteHandler = new MvcRouteHandler();
        }

        public Route(string routeTemplate)
        {
            this.DataTokens = new Dictionary<string, object>();
            this.RouteTemplate = routeTemplate;
            this.RouteHandler = new MvcRouteHandler();
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            IDictionary<string, object> variables;
            if (this.Match(httpContext.Request
                .AppRelativeCurrentExecutionFilePath.Substring(2), out variables))
            {
                RouteData routeData = new RouteData();
                foreach(var item in variables)
                {
                    routeData.Values.Add(item.Key, item.Value);
                }
                foreach (var item in DataTokens)
                {
                    routeData.DataTokens.Add(item.Key, item.Value);
                }
                routeData.RouteHandler = this.RouteHandler;
                return routeData;
            }
            return null;
        }

        protected bool Match(string requestUrl, out IDictionary<string, object> variables)
        {
            variables = new Dictionary<string, object>();
            string[] strArray1 = requestUrl.Split('/');
            string[] strArray2 = RouteTemplate.Split('/');

            if(strArray1.Length != strArray2.Length)
            {
                return false;
            }

            for (int i = 0; i < strArray2.Length; i++)
            {
                if (strArray2[i].StartsWith("{") && strArray2[i].EndsWith("}"))
                {
                    variables.Add(strArray2[i].Trim("{}".ToCharArray()), strArray1[i]);
                }
                else
                {
                    if (string.Compare(strArray1[i], strArray2[i], true) != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}