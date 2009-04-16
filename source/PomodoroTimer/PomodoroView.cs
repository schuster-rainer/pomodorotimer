using System;
using System.Drawing;
using System.Windows.Forms;

namespace PomodoroTimer
{
	using Plugin;

	public sealed class PomodoroView : IPomodoroView, IDisposable
	{
		private const int FIFTEEN_SECONDS_IN_MILLISEC = 15*1000;

		private readonly IResourceRepository resourceRepository;
		private readonly IPomodoroController controller;
		private readonly IScriptCommandEnvironment scriptCommandEnv;

		private NotifyIcon notifyIcon;
		private ContextMenuStrip notificationMenu;
		private ToolStripMenuItem timerDisplayMenuItem;
		private ToolStripMenuItem pluginsMenuItem;

		public PomodoroView (
						 IResourceRepository resourceRepository,
						 IPomodoroController controller,
						 IScriptCommandEnvironment scriptCommandEnv)
		{

			this.resourceRepository = resourceRepository;
			this.controller = controller;
			this.scriptCommandEnv = scriptCommandEnv;
			this.controller.View = this;
			scriptCommandEnv.CommandCreated += scriptCommandEnv_CommandCreated;

			initializeComponent ();
			
			scriptCommandEnv.CreateAndInitializeRuntime();
		}

		void scriptCommandEnv_CommandCreated(object sender, CommandEventArgs e)
		{
			pluginsMenuItem.DropDownItems.Add (new ToolStripMenuItem (e.Command.GetName()));
			pluginsMenuItem.Enabled = true;
		}

		private void initializeComponent ()
		{
			initializeMenuAndBindCommands ();
			initializeNotifyIcon ();
		}	

		private void initializeMenuAndBindCommands ()
		{
			notificationMenu = new ContextMenuStrip ();

			pluginsMenuItem = new ToolStripMenuItem("Plugins");
			pluginsMenuItem.Enabled = false;
			timerDisplayMenuItem = new ToolStripMenuItem(formatTimeDisplay(0, 0));

			notificationMenu.Items.Add (timerDisplayMenuItem);
			notificationMenu.Items.Add (new ToolStripMenuItem("Start Pomodoro", null,
					(s,e) => controller.StartCommand.Execute() ) );
			notificationMenu.Items.Add (new ToolStripMenuItem ("Start Break", null,
				                       (s, e) => controller.StartBreakCommand.Execute()));

			notificationMenu.Items.Add (new ToolStripMenuItem("Start Set-Break", null,
					(s,e) => controller.StartSetBreakCommand.Execute() ));

			notificationMenu.Items.Add(new ToolStripSeparator());
			notificationMenu.Items.Add (pluginsMenuItem);
			notificationMenu.Items.Add (new ToolStripSeparator());
			notificationMenu.Items.Add (new ToolStripMenuItem("About", null,
					(s,e) => controller.AboutCommand.Execute() ) );

			notificationMenu.Items.Add (new ToolStripMenuItem("Exit", null,
					(s,e) => controller.ExitCommand.Execute() ) );
			
		}


		private void initializeNotifyIcon ()
		{
			notifyIcon = new NotifyIcon
			             	{
			             		Icon = resourceRepository.GetEmbeddedResourceByName <Icon> ("alarmclock"),
			             		ContextMenuStrip = notificationMenu,
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
