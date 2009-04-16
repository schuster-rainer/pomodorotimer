using System;
using System.IO;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace PomodoroTimer.Plugin
{
	using System.ComponentModel;
	using System.Linq;
	using IronPython.Runtime.Types;
	using Microsoft.Scripting.Hosting;

	public class ScriptCommandDecorator : IScriptCommandEnvironment
    {
        private readonly ScriptEnvironmentBase scriptEnvironment;
		private readonly ICommandRepository commandRepository;
		private readonly IUnityContainer container;
        protected string rootDir;

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
		public event EventHandler<ScriptScopeEventArgs> ScriptExecuted
		{
			add
			{
				scriptEnvironment.ScriptExecuted += value;
			}
			remove
			{
				scriptEnvironment.ScriptExecuted -= value;
			}
		}
		public event EventHandler<CommandEventArgs> CommandCreated;

        public ScriptCommandDecorator( ScriptEnvironmentBase scriptEnvironment, 
											ICommandRepository commandRepository,
											IUnityContainer container)
        {
            this.scriptEnvironment = scriptEnvironment;
        	this.commandRepository = commandRepository;
        	this.container = container;
        }

        public void CreateAndInitializeRuntime()
        {
            RegisterGlobals += PluginCommandScriptDecorator_RegisterGlobals;
			ScriptExecuted += PluginCommandScriptDecorator_ScriptExecuted;
            
            scriptEnvironment.CreateAndInitializeRuntime();
            initializeRootDir();
            loadScripts();
        }

		void PluginCommandScriptDecorator_ScriptExecuted(object sender, ScriptScopeEventArgs e)
		{
			//get name of subclass python-type from PluginCommandBase
			var pluginTypes = (from item in e.Scope.GetItems()
							   let @type = item.Value as PythonType
							   where @type != null
							   let clrType = (Type)@type
							   where clrType.IsAbstract == false
							   where clrType.IsInterface == false
							   let @interfaces = clrType.GetInterfaces()
							   where @interfaces.Contains(typeof(IScriptCommand))
							   select item.Key);
			
			ObjectOperations op = e.Scope.Engine.Operations;

			foreach (var pluginType in pluginTypes)
			{
				object @class = e.Scope.GetVariable(pluginType); // get the class object
				object @instance = op.Call(@class); // create the instance
				//object method = op.GetMember(instance, "Execute"); // get a method
				//op.Call(method); // call method and get the result

				var cmd =  @instance as IScriptCommand;
				commandRepository.AddCommand(cmd);
				OnCommandCreated (cmd);
			}
		}

		protected virtual void OnCommandCreated (IScriptCommand command)
		{
			if( CommandCreated != null)
			{
				CommandCreated (this, new CommandEventArgs (command));
			}
		}

		void PluginCommandScriptDecorator_RegisterGlobals(object sender, ScriptScopeEventArgs e)
        {
            e.Scope.SetVariable("Commands", container.Resolve<ICommandRepository>());
            e.Scope.SetVariable("IoC", container);
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

        public void ExecuteCommand(IScriptCommand command)
        {
            command.Execute();
        }

        public void ExecuteCommandById(Guid pluginId)
        {
			throw new NotImplementedException();
        }

        public void ExecuteCommandByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}