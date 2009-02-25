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
		public void it_should_tick_after_one_second ()
		{
			bool eventRaised = tickEvent.wasRaisedAfter (TickTimeoutInMilliSec);
			eventRaised.ShouldBeTrue();
		}

		[Observation]
		public void it_should_alert_after_timer_expired ()
		{
			alertEvent.wasRaisedAfter (ExpirationTimeoutInMilliSec).ShouldBeTrue();
		}
	}
}