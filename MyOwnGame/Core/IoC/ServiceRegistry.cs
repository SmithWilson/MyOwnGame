using MyOwnGame.Core.Services.DataProvider;
using MyOwnGame.Core.Services.Navigation;
using MyOwnGame.Core.Services.Window;
using Unity;
using Unity.Lifetime;

namespace MyOwnGame.Core.IoC
{
    public class ServiceRegistry : IRegistries
    {
        public void Register(IUnityContainer ioc)
        {
			ioc
				.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager())
				.RegisterType<IWindowManager, WindowManager>()
				.RegisterType<IQuestionService, QuestionService>()
				.RegisterType<ITopicService, TopicService>()
				.RegisterType<IRoundService, RoundService>()
				.RegisterType<GameContext>(new ContainerControlledLifetimeManager()); 
        }
    }
}