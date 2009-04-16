using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PomodoroTimer.Tests.Scripting
{
	using Microsoft.Practices.Unity;
	using Plugin;
	using Xunit;

	[Concern (typeof (ScriptCommandDecorator))]
	public class when_plugin_environment_is_created : InstanceContextSpecification<ScriptCommandDecorator>
	{
		private static ScriptEnvironmentBase scriptEnvironment;
		private static IOutputStream outputStream;
		private static IUnityContainer container;
		private static ICommandRepository commandRepository;

		protected override void EstablishContext ()
		{
			outputStream = Dependency <IOutputStream>();
			container = Dependency <IUnityContainer>();
			commandRepository = Dependency <ICommandRepository>();
			scriptEnvironment = new PythonScriptEnvironment(outputStream);
		}

		protected override ScriptCommandDecorator CreateSut ()
		{
			return new ScriptCommandDecorator (scriptEnvironment, commandRepository, container);
		}

		protected override void Because ()
		{
			Sut.CreateAndInitializeRuntime();
		}

		[Observation]
		public void should_load_plugin ()
		{
		}
	}
	public class ScriptEnvironmentSpec
	{
	}
}
