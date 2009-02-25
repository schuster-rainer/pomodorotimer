namespace PomodoroTimer.Commands
{
	using Properties;

	public class StartPomodoroCommand : PomodoroTimerCommand
	{
		public StartPomodoroCommand ( ICountDownTimer countDownTimer )
			: base(countDownTimer, Settings.Default.PomodoroTimeInterval)
		{
		}
	}
}