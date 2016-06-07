using Core.DAL;
using Microsoft.Practices.Unity;

namespace Cms.BLL
{
    public class UContainer
    {
        private IUnityContainer _unityContainer;
        public UContainer()
        {
            _unityContainer = new UnityContainer();
            AddBindings();
            //new UnityDependencyResolver(_unityContainer);
        }

        private void AddBindings()
        {
            //_unityContainer.RegisterType<Register.IWebService, Register.WebService>(new ContainerControlledLifetimeManager())
            //.RegisterType<Contribute.IWebService, Contribute.WebService>(new ContainerControlledLifetimeManager());
            //_unityContainer.RegisterType<IWebRepository<Contract.Models.User.Users>, WebRepository<Contract.Models.User.Users, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());
        }
        public IUnityContainer GetUnity
        {
            get
            {
                return _unityContainer;
            }
        }
    }
    public class UnityInject : UnityDependencyResolver
    {
        public UnityInject() : base(new UContainer().GetUnity)
        {

        }
    }
}
