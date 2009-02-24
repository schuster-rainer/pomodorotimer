using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PomodoroTimer
{
	public class CountDownEventArgs : EventArgs
	{
		public TimeSpan Duration { get; private set; }

		public CountDownEventArgs ( TimeSpan duration )
		{
			this.Duration = duration;
		}
	}

	public class CountDownTimer : ICountDownTimer
	{
		private System.Timers.Timer timer;
		private DateTime startTime;
		private DateTime lastSignaledTime;

		#region ICountDownTimer Members

		public TimeSpan CountDown { get; set; }
		public event EventHandler<CountDownEventArgs> TimerChanged;
		public event EventHandler Alert;
		public event EventHandler<CountDownEventArgs> Tick;

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

		#endregion

		public CountDownTimer ()
		{
			initializeTimer ();
		}

		private void initializeTimer ()
		{
			createTimerToTickAfterOneSecond ();
			attachTickEvent ();
		}
		

		private void createTimerToTickAfterOneMinute ()
		{
			timer = new System.Timers.Timer ( 1000 * 60 );
		}

		private void createTimerToTickAfterOneSecond ()
		{
			timer = new System.Timers.Timer ( 1000 );
		}

		private void attachTickEvent ()
		{
			timer.Elapsed += new System.Timers.ElapsedEventHandler ( timer_Elapsed );
		}

		void timer_Elapsed ( object sender, System.Timers.ElapsedEventArgs e )
		{
			lastSignaledTime = e.SignalTime;
			checkCountDown ();
		}

		private void checkCountDown ()
		{
			TimeSpan elapsedTime = lastSignaledTime-startTime;
            bool timeHasExpired = elapsedTime >= CountDown;

			OnTick ( elapsedTime );
			
			if ( timeHasExpired )
			{
				Stop ();
				OnAlert ();
			}			
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
			EventHandler alert = new EventHandler ( Alert );
			if ( alert != null )
			{
				alert ( this, EventArgs.Empty );
			}
		}

		private void FireCountDownEvent (
			EventHandler<CountDownEventArgs> eventHandler,
			TimeSpan duration )
		{
			EventHandler<CountDownEventArgs> eventHandlerWrapper = new EventHandler<CountDownEventArgs> ( eventHandler );
			if ( eventHandlerWrapper != null )
			{
				eventHandlerWrapper ( this, new CountDownEventArgs ( duration ) );
			}
		}
	}
}
