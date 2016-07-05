using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class ShareDetails : ContentPage
	{
		public ShareDetails (int SecurityEntityId)
		{
			InitializeComponent ();
			VMSecurityShareDetail vm = BindingContext as VMSecurityShareDetail;
			vm.SecurityEntityId = SecurityEntityId;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			VMSecurityShareDetail vm = BindingContext as VMSecurityShareDetail;
			vm.LoadSecurityShareDetail ();

			MessagingCenter.Subscribe<VMSecurityShareDetail> (this, Strings.ShareDetails_Success, async (VMSecurityShareDetail sender) => {
				{
					await DisplayAlert("Success",Strings.ShareDetails_Success,"OK");
					//await	Navigation.PopAsync();
				}
			});

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

