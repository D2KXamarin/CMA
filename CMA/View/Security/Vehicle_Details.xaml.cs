using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class Vehicle_Details : ContentPage
	{
		public Vehicle_Details (int SecurityEntityId)
		{
			InitializeComponent ();
			VMVehicleDetails vm = BindingContext as VMVehicleDetails;
			vm.SecurityEntityId = SecurityEntityId;
		}
		protected override void OnAppearing ()
		{
			VMVehicleDetails vm = BindingContext as VMVehicleDetails;
			vm.LoadVehicelDetails ();
		}
	}
}

