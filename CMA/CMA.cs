using System;

using Xamarin.Forms;

namespace CMA
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage(new Login());

			GlobalVariables.IsOffline = SQLiteDatabase.Instance.IsDataInLocal ();
//			MainPage = new MenuPage ();
//			MainPage = new NavigationPage(new Vehicle_Details());

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

