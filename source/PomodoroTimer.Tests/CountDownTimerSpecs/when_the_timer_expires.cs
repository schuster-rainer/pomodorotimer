using Xunit;

namespace PomodoroTimer.Tests.CountDownTimerSpecs
{
	[Concern (typeof (CountDownTimer))]
	public class when_the_timer_expires : concern
	{
		protected override void Because ()
		{
			Sut.Start();
			alertEvent.WasRaisedAfter (CountDownTimeOut);
			alertEvent.Reset();
			tickEvent.Reset();
		}

		[Observation]
		public void should_not_alert_any_more ()
		{
			bool eventRaised = alertEvent.WasRaisedAfter (CountDownTimeOut);
			eventRaised.ShouldBeFalse();
		}

		[Observation]
        public void should_not_tick_any_more()
		{
			bool eventRaised = tickEvent.WasRaisedAfter (TickRateTimeOut);
			eventRaised.ShouldBeFalse();
		}
	}
}