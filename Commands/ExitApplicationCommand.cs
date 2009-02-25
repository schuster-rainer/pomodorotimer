namespace PomodoroTimer.Commands
{
	using System.Windows.Forms;

	public class ExitApplicationCommand : ICommand
	{
		public void Execute ()
		{
			Application.Exit ();
		}
	}
}