using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index(SimpleModel model)
        {
            string str = $"Controller is {model.Controller}, Action is {model.Action}";
            RawContentResult result = new RawContentResult(writer => writer.Write(str));
            return result;
        }
    }
}