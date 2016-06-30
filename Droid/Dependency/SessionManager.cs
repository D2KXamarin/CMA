using System;
using Xamarin.Forms;
using Android.Content;
using Android.Preferences;
using CMA.Droid;
using CMA;

[assembly:Dependency(typeof(SessionManager))]
namespace CMA.Droid
{
	/// <summary>
	/// Session manager for Android platform
	/// </summary>
	public class SessionManager : ISessionManager
	{
		public SessionManager ()
		{
		}

		#region ISessionManager implementation


		public void Exit()
		{
			Android.OS.Process.KillProcess (Android.OS.Process.MyPid());
		}
		#endregion
	}
}

