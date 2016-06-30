using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class LoanDetails : ContentPage
	{
		public LoanDetails ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<VMLoanDetails> (this, Strings.Display_Message, async (VMLoanDetails sender) => {
				await DisplayAlert (null, GlobalVariables.DisplayMessage, "Ok");
			});

		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMLoanDetails> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

