using System;

namespace MyMVC
{
    public class ControllerBuilder
    {
        private Func<IControllerFactory> factoryThunk;

        public static ControllerBuilder Current { get; set; }

        static ControllerBuilder()
        {
            Current = new ControllerBuilder();
        }

        public IControllerFactory GetControllerFactory()
        {
            return factoryThunk();
        }

        public void SetControllerFactory(IControllerFactory controllerFactory)
        {
            factoryThunk = () => controllerFactory;
        }
    }
}