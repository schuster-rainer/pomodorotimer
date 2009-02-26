using System;
using System.Drawing;
using System.Windows.Forms;

namespace PomodoroTimer
{

    public sealed class PomodoroView : IPomodoroView, IDisposable
	{
		private const int FIFTEEN_SECONDS_IN_MILLISEC = 15*1000;

		private readonly IResourceRepository resourceRepository;
		private readonly IPomodoroController controller;

		private NotifyIcon notifyIcon;

		private ContextMenu notificationMenu;
		private MenuItem timerDisplayMenuItem;

		public PomodoroView (
						 IResourceRepository resourceRepository,
						 IPomodoroController controller)
		{

			this.resourceRepository = resourceRepository;
			this.controller = controller;
			this.controller.View = this;
			initializeComponent ();
		}

		private void initializeComponent ()
		{
			initializeMenuAndBindCommands ();
			initializeNotifyIcon ();
		}	

		private void initializeMenuAndBindCommands ()
		{
			timerDisplayMenuItem = new MenuItem ( formatTimeDisplay ( 0, 0 ) );
			MenuItem[] menu = new MenuItem[] {
				timerDisplayMenuItem,				
				new MenuItem("Start Pomodoro", 
					(s,e) => controller.StartCommand.Execute() ),				
				new MenuItem("Start Break", 
					(s,e) => controller.StartBreakCommand.Execute() ),
				new MenuItem("Start Set-Break",
					(s,e) => controller.StartSetBreakCommand.Execute() ),
				new MenuItem("About", 
					(s,e) => controller.AboutCommand.Execute() ),
				new MenuItem("Exit",
					(s,e) => controller.ExitCommand.Execute() )
			};

			notificationMenu = new ContextMenu ( menu );
		}


		private void initializeNotifyIcon ()
		{
			notifyIcon = new NotifyIcon
			             	{
			             		Icon = resourceRepository.GetEmbeddedResourceByName <Icon> ("alarmclock"),
			             		ContextMenu = notificationMenu,
			             		BalloonTipTitle = "Pomodoro Timer",
			             		BalloonTipText = "Pomodoro ended"
			             	};
		}


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

		#region IPomodoroView Members

		public void Show ()
		{
			Visible = true;
		}

		public void ShowAlert()
		{			
			notifyIcon.ShowBalloonTip ( FIFTEEN_SECONDS_IN_MILLISEC );
			MessageBox.Show ( "Timebox completed!", "PomodoroTimer",
				MessageBoxButtons.OK, MessageBoxIcon.Stop );
		}

		private TimeSpan countdown;
		public TimeSpan Countdown
		{
			get
			{
				return countdown;
			}
			set
			{
				countdown = value;
				timerDisplayMenuItem.Text = formatTimeDisplay ( countdown.Minutes, countdown.Seconds );
			}
		}

		#endregion

		private static string formatTimeDisplay ( int minutes, int seconds )
		{
			return string.Format ( "{0:00.}:{1:00.}", minutes, seconds );
		}
	}
}
