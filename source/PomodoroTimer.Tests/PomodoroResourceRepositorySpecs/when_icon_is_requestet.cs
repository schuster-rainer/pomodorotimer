namespace PomodoroTimer.Tests.PomodoroResourceRepositorySpecs
{
	using System.Drawing;
	using Xunit;

	[Concern (typeof (PomodoroResourceRepository))]
	public class when_icon_is_requestet : InstanceContextSpecification<PomodoroResourceRepository>
	{
		private static Icon resourceToRetrieve;
		protected override void Because ()
		{
			resourceToRetrieve = Sut.GetEmbeddedResourceByName<Icon>("alarmclock");
		}

		[Observation]
		public void should_return_an_icon_instance ()
		{
			resourceToRetrieve.ShouldBeAnInstanceOf<Icon>();
		}

		[Observation]
		public void should_return_a_valid_instance()
		{
			resourceToRetrieve.ShouldNotBeNull();
		}
	}
}