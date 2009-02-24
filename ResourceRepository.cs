using System.ComponentModel;

namespace PomodoroTimer
{
	public class ResourceRepository : IResourceRepository
	{
		private ComponentResourceManager resources;

		public ResourceRepository ()
		{
			initializeResourceManager ();
		}

		private void initializeResourceManager ()
		{
			resources = new ComponentResourceManager ( typeof ( NotificationIconResources ) );
		}

		#region IResources Members

		public T GetEmbeddedResourceByName<T> ( string resourceName )
		{
			return ( T )resources.GetObject ( "alarmclock" );
		}

		#endregion
	}
}
