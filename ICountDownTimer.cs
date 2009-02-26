using System;
namespace PomodoroTimer
{
	public interface ICountDownTimer
	{
		void Start ();
		void Stop ();
		TimeSpan CountDown { get; set; }
        TimeSpan TickRate { get; set; }

		event EventHandler<CountDownEventArgs> TimerChanged;
		event EventHandler Alert;
		event EventHandler<CountDownEventArgs> Tick;
	}
}
