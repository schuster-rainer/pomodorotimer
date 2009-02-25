using System;

namespace PomodoroTimer
{
	public class CountDownEventArgs : EventArgs
	{
		public TimeSpan Duration { get; private set; }

		public CountDownEventArgs ( TimeSpan duration )
		{
			Duration = duration;
		}
	}

	public class CountDownTimer : ICountDownTimer
	{
		private System.Timers.Timer timer;
		private DateTime startTime;
		private DateTime lastSignaledTime;
		private TimeSpan countDown;

		public event EventHandler<CountDownEventArgs> TimerChanged;
		public event EventHandler Alert;
		public event EventHandler<CountDownEventArgs> Tick;

		public TimeSpan CountDown
		{
			get { return countDown; }
			set
			{
				countDown = value;
				OnTimerChanged(countDown);
			}
		}

		public void Start ()
		{
			startTime = DateTime.Now;
			timer.Stop ();
			timer.Start ();
		}

		public void Stop ()
		{
			timer.Stop ();
		}

		public CountDownTimer ()
		{
			initializeTimer ();
		}

		private void initializeTimer ()
		{
			createTimerToTickAfterOneSecond ();
			attachTickEvent ();
		}

		private void createTimerToTickAfterOneSecond ()
		{
			timer = new System.Timers.Timer ( 1000 );
		}

		private void attachTickEvent ()
		{
			timer.Elapsed += timer_Elapsed;
		}

		void timer_Elapsed ( object sender, System.Timers.ElapsedEventArgs e )
		{
			lastSignaledTime = e.SignalTime;
			checkCountDown ();
		}

		private void checkCountDown ()
		{
			TimeSpan elapsedTime = lastSignaledTime - startTime;
            bool timeHasExpired = elapsedTime >= CountDown;

			OnTick ( elapsedTime );

			if (!timeHasExpired) return;

			Stop ();
			OnAlert ();
		}

		private void OnTick ( TimeSpan duration)
		{
			FireCountDownEvent ( Tick, duration );
		}


		private void OnTimerChanged ( TimeSpan duration )
		{
			FireCountDownEvent ( TimerChanged, duration );
		}

		private void OnAlert ()
		{
			if ( Alert != null )
			{
				Alert.Invoke( this, EventArgs.Empty );
			}
		}

		private void FireCountDownEvent (
			EventHandler<CountDownEventArgs> eventHandler,
			TimeSpan duration )
		{
			if ( eventHandler != null )
			{
				eventHandler.Invoke( this, new CountDownEventArgs ( duration ) );
			}
		}
	}
}
