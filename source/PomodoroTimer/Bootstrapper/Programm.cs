using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using PomodoroTimer.Plugin;

namespace PomodoroTimer.Bootstrapper
{
	class Programm
	{
		[STAThread]
		public static void Main ( string[] args )
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault ( false );

			using ( var uniqueTrayIconInstance = new UniqueClassInstance ( "PomodoroTimer" ) )
			{
				if ( uniqueTrayIconInstance.IsFirstInstance )
				{
					RunApplication ();
				}
			}
		}

		private static void RunApplication ()
		{
			IUnityContainer container = createDependencyContainer ();
			configureDependencyContainer ( container );

            //var commandEnvironment = container.Resolve<ICommandScriptEnvironment>();
            //commandEnvironment.CreateAndInitializeRuntime();

            //foreach (var command in container.Resolve<ICommandRepository>())
            //{
            //    command.Execute(); 
            //}
            
			var pomodoroView = container.Resolve<IPomodoroView> ();
			pomodoroView.Show();
			Application.Run();
		}

		private static IUnityContainer createDependencyContainer ()
		{
			return new UnityContainer ();
		}

		private static void configureDependencyContainer ( IUnityContainer container )
		{
			container.RegisterType<IResourceRepository, PomodoroResourceRepository> ( new ContainerControlledLifetimeManager () );
			container.RegisterType<ICountDownTimer, CountDownTimer> (new ContainerControlledLifetimeManager() );
			container.RegisterType<IPomodoroController, PomodoroController> (new ContainerControlledLifetimeManager());
			container.RegisterType<IPomodoroView, PomodoroView> ( new ContainerControlledLifetimeManager());
			container.RegisterType<IPomodorCommandFactory, PomodoroCommandFactory> (new ContainerControlledLifetimeManager());
		    container.RegisterType<IOutputStream, OutputStreamFacade>(new ContainerControlledLifetimeManager());
            container.RegisterType<ScriptEnvironmentBase, PythonScriptEnvironment>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommandScriptEnvironment, PluginCommandScriptDecorator>(new ContainerControlledLifetimeManager());
		    container.RegisterType<ICommandRepository, CommandRepository> (new ContainerControlledLifetimeManager());

		    container.RegisterInstance<IUnityContainer>(container);

		}

        internal class OutputStreamFacade : IOutputStream
        {
            public void Write(string text)
            {
                Debug.Write(text);
            }
        }
	}
}