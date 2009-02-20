/*
 * Created by SharpDevelop.
 * User: RSchuster
 * Date: 16.02.2009
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{
	public interface IResourceRepository
    {
    	T GetEmbeddedResourceByName<T>( string resourceName);
    }

	public class ResourceRepository : IResourceRepository
	{
		private ComponentResourceManager resources;

		public ResourceRepository ()
		{
			initializeResourceManager();
		}

		private void initializeResourceManager()
		{
 			resources = new ComponentResourceManager ( typeof ( NotificationIconResources ) );
		}

		#region IResources Members

		public T GetEmbeddedResourceByName<T>(string resourceName)
		{
 			return (T)resources.GetObject ( "alarmclock" );
		}

		#endregion
	}

	public interface ICommand
	{
		void Execute ();
	}

	public interface IPomodoroController
	{
		ICommand StartPomodoroCommand { get; }
		ICommand StartPomodoroBreakCommand { get; }
		ICommand StartSetBreakCommand { get; }
		ICommand AboutCommand { get; }
		ICommand ExitCommand { get; }
	}

    public sealed class Pomodoro : IDisposable
	{
		private IMinuteStopWatch stopWatch;
		private IResourceRepository resourceRepository;
		private IPomodoroController controller;

		private NotifyIcon notifyIcon;
		
		private int currentTimeInterval;

		private ContextMenu notificationMenu;
		private MenuItem timerDisplayMenuItem;
		


		#region Initialize icon and menu
		public Pomodoro (IMinuteStopWatch stopWatch,
						 IResourceRepository resourRepository,
						 IPomodoroController controller)
		{
			this.stopWatch = stopWatch;
			this.resourceRepository = resourceRepository;
			this.controller = controller;
			InitializeComponent ();
		}

		private void InitializeComponent ()
		{
			initializeStopWatch ( Settings.Default.PomodoroTimeInterval );
			initializeMenu ();
			initializeNotifyIcon ();
		}

		private void initializeStopWatch ( int minutesToCountDown )
		{
			currentTimeInterval = minutesToCountDown;
			stopWatch = new MinuteStopWatch ( minutesToCountDown );
			stopWatch.Tick += new EventHandler<CountDownEventArgs> ( stopWatch_Tick );
			stopWatch.Alert += new EventHandler ( stopWatch_Alert );
		}

		void stopWatch_Alert ( object sender, EventArgs e )
		{
			notifyIcon.ShowBalloonTip ( 1000*60 );
			MessageBox.Show ( "Timebox completed!", "PomodoroTimer",
				MessageBoxButtons.OK, MessageBoxIcon.Stop );
		}

		private void initializeMenu ()
		{
			timerDisplayMenuItem = new MenuItem ( formatTimeDisplay ( 0, 0 ) );
			MenuItem[] menu = new MenuItem[] {
				timerDisplayMenuItem,				
				new MenuItem("Start Pomodoro", (s,e) => controller.StartPomodoroCommand.Execute() ),				
				new MenuItem("Start Break", (s,e) => controller.StartPomodoroBreakCommand.Execute() ),
				new MenuItem("Start Set-Break", (s,e) => controller.StartSetBreakCommand.Execute() ),
				new MenuItem("About", (s,e)=>controller.AboutCommand.Execute()),
				new MenuItem("Exit", (s,e) => controller.ExitCommand.Execute())
			};

			notificationMenu = new ContextMenu ( menu );
		}


		private void initializeNotifyIcon ()
		{
			notifyIcon = new NotifyIcon ();
			notifyIcon.DoubleClick += new EventHandler ( notifyIcon_DoubleClick );
			notifyIcon.Icon = resourceRepository.GetEmbeddedResourceByName<Icon>("alarmclock");
			notifyIcon.ContextMenu = notificationMenu;
			notifyIcon.BalloonTipTitle = "Pomodoro Timer";
			notifyIcon.BalloonTipText = "Pomodoro ended";
		}

		void notifyIcon_DoubleClick ( object sender, EventArgs e )
		{
			stopWatch.Start ();
		}

		void stopWatch_Tick ( object sender, CountDownEventArgs e )
		{
			var countDown = new TimeSpan ( 0, currentTimeInterval, 0 ) - e.Duration;
			timerDisplayMenuItem.Text = formatTimeDisplay ( countDown.Minutes, countDown.Seconds );
		}

		private string formatTimeDisplay ( int minutes, int seconds )
		{
			return string.Format ( "{0:00.}:{1:00.}", minutes, seconds );
		}

		#endregion

		#region Event Handlers
		private void menuAboutClick ( object sender, EventArgs e )
		{
			AboutDialog about = new AboutDialog ();
			about.ShowDialog ();
		}

		private void menuExitClick ( object sender, EventArgs e )
		{
			Application.Exit ();
		}

		private void menuStartPomodoroClick ( object sender, EventArgs e )
		{
			resetStopWatch ();
			initializeStopWatch ( Settings.Default.PomodoroTimeInterval );
			stopWatch.Start ();
		}

		private void menuStartBreakClick ( object sender, EventArgs e )
		{
			resetStopWatch ();
			initializeStopWatch ( Settings.Default.PomodoroBreakTimeInterval );
			stopWatch.Start ();
		}

		private void resetStopWatch ()
		{
			stopWatch.Stop ();
			stopWatch = null;
		}

		private void menuStartSetBreackClick ( object sender, EventArgs e )
		{
			resetStopWatch ();
			initializeStopWatch ( Settings.Default.SetBreakTimeInterval );
			stopWatch.Start ();
		}

		#endregion

		public bool Visible
		{
			get
			{
				return notifyIcon.Visible;
			}
			set
			{
				notifyIcon.Visible = value;
			}
		}

		#region IDisposable Members

		public void Dispose ()
		{
			notifyIcon.Dispose ();
		}

		#endregion
	}
}
