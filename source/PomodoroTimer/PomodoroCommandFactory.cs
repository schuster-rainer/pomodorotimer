namespace PomodoroTimer
{
	using Commands;

	public class PomodoroCommandFactory : IPomodorCommandFactory
	{
		private readonly ICountDownTimer countDownTimer;

		public PomodoroCommandFactory (ICountDownTimer countDownTimer)
		{
			this.countDownTimer = countDownTimer;
		}

		public ICommand CreateStartCommand ()
		{
			return new StartPomodoroCommand ( countDownTimer );
		}

		public ICommand CreateStartBreakCommand ()
		{
			return new StartPomodoroBreakCommand ( countDownTimer );
		}

		public ICommand CreateStartSetBreakCommand ()
		{
			return new StartPomodoroSetBreakCommand( countDownTimer );
		}

		public ICommand CreateExitCommand ()
		{
			return new ExitApplicationCommand ();
		}

		public ICommand CreateAboutCommand ()
		{
			return new AboutPomodoroCommand ();
		}
	}
}