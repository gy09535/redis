using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YunYing.MiniMvc
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public IModelBinder ModelBinder { get; private set; }
        public ControllerActionInvoker()
        {
            this.ModelBinder = new DefaultModelBinder();
        }
        public void InvokeAction(ControllerContext controllerContext, string actionName)
        {
            var method = controllerContext.Controller.GetType().GetMethods().First(m => string.Compare(actionName, m.Name, true) == 0);
            var parameters = new List<object>();
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                parameters.Add(this.ModelBinder.BindModel(controllerContext, parameter.Name, parameter.ParameterType));
            }
            ActionResult actionResult = (ActionResult)method.Invoke(controllerContext.Controller, parameters.ToArray());
            actionResult.ExecuteResult(controllerContext);
        }
    }
}
