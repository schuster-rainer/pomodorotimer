using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PomodoroTimer
{
	class Programm
	{		
		[STAThread]
		public static void Main ( string[] args )
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault ( false );

			using ( UniqueClassInstance trayIcon = new UniqueClassInstance( "PomodoroTimer" ) )
			{
				if ( trayIcon.IsFirstInstance )
				{
					RunApplication ();
				}
				else
				{
					ShowApplication ();
				}
			}
		}

		private static void RunApplication ()
		{
			using ( PomodoroNofiyIcon notificationIcon = new PomodoroNofiyIcon () )
			{
				notificationIcon.Visible = true;
				Application.Run ();
			}
		}

		private static void ShowApplication ()
		{
			throw new NotImplementedException ();
		}

	}
}