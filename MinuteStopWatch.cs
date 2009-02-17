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

	public class MinuteStopWatch
	{
		private System.Timers.Timer timer;
		private DateTime startTime;
		private DateTime lastSignaledTime;
		private int minutesToCountDown;		

		public event EventHandler Alert;
		public event EventHandler<CountDownEventArgs> Tick;

		public MinuteStopWatch ( int minutesToCountDown)
		{
			this.minutesToCountDown = minutesToCountDown;
			initTimer ();
		}

		private void initTimer ()
		{
			createOneSecondElapsingTimer ();
			attachTickEvent ();
		}

		private void createOneMinuteElapsingTimer ()
		{
			timer = new System.Timers.Timer ( 1000 * 60 );
		}

		private void createOneSecondElapsingTimer ()
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
            bool timeHasExpired = elapsedTime.Minutes >= minutesToCountDown;

			notifyTick ( elapsedTime );
			
			if ( timeHasExpired )
			{
				Stop ();
				notifyAlert ();
			}			
		}

		private void notifyTick ( TimeSpan duration)
		{
			EventHandler<CountDownEventArgs> tick = new EventHandler<CountDownEventArgs> ( Tick );
			if ( tick != null )
			{
				tick ( this, new CountDownEventArgs ( duration ) );
			}
		}

		private void notifyAlert ()
		{
			EventHandler alert = new EventHandler ( Alert );
			if ( alert != null )
			{
				alert ( this, EventArgs.Empty );
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
	}
}
