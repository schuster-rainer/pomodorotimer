using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PomodoroTimer
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
			this.mutex = new Mutex ( mutexIsInitiallyOwned, uniqueName, out isFirstInstance );
		}


		#region IDisposable Members

		public void Dispose ()
		{
		}

		#endregion
	}
}
