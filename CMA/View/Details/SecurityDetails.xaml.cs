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
			lvSecurityDetails.ItemTapped += (object sender, ItemTappedEventArgs e) => 
			{
				SecurityDetailsModel C = (SecurityDetailsModel)lvSecurityDetails.SelectedItem;
				if(C.SecurityType=="GOLD"){
					this.Navigation.PushAsync (new GoldDetails (C.SecurityEntityId));
				}
				else if(C.SecurityType=="VEHICLE"){
					this.Navigation.PushAsync (new Vehicle_Details (C.SecurityEntityId));
				}
				else if(C.SecurityType=="SHARES"){
					this.Navigation.PushAsync (new ShareDetails (C.SecurityEntityId));
				}
				else if(C.SecurityType=="PROPERTY"){
					this.Navigation.PushAsync (new PropertyDetails (C.SecurityEntityId));
				}

			};
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

