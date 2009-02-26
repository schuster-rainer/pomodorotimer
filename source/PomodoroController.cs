using System;

namespace PomodoroTimer
{
	using Commands;

	public class PomodoroController : IPomodoroController
	{
		public ICommand StartCommand { get; private set; }
		public ICommand StartBreakCommand { get; private set; }
		public ICommand StartSetBreakCommand { get; private set; }
		public ICommand AboutCommand { get; private set; }
		public ICommand ExitCommand { get; private set; }

		public IPomodoroView View { get; set; }

		private readonly ICountDownTimer countDownTimer;		
		private readonly IPomodorCommandFactory commandFactory;
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
			subcribeToCountDownTimerEvents();
			createCommands();
		}

		private void createCommands ()
		{
			StartCommand = commandFactory.CreateStartCommand ();
			StartBreakCommand = commandFactory.CreateStartBreakCommand();
			StartSetBreakCommand = commandFactory.CreateStartSetBreakCommand ();
			ExitCommand = commandFactory.CreateExitCommand ();
			AboutCommand = commandFactory.CreateAboutCommand ();
		}

		private void subcribeToCountDownTimerEvents ()
		{
			countDownTimer.Tick += countDownTimer_Tick;
			countDownTimer.Alert += countDownTimer_Tick;
			countDownTimer.TimerChanged += countDownTimer_TimerChanged;
		}

		void countDownTimer_TimerChanged ( object sender, CountDownEventArgs e )
		{
			timeSpanToCountDown = e.Duration;
		}

		private void countDownTimer_Tick ( object sender, CountDownEventArgs e )
		{
			View.Countdown = timeSpanToCountDown - e.Duration;
		}

		private void countDownTimer_Tick ( object sender, EventArgs e )
		{
			View.ShowAlert ();
		}
	}
}
