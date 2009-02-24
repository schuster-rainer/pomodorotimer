using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Practices.Unity;

namespace PomodoroTimer
{
	public interface IPomodorCommandFactory
	{
		ICommand CreateStartCommand ();
		ICommand CreateStartBreakCommand ();
		ICommand CreateStartSetBreakCommand ();
		ICommand CreateExitCommand ();
		ICommand CreateAboutCommand ();
	}

	public class PomodoroCommandFactory : IPomodorCommandFactory
	{
		private ICountDownTimer countDownTimer;
		public PomodoroCommandFactory (ICountDownTimer countDownTimer)
		{
			this.countDownTimer = countDownTimer;
		}

		#region IPomodorCommandFactory Members

		public ICommand CreateStartCommand ()
		{
			return new StartPomodoroBreakCommand ( countDownTimer );
		}

		public ICommand CreateStartBreakCommand ()
		{
			return new StartPomodoroBreakCommand ( countDownTimer );
		}

		public ICommand CreateStartSetBreakCommand ()
		{
			return new StartPomodoroSetBreakCommand( countDownTimer );
		}

		public ICommand CreateExitCommand ()
		{
			return new ExitApplicationCommand ();
		}

		public ICommand CreateAboutCommand ()
		{
			return new AboutPomodoroCommand ();
		}

		#endregion
	}
}
