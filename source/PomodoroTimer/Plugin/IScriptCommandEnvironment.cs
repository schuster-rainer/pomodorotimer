using System;

namespace PomodoroTimer.Plugin
{
    public interface IScriptCommandEnvironment : IScriptEnvironment
    {
        void ExecuteCommand(IScriptCommand command);
        void ExecuteCommandById(Guid pluginId);
        void ExecuteCommandByName(string name);

    	event EventHandler <CommandEventArgs> CommandCreated;
    }

	public class CommandEventArgs : EventArgs
	{
		public IScriptCommand Command { get; private set; }

		public CommandEventArgs (IScriptCommand command)
		{
			Command = command;
		}
	}
}