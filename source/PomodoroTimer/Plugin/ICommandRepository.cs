namespace PomodoroTimer.Plugin
{
	using System.Collections.Generic;

	public interface ICommandRepository : IEnumerable<IScriptCommand>
	{
		List<IScriptCommand> Commands { get; }
		void AddCommand(IScriptCommand plugin);
	}
}