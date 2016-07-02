using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class ShareDetails : ContentPage
	{
		public ShareDetails ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<VMSecurityShareDetail> (this, Strings.Display_Message, async (VMSecurityShareDetail sender) => {
				await DisplayAlert (null, GlobalVariables.DisplayMessage, "Ok");
			});

		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMSecurityShareDetail> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

