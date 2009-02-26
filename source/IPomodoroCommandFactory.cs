namespace PomodoroTimer
{
	using Commands;

	public interface IPomodorCommandFactory
	{
		ICommand CreateStartCommand ();
		ICommand CreateStartBreakCommand ();
		ICommand CreateStartSetBreakCommand ();
		ICommand CreateExitCommand ();
		ICommand CreateAboutCommand ();
	}
}
