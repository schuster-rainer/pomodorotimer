namespace PomodoroTimer.Commands
{
	using Properties;

	public class StartPomodoroSetBreakCommand : PomodoroTimerCommand
	{
		public StartPomodoroSetBreakCommand ( ICountDownTimer countDownTimer )
			: base(countDownTimer, Settings.Default.PomodoroSetBreakTimeInterval)
		{
		}
	}
}