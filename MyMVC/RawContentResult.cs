using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyMVC
{
    public class RawContentResult : ActionResult
    {
        public Action<TextWriter> Callback { get; private set; }

        public RawContentResult(Action<TextWriter> callback)
        {
            this.Callback = callback;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.Callback(context.RequestContext.HttpContext.Response.Output);
        }
    }
}