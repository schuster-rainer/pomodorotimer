using System;
using PomodoroTimer.Commands;

namespace PomodoroTimer.Plugin
{
    public interface IScriptCommand : ICommand
    {
        Guid Id { get; }
    	string GetName ();
    }
}