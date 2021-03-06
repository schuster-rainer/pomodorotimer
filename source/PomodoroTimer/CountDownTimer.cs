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
	    private TimeSpan tickRate;

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

	    public TimeSpan TickRate
	    {
	        get { return tickRate; }
	        set
	        {
	            tickRate = value;
	            timer.Interval = tickRate.TotalMilliseconds;
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
		    createTimer();
			setTickRateToOneSecond ();
			attachTickEvent ();
		}

        private void createTimer()
        {
            timer = new System.Timers.Timer();
        }

        private void setTickRateToOneSecond()
		{
            TickRate = new TimeSpan(0,0,1);
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
            bool timerHasExpired = elapsedTime >= CountDown;

			OnTick ( elapsedTime );

			if (!timerHasExpired) return;

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
