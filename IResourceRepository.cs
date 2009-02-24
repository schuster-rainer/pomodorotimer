using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using PomodoroTimer.Properties;

namespace PomodoroTimer
{
	public interface IResourceRepository
	{
		T GetEmbeddedResourceByName<T> ( string resourceName );
	}
}
