using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class SecurityDetails : ContentPage
	{
		public SecurityDetails ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<VMSecurityDetails> (this, Strings.Display_Message, async (VMSecurityDetails sender) => {
				await DisplayAlert (null, GlobalVariables.DisplayMessage, "Ok");

			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMSecurityDetails> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

