using System;
using PomodoroTimer.Commands;

namespace PomodoroTimer.Plugin
{
    public interface IPluginCommand : ICommand
    {
        Guid Id { get; }
        string Name { get; }
    }
}