using Core.DAL;
using Microsoft.Practices.Unity;

namespace Gygl.BLL
{
    public class UContainer
    {
        private IUnityContainer _unityContainer;
        public UContainer()
        {
            _unityContainer = new UnityContainer();
            AddBindings();
        }

        private void AddBindings()
        {
            _unityContainer.RegisterType<Register.Manage.IUserManage, Register.Manage.UserManage>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Register.Manage.IUserDetailManage, Register.Manage.UserDetailManage>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Register.Manage.IRoleAuthoriseManage, Register.Manage.RoleAuthoriseManage>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Register.Manage.IUserRoleManage, Register.Manage.UserRoleManage>(new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<Magazine.Service.IArticleService, Magazine.Service.ArticleService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Magazine.Service.IGyglCategoryService, Magazine.Service.GyglCategoryService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Magazine.Service.IImageService, Magazine.Service.ImageService>(new ContainerControlledLifetimeManager());
        }

        public UnityDependencyResolver GetUnity
        {
            get {
                return new UnityDependencyResolver(_unityContainer);
            }
        }
    }
}
