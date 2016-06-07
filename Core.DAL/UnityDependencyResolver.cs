using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.DAL
{
    public  class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _unityContainer;
        public UnityDependencyResolver(IUnityContainer _unityContainer)
        {
            this._unityContainer = _unityContainer;
        }

        public object GetService(Type serviceType)
        {
            return (serviceType.IsClass && !serviceType.IsAbstract) ||
            _unityContainer.IsRegistered(serviceType) ?
            _unityContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return (serviceType.IsClass && !serviceType.IsAbstract) ||
            _unityContainer.IsRegistered(serviceType) ?
            _unityContainer.ResolveAll(serviceType) : new object[] { };
        }
    }
}
