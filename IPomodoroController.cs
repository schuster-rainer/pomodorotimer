using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{
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
