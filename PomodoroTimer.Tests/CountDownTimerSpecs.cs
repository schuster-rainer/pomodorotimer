using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using System.Threading;

namespace PomodoroTimer.Tests
{
	public class CountDownTimerSpecs
	{
		public abstract class concern 
			: InstanceContextSpecification<CountDownTimer>
		{
			protected static int EXTRA_MILLISEC_FOR_SAFTY = 1;
			protected static int TIMERTICK_IN_MILLISEC = 1000;
			protected TimeSpan countDownTimeSpan;
			protected ManualResetEvent tickEvent;
			protected ManualResetEvent alertEvent;

			protected override void EstablishContext ()
			{
				base.EstablishContext ();
				countDownTimeSpan = new TimeSpan ( 0, 0, 2 );
				tickEvent = new ManualResetEvent ( false );
				alertEvent = new ManualResetEvent ( false );
			}

			protected override CountDownTimer CreateSut ()
			{
				CountDownTimer stopWatch = new CountDownTimer ();
				stopWatch.CountDown = countDownTimeSpan;
				stopWatch.Alert += ( s, e ) => alertEvent.Set ();
				stopWatch.Tick += ( s, e ) => tickEvent.Set ();

				return stopWatch;
			}

			protected int ExpirationTimeoutInMilliSec
			{
				get
				{
					return ( int )countDownTimeSpan.TotalMilliseconds + EXTRA_MILLISEC_FOR_SAFTY;
				}
			}

			protected int TickTimeoutInMilliSec
			{
				get
				{
					return TIMERTICK_IN_MILLISEC + EXTRA_MILLISEC_FOR_SAFTY;
				}
			}
		}

		[Concern ( typeof ( CountDownTimer ) )]
		public class when_the_timer_was_started
			: concern
		{
			protected override void Because ()
			{
				Sut.Start ();
			}

			[Observation]
			public void it_should_tick_after_one_second ()
			{
				bool eventRaised = tickEvent.wasRaisedAfter ( TickTimeoutInMilliSec );
				eventRaised.ShouldBeTrue ();

			}

			[Observation]
			public void it_should_alert_after_timer_expired ()
			{
				alertEvent.wasRaisedAfter ( ExpirationTimeoutInMilliSec ).ShouldBeTrue ();
			}
		}

		[Concern ( typeof ( CountDownTimer ) )]
		public class when_the_timer_was_started_and_then_stopped
			: concern
		{
			protected override void Because ()
			{
				Sut.Start ();
				Sut.Stop ();

				tickEvent.Reset ();
			}

			[Observation]
			public void it_should_not_receive_any_more_ticks ()
			{
				bool eventOccured = tickEvent.wasRaisedAfter ( TickTimeoutInMilliSec );
				eventOccured.ShouldBeFalse ();
			}
		}

		[Concern ( typeof ( CountDownTimer ) )]
		public class when_the_timer_expires
			: concern
		{
			protected override void Because ()
			{
				Sut.Start ();
				alertEvent.wasRaisedAfter ( ExpirationTimeoutInMilliSec );
				alertEvent.Reset ();
				tickEvent.Reset ();
			}

			[Observation]
			public void it_should_not_alert_any_more ()
			{
				bool eventRaised = alertEvent.wasRaisedAfter ( ExpirationTimeoutInMilliSec );
				eventRaised.ShouldBeFalse ();
			}

			[Observation]
			public void it_should_not_tick_any_more ()
			{
				bool eventRaised = tickEvent.wasRaisedAfter ( TickTimeoutInMilliSec );
				eventRaised.ShouldBeFalse ();
			}
		}

		//[Concern ( typeof ( CountDownTimer ) )]
		//public class when_the_timer_changes
		//    : concern
		//{
		//    protected ManualResetEvent timerChangedEvent;
		//    stopWatch.TimerChanged += ( s, e ) => 
		//                        {
		//                            tickEvent.Set ();
		//                        }
        //	timerChangedEvent = new ManualResetEvent ( false );
		//}
	}
}