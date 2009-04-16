using System;
using System.IO;
using IronPython.Hosting;
using Microsoft.Practices.Unity;
using Microsoft.Scripting.Hosting;

namespace PomodoroTimer.Plugin
{
    public class PythonScriptEnvironment : ScriptEnvironmentBase
    {
        private readonly IOutputStream outputStream;

        public PythonScriptEnvironment( IOutputStream outputStream, IUnityContainer container)
            : base(outputStream, container)
        {
            this.outputStream = outputStream;
        }

        protected override Stream createRuntimeStream(IOutputStream outputStream)
        {
            var pythonStream = new PythonStreamBridge(outputStream);
            return pythonStream;
        }

        protected override void addAssembliesToRuntime()
        {
            //addPluginsAssemblyToRuntime();
            base.addAssembliesToRuntime();
        }

        //private void addPluginsAssemblyToRuntime()
        //{
        //    //string pluginsAssemblyPath = Path.Combine(rootDir, "Plugins.dll");
        //    //Assembly pluginsAssembly = Assembly.LoadFile(pluginsAssemblyPath);
        //    //runtime.LoadAssembly(pluginsAssembly);
        //}

        public override ScriptEngine createEngine()
        {
            return Python.CreateEngine();
        }
    }
}