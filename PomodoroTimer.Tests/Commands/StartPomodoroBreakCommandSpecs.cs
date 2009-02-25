namespace PomodoroTimer.Tests.Commands
{
	using System;
	using PomodoroTimer.Commands;
	using Xunit;

	[Concern (typeof (PomodoroTimerCommand))]
	public class when_command_is_executed : InstanceContextSpecification<PomodoroTimerCommand>
	{
		private ICountDownTimer countDownTimer;
		private TimeSpan countDown;

		protected override void EstablishContext ()
		{
			countDownTimer = Dependency <ICountDownTimer>();
			countDown = new TimeSpan(0,0,1);
		}

		protected override PomodoroTimerCommand CreateSut ()
		{
			return new PomodoroTimerCommand (countDownTimer, countDown );
		}

		protected override void Because ()
		{
			Sut.Execute();
		}

		[Observation]
		public void should_reset_the_timer ()
		{
			countDownTimer.WasToldTo(x => x.Stop());
		}

		[Observation]
		public void should_start_the_timer ()
		{
			countDownTimer.WasToldTo (x => x.Start());
		}
	}
}
