namespace MyMVC
{
    public interface IActionInvoker
    {
        void InvokeAction(ControllerContext context, string actionName);
    }
}