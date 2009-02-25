namespace PomodoroTimer
{
	public interface IResourceRepository
	{
		T GetEmbeddedResourceByName<T> ( string resourceName );
	}
}
