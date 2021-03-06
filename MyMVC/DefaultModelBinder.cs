﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;

namespace MyMVC
{
    public class DefaultModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, 
            string modelName, Type modelType)
        {
            if(modelType.IsValueType || typeof(string) == modelType)
            {
                object instance;
                if(GetValueTypeInstance(controllerContext,modelName,modelType,out instance))
                {
                    return instance;
                }
                return Activator.CreateInstance(modelType);
            }

            object modelInstance = Activator.CreateInstance(modelType);
            foreach(PropertyInfo property in modelType.GetProperties())
            {
                if(!property.CanWrite || (!property.PropertyType.IsValueType 
                    && property.PropertyType != typeof(string)))
                {
                    continue;
                }

                object propertyValue;
                if(GetValueTypeInstance(controllerContext,property.Name,property.PropertyType,out propertyValue))
                {
                    property.SetValue(modelInstance, propertyValue, null);
                }
            }
            return modelInstance;
        }

        private bool GetValueTypeInstance(ControllerContext controllerContext, string modelName, Type modelType, out object instance)
        {
            Dictionary<string, object> dataSource =
                new Dictionary<string, object>();

            foreach(string key in controllerContext.RequestContext.HttpContext.Request.Form)
            {
                if(dataSource.ContainsKey(key.ToLower()))
                {
                    continue;
                }
                dataSource.Add(key.ToLower(), controllerContext.RequestContext.HttpContext.Request.Form[key]);
            }

            foreach(string key in controllerContext.RequestContext.HttpContext.Request.QueryString)
            {
                if (dataSource.ContainsKey(key.ToLower()))
                {
                    continue;
                }
                dataSource.Add(key.ToLower(), controllerContext.RequestContext.HttpContext.Request.QueryString[key]);
            }

            foreach(var item in controllerContext.RequestContext.RouteData.Values)
            {
                if (dataSource.ContainsKey(item.Key.ToLower()))
                {
                    continue;
                }
                dataSource.Add(item.Key.ToLower(), controllerContext.RequestContext.RouteData.Values[item.Key.ToLower()]);
            }

            foreach (var item in controllerContext.RequestContext.RouteData.Values)
            {
                if (dataSource.ContainsKey(item.Key.ToLower()))
                {
                    continue;
                }
                dataSource.Add(item.Key.ToLower(), controllerContext.RequestContext.RouteData.DataTokens[item.Key.ToLower()]);
            }

            if(dataSource.TryGetValue(modelName.ToLower(), out instance))
            {
                instance = Convert.ChangeType(instance, modelType);
                return true;
            }
            return false;
        }
    }
}