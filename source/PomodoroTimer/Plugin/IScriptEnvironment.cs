using System;
using Microsoft.Scripting.Hosting;

namespace PomodoroTimer.Plugin
{
    public interface IScriptEnvironment
    {
        void CreateAndInitializeRuntime();
        event EventHandler<ScriptScopeEventArgs> RegisterGlobals;
		event EventHandler<ScriptScopeEventArgs> ScriptExecuted;
    }

    public class ScriptScopeEventArgs : EventArgs
    {
        public ScriptScope Scope { get; private set; }

        public ScriptScopeEventArgs(ScriptScope scope)
        {
            Scope = scope;
        }
    }
}