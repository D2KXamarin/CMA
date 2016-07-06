using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class Vehicle_Details : ContentPage
	{
		int loadVehicleDetails = 0;
		public Vehicle_Details (int SecurityEntityId)
		{
			InitializeComponent ();
			VMVehicleDetails vm = BindingContext as VMVehicleDetails;



			btnInsuranceCO.Clicked+= async (object sender, EventArgs e) => {
				loadVehicleDetails=2;
				Navigation.PushAsync(new InsuranceCompany());
			};
			//			VMVehicleDetails vma = BindingContext as VMVehicleDetails;
			vm.SecurityEntityId = SecurityEntityId;
		}
		protected override void OnAppearing ()
		{
			VMVehicleDetails vm = BindingContext as VMVehicleDetails;
			if (loadVehicleDetails == 0) {
				vm.LoadVehicelDetails ();
			} else {
				vm.PInsuranceCompany = GlobalVariables.CompanyName;
			}


			MessagingCenter.Subscribe<VMVehicleDetails> (this, Strings.VehicleDetails_Success, async (VMVehicleDetails sender) => {
				{
					await DisplayAlert("Success",GlobalVariables.DisplayMessage,"OK");
					//					await	Navigation.PopAsync();
				}
			});

			MessagingCenter.Subscribe<VMVehicleDetails> (this, Strings.VehicleDetails__FAILURE, async (VMVehicleDetails sender) => {
				{
					await DisplayAlert ("Error",Strings.VehicleDetails__FAILURE, "OK");
				}
			});
			MessagingCenter.Subscribe<VMVehicleDetails> (this, Strings.Display_Message, async (VMVehicleDetails sender) => {
				{
					await DisplayAlert ("Alert!",GlobalVariables.DisplayMessage, "OK");
				}
			});
		}
		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMVehicleDetails> (this, Strings.VehicleDetails_Success);
			MessagingCenter.Unsubscribe<VMVehicleDetails> (this, Strings.VehicleDetails__FAILURE);
			MessagingCenter.Unsubscribe<VMVehicleDetails> (this, Strings.Display_Message);



		}
	}
}

