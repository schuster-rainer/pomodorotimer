/*
 * Created by SharpDevelop.
 * User: RSchuster
 * Date: 16.02.2009
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{

    public sealed class PomodoroView : IPomodoroView, IDisposable
	{
		private static int FIFTEEN_SECONDS_IN_MILLISEC = 15*1000;

		private IResourceRepository resourceRepository;
		private IPomodoroController controller;

		private NotifyIcon notifyIcon;

		private ContextMenu notificationMenu;
		private MenuItem timerDisplayMenuItem;

		#region Initialize icon and menu
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
			notifyIcon = new NotifyIcon ();
			notifyIcon.Icon = resourceRepository.GetEmbeddedResourceByName<Icon>("alarmclock");
			notifyIcon.ContextMenu = notificationMenu;
			notifyIcon.BalloonTipTitle = "Pomodoro Timer";
			notifyIcon.BalloonTipText = "Pomodoro ended";
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

		#region IPomodoroView Members

		public void Show ()
		{
			throw new NotImplementedException ();
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

		private string formatTimeDisplay ( int minutes, int seconds )
		{
			return string.Format ( "{0:00.}:{1:00.}", minutes, seconds );
		}
	}
}
