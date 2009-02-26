using System;
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
            base.EstablishContext();
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
		    Sut.CountDown = new TimeSpan(0,0,0,1);
		}

		[Observation]
		public void should_signal_change ()
		{
		    timerChangedEvent.wasRaisedAfter(TickRateTimeOut);
		}
	}
}