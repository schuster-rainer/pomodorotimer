using System.ComponentModel;

namespace PomodoroTimer
{
	public class PomodoroResourceRepository : IResourceRepository
	{
		private ComponentResourceManager resources;

		public PomodoroResourceRepository ()
		{
			initializeResourceManager ();
		}

		private void initializeResourceManager ()
		{
			resources = new ComponentResourceManager ( typeof ( PomodoroResources ) );
		}

		#region IResources Members

		public T GetEmbeddedResourceByName<T> ( string resourceName )
		{
			return ( T )resources.GetObject ( "alarmclock" );
		}

		#endregion
	}
}
