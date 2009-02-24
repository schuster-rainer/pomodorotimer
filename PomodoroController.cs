using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{
	public class PomodoroController : IPomodoroController
	{
		#region IPomodoroController Members

		public ICommand StartCommand { get; private set; }
		public ICommand StartBreakCommand { get; private set; }
		public ICommand StartSetBreakCommand { get; private set; }
		public ICommand AboutCommand { get; private set; }
		public ICommand ExitCommand { get; private set; }

		public IPomodoroView View { get; set; }

		#endregion

		private ICountDownTimer countDownTimer;		
		private IPomodorCommandFactory commandFactory;
		private TimeSpan timeSpanToCountDown;

		public PomodoroController ( ICountDownTimer countDownTimer, 
				IPomodorCommandFactory commandFactory)
		{
			this.countDownTimer = countDownTimer;
			this.commandFactory = commandFactory;
			InitializeComponent ();
			timeSpanToCountDown = new TimeSpan ( 0, 0, 0 );
		}

		private void InitializeComponent ()
		{
			initializeCountDownTimer( Settings.Default.PomodoroTimeInterval );
			initializeCommands();
		}

		private void initializeCommands ()
		{
			StartCommand = commandFactory.CreateStartCommand ();
			StartBreakCommand = commandFactory.CreateStartBreakCommand();
			StartSetBreakCommand = commandFactory.CreateStartSetBreakCommand ();
			ExitCommand = commandFactory.CreateExitCommand ();
			AboutCommand = commandFactory.CreateAboutCommand ();
		}

		private void initializeCountDownTimer ( TimeSpan countDown )
		{
			countDownTimer.Tick += new EventHandler<CountDownEventArgs> ( countDownTimer_Tick );
			countDownTimer.Alert += new EventHandler ( countDownTimer_Tick );
			countDownTimer.TimerChanged += new EventHandler<CountDownEventArgs> ( countDownTimer_TimerChanged );
		}

		void countDownTimer_TimerChanged ( object sender, CountDownEventArgs e )
		{
			timeSpanToCountDown = e.Duration;
		}

		#region Events
		private void countDownTimer_Tick ( object sender, CountDownEventArgs e )
		{
			View.Countdown = timeSpanToCountDown - e.Duration;
		}

		private void countDownTimer_Tick ( object sender, EventArgs e )
		{
			View.ShowAlert ();
		}
		#endregion
	}
}
