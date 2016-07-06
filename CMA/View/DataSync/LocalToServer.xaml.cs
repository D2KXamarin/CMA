using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class LocalToServer : ContentPage
	{
		public LocalToServer ()
		{
			InitializeComponent ();

			btnUpload.Clicked += async (object sender, EventArgs e) => {
				LocalToServerDataSync localToServerDataSync = new LocalToServerDataSync ();
				bool result =	await localToServerDataSync.Process ();
				if (result)
					await DisplayAlert ("", "Process Completed Successfully", "OK");
				else
					await DisplayAlert ("", "Error... Please try again", "OK");
			};
		}
	}
}

