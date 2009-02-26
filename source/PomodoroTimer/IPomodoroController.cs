namespace PomodoroTimer
{
	using Commands;

	public interface IPomodoroController
	{
		ICommand StartCommand { get; }
		ICommand StartBreakCommand { get; }
		ICommand StartSetBreakCommand { get; }
		ICommand AboutCommand { get; }
		ICommand ExitCommand { get; }

		IPomodoroView View { get; set; }
	}
}
