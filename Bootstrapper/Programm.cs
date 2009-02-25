using System;
using System.Windows.Forms;
using Microsoft.Practices.Unity;

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
			var pomodoroView = container.Resolve<PomodoroView> ();
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
		}
	}
}