namespace PomodoroTimer.Commands
{
	public class AboutPomodoroCommand : ICommand
	{
		public void Execute ()
		{
			using (var about = new AboutDialog())
			{
				about.ShowDialog ();
			}
		}
	}
}