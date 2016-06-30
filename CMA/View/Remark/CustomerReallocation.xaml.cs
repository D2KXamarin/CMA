using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace CMA
{
	public partial class CustomerReallocation: ContentPage
	{
		public CustomerReallocation ()
		{
			InitializeComponent ();
			LoadData ();
			btnSearchPAS.Clicked += async (object sender, EventArgs e) => {
				GlobalVariables.StakeholderType = "P";
				await Navigation.PushAsync (new StakeholderList ());
			};
			btnSearchSAS.Clicked += async (object sender, EventArgs e) => {
				GlobalVariables.StakeholderType = "S";
				await Navigation.PushAsync (new StakeholderList ());
			};
			btnSearchIS.Clicked += async (object sender, EventArgs e) => {
				GlobalVariables.StakeholderType = "I";
				await Navigation.PushAsync (new StakeholderList ());
			};
		}

		public async Task LoadData ()
		{
			VMCustomerReallocation vm = BindingContext as VMCustomerReallocation;
			await vm.LoadStakeholderList ();

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			try {
				VMCustomerReallocation vm = BindingContext as VMCustomerReallocation;
				vm.AllotedStakeholderList ();

				if (vm != null) {

					this.PrimaryActionStakeholderPicker.Items.Clear ();
					vm.strPriStakeholder = "";
					if (vm.PPriActionStakeholder != null) {
						foreach (var currentValue in vm.PPriActionStakeholder) {
							PrimaryActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strPriStakeholder += "," + currentValue.UserLoginId;
						}
						PrimaryActionStakeholderPicker.SelectedIndex = 0;

						vm.strPriStakeholder = vm.strPriStakeholder.Remove (0, 1);
					}



					this.SecondaryActionStakeholderPicker.Items.Clear ();
					vm.strSecStakeholder = "";
					if (vm.PSecActionStakeholder != null) {
						foreach (var currentValue in vm.PSecActionStakeholder) {
							SecondaryActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strSecStakeholder += "," + currentValue.UserLoginId;
						}
						SecondaryActionStakeholderPicker.SelectedIndex = 0;
						vm.strSecStakeholder = vm.strSecStakeholder.Remove (0, 1);
					}



					this.InfoActionStakeholderPicker.Items.Clear ();
					vm.strInfoStakeholder = "";
					if (vm.PInfoStakeholder != null) {

						foreach (var currentValue in vm.PInfoStakeholder) {
							InfoActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strInfoStakeholder += "," + currentValue.UserLoginId;
						}
						InfoActionStakeholderPicker.SelectedIndex = 0;	
						vm.strInfoStakeholder = vm.strInfoStakeholder.Remove (0, 1);
					}

				}
			} catch {
			}
			MessagingCenter.Subscribe<VMCustomerReallocation> (this, Strings.CustomerReallocationSuccess, async (VMCustomerReallocation sender) => {
				await DisplayAlert ("", "Saved Successfully", "OK");	
			});

		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMCustomerReallocation> (this, Strings.CustomerReallocationSuccess);
			base.OnDisappearing ();
		}

	}
}

