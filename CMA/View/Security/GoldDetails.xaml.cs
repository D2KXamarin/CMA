using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class GoldDetails : ContentPage
	{
		public GoldDetails (int SecurityEntityId)
		{
			InitializeComponent ();
			VMGoldDetals vm = BindingContext as VMGoldDetals;
			vm.SecurityEntityId = SecurityEntityId;
		}
		protected override void OnAppearing ()
		{
			VMGoldDetals vm = BindingContext as VMGoldDetals;
			vm.LoadGoldDetails ();

			MessagingCenter.Subscribe<VMGoldDetals> (this, Strings.GoldDetails_Success, async (VMGoldDetals sender) => {
				{
					await DisplayAlert("Success",GlobalVariables.DisplayMessage,"OK");
					//					await	Navigation.PopAsync();
				}
			});

			MessagingCenter.Subscribe<VMGoldDetals> (this, Strings.GoldDetails__FAILURE, async (VMGoldDetals sender) => {
				{
					await DisplayAlert ("Error",Strings.ActionEvent__FAILURE, "OK");
				}
			});
			MessagingCenter.Subscribe<VMGoldDetals> (this, Strings.Display_Message, async (VMGoldDetals sender) => {
				{
					await DisplayAlert ("Alert!",GlobalVariables.DisplayMessage, "OK");
				}
			});
		}
		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMGoldDetals> (this, Strings.GoldDetails_Success);
			MessagingCenter.Unsubscribe<VMGoldDetals> (this, Strings.GoldDetails__FAILURE);
			MessagingCenter.Unsubscribe<VMGoldDetals> (this, Strings.Display_Message);


		}
		}
	}


