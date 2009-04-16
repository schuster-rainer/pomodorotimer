using Xunit;

namespace PomodoroTimer.Tests.CountDownTimerSpecs
{
	[Concern (typeof (CountDownTimer))]
	public class when_the_timer_was_started_and_then_stopped : concern
	{
		protected override void Because ()
		{
			Sut.Start();
			Sut.Stop();

			tickEvent.Reset();
		}

		[Observation]
		public void should_not_receive_any_more_ticks ()
		{
			bool eventOccured = tickEvent.WasRaisedAfter (TickRateTimeOut);
			eventOccured.ShouldBeFalse();
		}
	}
}