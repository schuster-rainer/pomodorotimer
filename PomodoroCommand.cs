using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{
	public abstract class PomodoroCommand : ICommand
	{
		protected ICountDownTimer countDownTimer;
		public PomodoroCommand ( ICountDownTimer countDownTimer )
		{
			this.countDownTimer = countDownTimer;
		}

		#region ICommand Members

		public abstract void Execute ();

		#endregion
	}

	public class StartPomodoroCommand : PomodoroCommand
	{
		public StartPomodoroCommand ( ICountDownTimer countDownTimer )
			: base ( countDownTimer )
		{
		}

		public override void Execute ()
		{
			countDownTimer.Stop ();
			countDownTimer.CountDown = Settings.Default.PomodoroTimeInterval;
			countDownTimer.Start ();
		}
	}

	public class StartPomodoroBreakCommand : PomodoroCommand
	{
		public StartPomodoroBreakCommand ( ICountDownTimer countDownTimer )
			: base ( countDownTimer )
		{
		}

		public override void Execute ()
		{
			countDownTimer.Stop ();
			countDownTimer.CountDown = Settings.Default.PomodoroBreakTimeInterval;
			countDownTimer.Start ();
		}
	}

	public class StartPomodoroSetBreakCommand : PomodoroCommand
	{
		public StartPomodoroSetBreakCommand ( ICountDownTimer countDownTimer )
			: base ( countDownTimer )
		{
		}
		public override void Execute ()
		{
			countDownTimer.Stop ();
			countDownTimer.CountDown = Settings.Default.PomodoroSetBreakTimeInterval;
			countDownTimer.Start ();
		}
	}

	public class ExitApplicationCommand : ICommand
	{
		public ExitApplicationCommand ()
		{
		}

		#region IPomodoroCommand Members

		public void Execute ()
		{
			Application.Exit ();
		}

		#endregion
	}

	public class AboutPomodoroCommand : ICommand
	{
		#region IPomodoroCommand Members

		public void Execute ()
		{
			AboutDialog about = new AboutDialog ();
			about.ShowDialog ();
		}

		#endregion
	}
}
