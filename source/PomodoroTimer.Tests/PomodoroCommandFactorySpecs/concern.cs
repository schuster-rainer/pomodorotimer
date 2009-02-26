namespace PomodoroTimer.Tests.PomodoroCommandFactorySpecs
{
	using PomodoroTimer.Commands;
	using Xunit;

	[Concern (typeof (PomodoroCommandFactory))]
	public abstract class concern : InstanceContextSpecification <PomodoroCommandFactory>
	{
		private static ICountDownTimer countDownTimer;
		protected ICommand commandToCreate;

		protected override void EstablishContext ()
		{
			countDownTimer = Dependency <CountDownTimer>();
		}

		protected override PomodoroCommandFactory CreateSut ()
		{
			return new PomodoroCommandFactory (countDownTimer);
		}
	}
}
