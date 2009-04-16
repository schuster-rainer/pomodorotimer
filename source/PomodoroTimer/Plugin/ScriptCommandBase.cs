using System;

namespace PomodoroTimer.Plugin
{
    public abstract class ScriptCommandBase : IScriptCommand
    {
		public Guid Id
		{
			get; private set;
		}

    	public abstract string GetName ();

        protected ScriptCommandBase()
        {
            //this.name = name;
            Id = Guid.NewGuid();
        }

    	public virtual void Execute ()
    	{
    	}
    }
}