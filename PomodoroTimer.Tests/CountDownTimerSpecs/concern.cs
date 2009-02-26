using System;
using System.Threading;
using Xunit;

namespace PomodoroTimer.Tests.CountDownTimerSpecs
{
	public abstract class concern : InstanceContextSpecification <CountDownTimer>
	{
        protected const int TICKRATE_IN_MILLISEC = 10;
		protected const int EXTRA_MILLISEC_FOR_SAFTY = 20;
		protected TimeSpan countDownTimeSpan;
        protected TimeSpan tickRate;
		protected ManualResetEvent tickEvent;
		protected ManualResetEvent alertEvent;

		protected override void EstablishContext ()
		{
			base.EstablishContext();
            countDownTimeSpan = new TimeSpan(0, 0, 0, 0, TICKRATE_IN_MILLISEC*5);
            tickRate = new TimeSpan(0, 0, 0, 0, TICKRATE_IN_MILLISEC);
			tickEvent = new ManualResetEvent (false);
			alertEvent = new ManualResetEvent (false);
		}

		protected override CountDownTimer CreateSut ()
		{
			var countDownTimer = new CountDownTimer
			                         {
			                             CountDown = countDownTimeSpan,
			                             TickRate = tickRate
			                         };

		    countDownTimer.Alert += (s, e) => alertEvent.Set();
			countDownTimer.Tick += (s, e) => tickEvent.Set();

			return countDownTimer;
		}

		protected int CountDownTimeOut
		{
			get
			{
				return (int) countDownTimeSpan.TotalMilliseconds
				       + EXTRA_MILLISEC_FOR_SAFTY;
			}
		}

		protected int TickRateTimeOut
		{
            get { return (int) tickRate.TotalMilliseconds + EXTRA_MILLISEC_FOR_SAFTY; }
		}
	}
}