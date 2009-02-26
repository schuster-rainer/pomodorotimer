using System;
namespace PomodoroTimer.Commands
{
	public class PomodoroTimerCommand : ICommand
	{
		private readonly ICountDownTimer countDownTimer;
		private readonly TimeSpan countDown;

		public PomodoroTimerCommand ( ICountDownTimer countDownTimer, TimeSpan countDown )
		{
			this.countDownTimer = countDownTimer;
			this.countDown = countDown;
		}

		public virtual void Execute ()
		{
			countDownTimer.Stop();
			countDownTimer.CountDown = countDown;
			countDownTimer.Start();
		}
	}
}