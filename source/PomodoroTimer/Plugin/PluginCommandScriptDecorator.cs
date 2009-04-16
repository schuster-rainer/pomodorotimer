using System;
using System.IO;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace PomodoroTimer.Plugin
{
    public class PluginCommandScriptDecorator : ICommandScriptEnvironment
    {
        private readonly ScriptEnvironmentBase scriptEnvironment;
        private readonly IUnityContainer container;
        protected string rootDir;

        public PluginCommandScriptDecorator( ScriptEnvironmentBase scriptEnvironment, IUnityContainer container)
        {
            this.scriptEnvironment = scriptEnvironment;
            this.container = container;
        }

        public void CreateAndInitializeRuntime()
        {
            RegisterGlobals += PluginCommandScriptDecorator_RegisterGlobals;
            
            scriptEnvironment.CreateAndInitializeRuntime();
            initializeRootDir();
            loadScripts();
        }

        void PluginCommandScriptDecorator_RegisterGlobals(object sender, ScriptScopeEventArgs e)
        {
            e.Scope.SetVariable("Commands", container.Resolve<ICommandRepository>());
            e.Scope.SetVariable("IoC", container);
        }

        public event EventHandler<ScriptScopeEventArgs> RegisterGlobals
        {
            add
            {
                scriptEnvironment.RegisterGlobals += value;
            }
            remove
            {
                scriptEnvironment.RegisterGlobals -= value;
            }
        }

        private void initializeRootDir()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            rootDir = Directory.GetParent(executingAssembly.Location).FullName;
        }


        private void loadScripts()
        {
            string pluginsDir = Path.Combine(rootDir, "plugins");
            executeScriptsFromPath(pluginsDir);
        }

        private void executeScriptsFromPath(string pluginsDir)
        {
            foreach (string path in Directory.GetFiles(pluginsDir))
            {
                if (path.ToLower().EndsWith(".py"))
                {
                    scriptEnvironment.TryExecuteScriptFromPath(path);
                }
            }
        }

        public void ExecuteCommand(IPluginCommand command)
        {
            command.Execute();
        }

        public void ExecuteCommandById(Guid pluginId)
        {
            
        }

        public void ExecuteCommandByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}