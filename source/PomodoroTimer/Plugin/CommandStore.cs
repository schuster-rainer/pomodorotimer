using System;
using System.Collections;
using System.Collections.Generic;

namespace PomodoroTimer.Plugin
{
    public interface ICommandRepository : IEnumerable<IPluginCommand>
    {
        List<IPluginCommand> Commands { get; }
        void AddCommand(IPluginCommand plugin);
    }

    public class CommandRepository : ICommandRepository
    {
        private readonly List<IPluginCommand> plugins = new List<IPluginCommand>();

        public List<IPluginCommand> Commands
        {
            get { return plugins; }
        }

        public void AddCommand(IPluginCommand plugin)
        {
            plugins.Add(plugin);
        }

        public IEnumerator<IPluginCommand> GetEnumerator()
        {
            return Commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}