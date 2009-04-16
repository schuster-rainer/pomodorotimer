using System;

namespace PomodoroTimer.Plugin
{
    public interface ICommandScriptEnvironment : IScriptEnvironment
    {
        void ExecuteCommand(IPluginCommand command);
        void ExecuteCommandById(Guid pluginId);
        void ExecuteCommandByName(string name);
    }
}