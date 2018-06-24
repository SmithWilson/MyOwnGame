using MyOwnGame.Core.IoC;
using MyOwnGame.Core.Services.Window;
using System.Collections.Generic;
using System.Windows;
using Unity;

namespace MyOwnGame
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
    { 
        protected override void OnStartup(StartupEventArgs e)
        {
			base.OnStartup(e);

            var dependencyManager = new DependencyManager();
            dependencyManager
                .Registry(new List<IRegistries>
                {
                    new ServiceRegistry()
                });

            dependencyManager
                .Build() 
                .Resolve<IWindowManager>()
                .Create();
        }
    }
}