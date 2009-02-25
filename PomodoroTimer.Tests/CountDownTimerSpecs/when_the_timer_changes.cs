using System.Threading;
using Xunit;

namespace PomodoroTimer.Tests.CountDownTimerSpecs
{
	[Concern (typeof (CountDownTimer))]
	public class when_the_timer_changes : concern
	{
		protected ManualResetEvent timerChangedEvent;

		protected override void EstablishContext ()
		{
			timerChangedEvent = new ManualResetEvent (false);
		}

		protected override CountDownTimer CreateSut ()
		{
			var countDownTimer = base.CreateSut();
			countDownTimer.TimerChanged += (s, e) => timerChangedEvent.Set();
			return countDownTimer;
		}

		protected override void Because ()
		{
			
		}

		[Observation]
		public void it_should_not_tick ()
		{

		}
	}
}