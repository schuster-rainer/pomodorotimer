﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Rhino.Mocks;

namespace PomodoroTimer.Tests
{
	public class PomodoroControllerSpecs
	{
		public abstract class concern : InstanceContextSpecification<PomodoroController>
		{
			protected static IPomodoroView pomodoroView;
			protected static ICountDownTimer countdown;
			protected static IPomodorCommandFactory commandFactory;

			protected override void EstablishContext ()
			{
				base.EstablishContext ();

				pomodoroView = Dependency<IPomodoroView> ();
				countdown = Dependency<ICountDownTimer> ();
				commandFactory = Dependency<IPomodorCommandFactory> ();
			}

			protected override PomodoroController CreateSut ()
			{
				var pomodoroController = new PomodoroController ( countdown, commandFactory );
				pomodoroController.View = pomodoroView;
				return pomodoroController;
			}
		}

		//[Concern ( typeof ( PomodoroController ) )]
		//public abstract class when_timer_changes : concern
		//{
		//    EventHandler<CountDownEventArgs> timerChanged;
		//    Action changeCountDown;
		//    protected override void EstablishContext ()
		//    {
		//        base.EstablishContext ();
		//        timerChanged = Dependency<EventHandler<CountDownEventArgs>> ();
		//        countdown.TimerChanged += timerChanged;
		//    }
		//    protected override void Because ()
		//    {				
		//        changeCountDown = new Action<CountDownEventArgs>
		//            countdown.CountDown = new TimeSpan ( 0, 10, 0 ) );
		//    }


		//    [Observation]
		//    public void it_should_be_signaled ()
		//    {
		//        countdown.WhenToldTo ( changeCountDown )
		//            .Do ( timerChanged.Raise<CountDownEventArgs> ( x=> x.Duration );
		//    }
		//}
	}
}
