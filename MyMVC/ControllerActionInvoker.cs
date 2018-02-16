using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace MyMVC
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public IModelBinder ModelBinder { get; set; }

        public ControllerActionInvoker()
        {
            this.ModelBinder = new DefaultModelBinder();
        }

        public void InvokeAction(ControllerContext context, string actionName)
        {
            MethodInfo methodInfo = context.Controller.GetType()
                .GetMethods().First(m => string.Compare(actionName, m.Name, true) == 0);

            List<object> parameters = new List<object>();
            foreach(ParameterInfo parameter in methodInfo.GetParameters())
            {
                parameters.Add(this.ModelBinder.BindModel(context, parameter.Name, parameter.ParameterType));
            }

            ActionExecutor executor = new ActionExecutor(methodInfo);
            ActionResult actionResult = (ActionResult)executor.Execute(context.Controller, parameters.ToArray());
            actionResult.ExecuteResult(context);
        }
    }
}