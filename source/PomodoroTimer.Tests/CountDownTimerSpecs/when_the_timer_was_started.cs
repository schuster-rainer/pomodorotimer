using Xunit;

namespace PomodoroTimer.Tests.CountDownTimerSpecs
{
	[Concern (typeof (CountDownTimer))]
	public class when_the_timer_was_started : concern
	{
		protected override void Because ()
		{
			Sut.Start();
		}

		[Observation]
		public void should_tick ()
		{
		    bool eventRaised = tickEvent.WasRaisedAfter (TickRateTimeOut);
		    eventRaised.ShouldBeTrue();
		}

	    [Observation]
		public void should_alert_after_timer_expired ()
		{
			alertEvent.WasRaisedAfter (CountDownTimeOut).ShouldBeTrue();
		}
	}
}