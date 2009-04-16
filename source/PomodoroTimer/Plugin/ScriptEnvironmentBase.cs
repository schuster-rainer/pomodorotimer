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

        public ScriptEnvironmentBase(IOutputStream outputStream)
        {
            this.outputStream = outputStream;
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
    	public event EventHandler <ScriptScopeEventArgs> ScriptExecuted;

        protected virtual void OnRegisterGlobals(ScriptScopeEventArgs eventArgs)
        {
            if( RegisterGlobals != null)
            {
                RegisterGlobals(this, eventArgs);
            }
        }

		protected virtual void OnScriptExecuted(ScriptScopeEventArgs e)
		{
			if (ScriptExecuted != null)
			{
				ScriptExecuted(this, e);
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
        	executeScript (source, globals);
        }

    	private void executeScript (ScriptSource source, ScriptScope globals)
    	{
			source.Execute(globals);
    		OnScriptExecuted (new ScriptScopeEventArgs (globals));
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
