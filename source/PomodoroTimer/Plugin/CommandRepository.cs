using System;
using System.Collections;
using System.Collections.Generic;

namespace PomodoroTimer.Plugin
{
	public class CommandRepository : ICommandRepository
    {
        private readonly List<IScriptCommand> plugins = new List<IScriptCommand>();

        public List<IScriptCommand> Commands
        {
            get { return plugins; }
        }

        public void AddCommand(IScriptCommand plugin)
        {
            plugins.Add(plugin);
        }

        public IEnumerator<IScriptCommand> GetEnumerator()
        {
            return Commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}