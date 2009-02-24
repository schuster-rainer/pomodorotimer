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
	public static class EventTestExtensions
	{
		public static void WasRaised<T> ( this EventHandler<T> eventHandler )
			where T : EventArgs
		{
			eventHandler.WhenToldTo ( handler => handler.Invoke ( null, null ) )
				.IgnoreArguments ();
		}

		public static void WasRaised ( this EventHandler eventHandler )
		{
			eventHandler.WhenToldTo ( handler => handler.Invoke ( null, null ) )
				.IgnoreArguments ();
		}

		public static bool wasRaisedAfter ( this EventWaitHandle eventWaitHandle, int milliseconds )
		{
			return eventWaitHandle.WaitOne ( milliseconds );
		}

		public static void ShouldNotBeRaisedAfter ( this EventWaitHandle eventWaitHandle, int milliseconds )
		{
			eventWaitHandle.WaitOne ( milliseconds ).ShouldBeFalse ();
		}
	}
}
