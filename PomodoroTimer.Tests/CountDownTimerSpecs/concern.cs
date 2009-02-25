using System;
using System.Threading;
using Xunit;

namespace PomodoroTimer.Tests.CountDownTimerSpecs
{
	public abstract class concern : InstanceContextSpecification <CountDownTimer>
	{
		protected const int EXTRA_MILLISEC_FOR_SAFTY = 20;
		protected const int TIMERTICK_IN_MILLISEC = 1000;
		protected TimeSpan countDownTimeSpan;
		protected ManualResetEvent tickEvent;
		protected ManualResetEvent alertEvent;

		protected override void EstablishContext ()
		{
			base.EstablishContext();
			countDownTimeSpan = new TimeSpan (0, 0, 2);
			tickEvent = new ManualResetEvent (false);
			alertEvent = new ManualResetEvent (false);
		}

		protected override CountDownTimer CreateSut ()
		{
			var stopWatch = new CountDownTimer
			                	{
			                		CountDown = countDownTimeSpan
			                	};

			stopWatch.Alert += (s, e) => alertEvent.Set();
			stopWatch.Tick += (s, e) => tickEvent.Set();

			return stopWatch;
		}

		protected int ExpirationTimeoutInMilliSec
		{
			get
			{
				return (int) countDownTimeSpan.TotalMilliseconds
				       + EXTRA_MILLISEC_FOR_SAFTY;
			}
		}

		protected static int TickTimeoutInMilliSec
		{
			get { return TIMERTICK_IN_MILLISEC + EXTRA_MILLISEC_FOR_SAFTY; }
		}
	}
}