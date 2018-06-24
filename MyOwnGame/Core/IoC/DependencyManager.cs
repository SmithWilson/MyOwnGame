using System.Collections.Generic;
using Unity;

namespace MyOwnGame.Core.IoC
{
    public class DependencyManager
    {
        #region Fields
        private readonly IUnityContainer _container;
        private readonly List<IRegistries> _dependencies;
        #endregion

        public DependencyManager()
        {
            _container = new UnityContainer();
            _dependencies = new List<IRegistries>();
        }


        public DependencyManager Registry(List<IRegistries> dependencies)
        {
            _dependencies.AddRange(dependencies);
            return this; 
        }

        public IUnityContainer Build()
        {
            _dependencies.ForEach(d => d.Register(_container));
            return _container;
        }
    }
}