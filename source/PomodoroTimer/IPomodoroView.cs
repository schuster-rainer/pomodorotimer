using System;

namespace PomodoroTimer
{
	public interface IPomodoroView
	{
		void Show ();
		void ShowAlert ();
		TimeSpan Countdown { get; set; }
	}
}
