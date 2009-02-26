namespace PomodoroTimer.Commands
{
	using Properties;

	public class StartPomodoroBreakCommand : PomodoroTimerCommand
	{
		public StartPomodoroBreakCommand ( ICountDownTimer countDownTimer )
			: base(countDownTimer, Settings.Default.PomodoroBreakTimeInterval)
		{
		}
	}
}