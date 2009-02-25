namespace PomodoroTimer.Tests.PomodoroCommandFactorySpecs
{
	using Xunit;

	[Concern(typeof(PomodoroCommandFactory))]
	public class when_start_setbreak_command_is_created : concern
	{
		protected override void Because()
		{
			commandToCreate = Sut.CreateStartSetBreakCommand();
		}

		[Observation]
		public void should_be_a_valid_instance()
		{
			commandToCreate.ShouldNotBeNull();
		}
	}
}