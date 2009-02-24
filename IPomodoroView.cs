using System;
using System.Drawing;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{
	public interface IPomodoroView
	{
		void Show ();
		void ShowAlert ();
		TimeSpan Countdown { get; set; }
	}
}
