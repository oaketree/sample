using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Cms.BLL
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer _unityContainer;
        public UnityDependencyResolver()
        {
            _unityContainer = new UnityContainer();
            AddBindings();
        }

        private void AddBindings()
        {
            //_unityContainer.RegisterType<Register.IWebService, Register.WebService>(new ContainerControlledLifetimeManager())
            //.RegisterType<Contribute.IWebService, Contribute.WebService>(new ContainerControlledLifetimeManager());

            //_unityContainer.RegisterType<IWebRepository<Contract.Models.User.Users>, WebRepository<Contract.Models.User.Users, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());

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
