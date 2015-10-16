using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Compilation;

namespace YunYing.MiniMvc
{
public class DefaultControllerFactory : IControllerFactory
{
    private List<Type> controllerTypes = new List<Type>();
    public DefaultControllerFactory()
    {
        foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
        {
            foreach (Type type in assembly.GetTypes().Where(type => typeof(IController).IsAssignableFrom(type)))
            {
                controllerTypes.Add(type);
            }
        }
    }
    public IController CreateController(RequestContext requestContext, string controllerName)
    {
        string typeName = controllerName + "Controller";
        List<string> namespaces = new List<string>();
        namespaces.AddRange(requestContext.RouteData.Namespaces);
        namespaces.AddRange(ControllerBuilder.Current.DefaultNamespaces);
        foreach (var ns in namespaces)
        {
            string controllerTypeName = string.Format("{0}.{1}", ns, typeName);
            Type controllerType = controllerTypes.FirstOrDefault(type => string.Compare(type.FullName, controllerTypeName, true) == 0);
            if (null != controllerType)
            {
                return (IController)Activator.CreateInstance(controllerType); 
            }
        }            
        return null;
    }
}
}