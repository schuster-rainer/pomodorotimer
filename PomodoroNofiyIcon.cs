/*
 * Created by SharpDevelop.
 * User: RSchuster
 * Date: 16.02.2009
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace PomodoroTimer
{
	public sealed class PomodoroNofiyIcon : IDisposable
	{
		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		private MinuteStopWatch stopWatch;
		private MenuItem timerDisplayMenuItem;
		private System.ComponentModel.ComponentResourceManager resources;
		private static int POMODORO_TIME_INTERVAL = 25;
		private static int BREAK_TIME_INTERVAL = 5;
		private int currentTimeInterval = POMODORO_TIME_INTERVAL;
		private Icon pomodoroIcon;
		
		#region Initialize icon and menu
		public PomodoroNofiyIcon()
		{
			InitializeComponent ();
		}

		private void InitializeComponent ()
		{
			initializeResources ();
			initializeStopWatch ( POMODORO_TIME_INTERVAL );
			initializeMenu ();
			initializeNotifyIcon ();
		}

		private void initializeResources ()
		{
			resources = new System.ComponentModel.ComponentResourceManager ( typeof ( NotificationIconResources ) );
			pomodoroIcon = ( Icon )resources.GetObject ( "pomodoro" );
		}

		private void initializeStopWatch (int minutesToCountDown )
		{
			currentTimeInterval = minutesToCountDown;
			stopWatch = new MinuteStopWatch ( minutesToCountDown );
			stopWatch.Tick += new EventHandler<CountDownEventArgs> ( stopWatch_Tick );
			stopWatch.Alert += new EventHandler ( stopWatch_Alert );
		}

		void stopWatch_Alert ( object sender, EventArgs e )
		{
			notifyIcon.ShowBalloonTip ( 1000*60 );
			//MessageBox.Show ( "Pomodoro beendet!", "PomodoroTimer",
			//    MessageBoxButtons.OK, MessageBoxIcon.Stop );
		}

		private void initializeMenu ()
		{
			timerDisplayMenuItem = new MenuItem ( formatTimeDisplay(0,0) );
			MenuItem[] menu = new MenuItem[] {
				timerDisplayMenuItem,				
				new MenuItem("Start Pomodoro", menuStartPomodoroClick),				
				new MenuItem("Start Break", menuStartBreakClick),
				//new MenuItem("Stop", menuStopClick),
				new MenuItem("About", menuAboutClick),
				new MenuItem("Exit", menuExitClick)
			};

			notificationMenu = new ContextMenu ( menu );
		}


		private void initializeNotifyIcon ()
		{			
			notifyIcon = new NotifyIcon ();				
			notifyIcon.DoubleClick += new EventHandler ( notifyIcon_DoubleClick );			
			notifyIcon.Icon = pomodoroIcon;
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
			var countDown = new TimeSpan(0, currentTimeInterval, 0) - e.Duration;
			timerDisplayMenuItem.Text = formatTimeDisplay ( countDown.Minutes, countDown.Seconds);
		}

		private string formatTimeDisplay ( int minutes, int seconds)
		{
			return string.Format ( "{0:00.}:{1:00.}", minutes, seconds );
		}
	
		#endregion
		
		#region Event Handlers
		private void menuAboutClick(object sender, EventArgs e)
		{
			AboutDialog about = new AboutDialog ();
			about.ShowDialog ();
		}
		
		private void menuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void menuStartPomodoroClick(object sender, EventArgs e)
		{
			resetStopWatch ();
			initializeStopWatch ( POMODORO_TIME_INTERVAL );
			stopWatch.Start ();
		}

		private void menuStartBreakClick ( object sender, EventArgs e )
		{
			resetStopWatch ();
			initializeStopWatch ( BREAK_TIME_INTERVAL );
			stopWatch.Start ();
		}

		private void resetStopWatch ()
		{
			stopWatch.Stop ();
			stopWatch = null;
		}

		private void menuStopClick(object sender, EventArgs e)
		{
			stopWatch.Stop ();
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
