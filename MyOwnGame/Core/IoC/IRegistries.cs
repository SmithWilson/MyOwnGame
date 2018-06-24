using Unity;

namespace MyOwnGame.Core.IoC
{
    public interface IRegistries
    {
        void Register(IUnityContainer ioc); 
    }
}
