using System;
using System.Threading;

namespace PomodoroTimer.Bootstrapper
{
	public class UniqueClassInstance : IDisposable
	{
		private Mutex mutex;
		private const bool mutexIsInitiallyOwned = true;
		
		private bool isFirstInstance;
		public bool IsFirstInstance { get { return isFirstInstance; } }
		
		public UniqueClassInstance ( string uniqueName )
		{
			createUniqueMutex ( uniqueName );
		}

		private void createUniqueMutex ( string uniqueName )
		{
			mutex = new Mutex ( mutexIsInitiallyOwned, uniqueName, out isFirstInstance );
		}


		#region IDisposable Members

		public void Dispose ()
		{
		}

		#endregion
	}
}