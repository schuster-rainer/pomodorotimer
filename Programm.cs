using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Practices.Unity;

namespace PomodoroTimer
{
    class Programm
	{
		[STAThread]
		public static void Main ( string[] args )
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault ( false );

			using ( UniqueClassInstance trayIcon = new UniqueClassInstance ( "PomodoroTimer" ) )
			{
				if ( trayIcon.IsFirstInstance )
				{
					RunApplication ();
				}
			}
		}

		private static void RunApplication ()
		{
			IUnityContainer container = createDependencyContainer ();
			configureDependencyContainer ( container );
			PomodoroView notificationIcon = container.Resolve<PomodoroView> ();			
			notificationIcon.Visible = true;
			Application.Run ();
		}

		private static IUnityContainer createDependencyContainer ()
		{
			UnityContainer container = new UnityContainer ();
			return container;
		}

		private static void configureDependencyContainer ( IUnityContainer container )
		{
			container.RegisterType<IResourceRepository, ResourceRepository> ( new ContainerControlledLifetimeManager () );
			container.RegisterType<ICountDownTimer, CountDownTimer> (new ContainerControlledLifetimeManager() );
			container.RegisterType<IPomodoroController, PomodoroController> (new ContainerControlledLifetimeManager());
			container.RegisterType<IPomodoroView, PomodoroView> ( new ContainerControlledLifetimeManager());
			container.RegisterType<IPomodorCommandFactory, PomodoroCommandFactory> (new ContainerControlledLifetimeManager());
		}
	}
}