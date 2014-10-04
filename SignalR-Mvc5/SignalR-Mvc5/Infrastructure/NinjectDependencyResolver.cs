using Ninject;
using SignalR_Mvc5.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SignalR_Mvc5.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _ninjectKernel;

        public NinjectDependencyResolver()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
        }

        public object GetService(Type serviceType)
        {
            return _ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _ninjectKernel.GetAll(serviceType);
        }
    }
}