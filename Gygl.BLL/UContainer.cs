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
            _unityContainer.RegisterType<Magazine.Service.IGyglService, Magazine.Service.GyglService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Magazine.Service.ICategoryService, Magazine.Service.CategoryService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<Magazine.Service.ICommentService, Magazine.Service.CommentService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<News.Service.INewsService, News.Service.NewsService>(new ContainerControlledLifetimeManager());
        }

        public UnityDependencyResolver GetUnity
        {
            get {
                return new UnityDependencyResolver(_unityContainer);
            }
        }
    }
}
