using System;
namespace PomodoroTimer
{
	public interface IMinuteStopWatch
	{
		void Start ();
		void Stop ();
		int Countdown { get; set; }
		
		event EventHandler Alert;
		event EventHandler<CountDownEventArgs> Tick;
	}
}
