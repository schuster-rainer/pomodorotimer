using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime.Exceptions;
using IronPython.Runtime.Types;
using IronPython.Runtime.Operations;
using Microsoft.Practices.Unity;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;

namespace PomodoroTimer.Plugin
{
    public abstract class ScriptEnvironmentBase : IScriptEnvironment
    {
        protected ScriptEngine engine;
        protected ScriptRuntime runtime;
        protected IOutputStream outputStream;
        private readonly IUnityContainer Container;

        public ScriptEnvironmentBase(IOutputStream outputStream, IUnityContainer container)
        {
            this.outputStream = outputStream;
            Container = container;
        }

        public virtual void CreateAndInitializeRuntime()
        {
            this.engine = createEngine();
            this.runtime = engine.Runtime;

            Stream runtimeStream = createRuntimeStream(outputStream);
            setRuntimeIOStreams(runtimeStream);
            addAssembliesToRuntime();
        }

        private void registerGlobals(ScriptScope globals)
        {
            OnRegisterGlobals(new ScriptScopeEventArgs(globals));
        }

        public event EventHandler<ScriptScopeEventArgs> RegisterGlobals;

        protected virtual void OnRegisterGlobals(ScriptScopeEventArgs eventArgs)
        {
            if( RegisterGlobals != null)
            {
                RegisterGlobals(this, eventArgs);
            }
        }

        public abstract ScriptEngine createEngine();

    
        protected abstract Stream createRuntimeStream(IOutputStream outputStream);


        private void setRuntimeIOStreams(Stream engineStream)
        {
            runtime.IO.SetOutput(engineStream, Encoding.UTF8);
            runtime.IO.SetErrorOutput(engineStream, Encoding.UTF8);
        }

       
        protected virtual void addAssembliesToRuntime()
        {
            addExecutingAssemblyToRuntime();
            addStringAndUriContainingAssembliesToRuntime();
        }

        private void addExecutingAssemblyToRuntime()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            runtime.LoadAssembly(executingAssembly);
        }

        private void addStringAndUriContainingAssembliesToRuntime()
        {
            runtime.LoadAssembly(typeof(String).Assembly);
            runtime.LoadAssembly(typeof(Uri).Assembly);
        }

        public void TryExecuteScriptFromPath(string path)
        {
            try
            {
                executeScriptFromPath(path);
            }
            catch (SyntaxErrorException e)
            {
                string msg = "Syntax error in \"{0}\"";
                showError(msg, Path.GetFileName(path), e);
            }
            catch (SystemExitException e)
            {
                string msg = "SystemExit in \"{0}\"";
                showError(msg, Path.GetFileName(path), e);
            }

            catch (Exception e)
            {
                string msg = "Error loading plugin \"{0}\"";
                showError(msg, Path.GetFileName(path), e);
            }
        }

        private void executeScriptFromPath(string path)
        {
            ScriptSource source = engine.CreateScriptSourceFromFile(path);
            CompiledCode code = source.Compile(); 
            ScriptScope globals = engine.CreateScope();
            registerGlobals(globals);
            source.Execute(globals);

            var items = globals.GetItems();
            var plugins = (from item in items
                           let type = item.Value as PythonType
                           where type != null
                           let clrType = IronPython.Runtime.ClrModule.GetClrType(type)
                           where clrType.IsAbstract == false
                           where clrType.BaseType == typeof(PluginCommandBase)
                           select clrType);

            foreach (Type pluginType in plugins)
            {
                //object cmd = Container.Resolve(pluginType);
                //var cmdRep = Container.Resolve<ICommandRepository>();
                //cmdRep.AddCommand(cmd as IPluginCommand);
            }

            //Test obj = Ops.Call(ptype) as Test;

        }

        private void showError(string title, string name, Exception e)
        {
            string caption = String.Format(title, name);
            ExceptionOperations eo = engine.GetService<ExceptionOperations>();
            string error = eo.FormatException(e);
            runtime.IO.ErrorWriter.Write(error);
        }
    }
}
