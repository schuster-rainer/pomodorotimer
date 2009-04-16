using System;

namespace PomodoroTimer.Plugin
{
    public class PluginCommandBase : IPluginCommand
    {
        private string name;
        private Guid id;

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        protected PluginCommandBase(string name)
        {
            this.name = name;
            this.id = Guid.NewGuid();
        }

        virtual public void Execute()
        {
        }
    }
}