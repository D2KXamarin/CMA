using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace CMA
{
	public partial class CustomerReallocation: ContentPage
	{
		int GetAction = 0;
		public CustomerReallocation ()
		{
			InitializeComponent ();
			//			
			GetAction=0;
			//			LoadValidData ();

			//			ToolMenuList.Clicked += (object sender, EventArgs e) => {
			//				
			//				Navigation.PushAsync (new Operations ());
			//			
			//			};

			btnSearchPAS.Clicked += async (object sender, EventArgs e) => {
				GlobalVariables.StakeholderType = "P";
				GetAction=2;
				await Navigation.PushAsync (new StakeholderList ());

			};
			btnSearchSAS.Clicked += async (object sender, EventArgs e) => {
				GlobalVariables.StakeholderType = "S";
				GetAction=2;
				await Navigation.PushAsync (new StakeholderList ());
			};
			btnSearchIS.Clicked += async (object sender, EventArgs e) => {
				GlobalVariables.StakeholderType = "I";
				GetAction=2;
				await Navigation.PushAsync (new StakeholderList ());
			};
			btnCancel.Clicked += async (object sender, EventArgs e) => {
				await DisplayAlert("","Do You Want To Cancel","Ok");

				GetAction=0;
				LoadData();
			};
		}
		public async Task LoadValidData(){
			if (GlobalVariables.CustomerEntityID != null) {
				LoadData ();
			} else {
				await DisplayAlert("","Please Select Branch & Customer","Ok");
				GetAction = 0;
				Navigation.PushAsync(new Operations ());


			}
		}

		public async Task LoadData ()
		{
			VMCustomerReallocation vm = BindingContext as VMCustomerReallocation;
			await vm.LoadStakeholderList ();
			LoadAssignAction ();

		}
		void LoadAssignAction ()
		{
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
						if(vm.strPriStakeholder!=""){
							vm.strPriStakeholder = vm.strPriStakeholder.Remove (0, 1);
						}
					}

					this.SecondaryActionStakeholderPicker.Items.Clear ();
					vm.strSecStakeholder = "";
					if (vm.PSecActionStakeholder != null) {
						foreach (var currentValue in vm.PSecActionStakeholder) {
							SecondaryActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strSecStakeholder += "," + currentValue.UserLoginId;
						}
						SecondaryActionStakeholderPicker.SelectedIndex = 0;
						if(vm.strSecStakeholder!=""){
							vm.strSecStakeholder = vm.strSecStakeholder.Remove (0, 1);
						}
					}

					this.InfoActionStakeholderPicker.Items.Clear ();
					vm.strInfoStakeholder = "";
					if (vm.PInfoStakeholder != null) {

						foreach (var currentValue in vm.PInfoStakeholder) {
							InfoActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strInfoStakeholder += "," + currentValue.UserLoginId;
						}
						InfoActionStakeholderPicker.SelectedIndex = 0;	
						if(vm.strInfoStakeholder!=""){

							vm.strInfoStakeholder = vm.strInfoStakeholder.Remove (0, 1);

						}
					}

				}
			} catch {
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			if (GetAction == 2) {
				LoadAssignAction ();

			} else {
				LoadValidData ();
			}



			MessagingCenter.Subscribe<VMCustomerReallocation> (this, Strings.CustomerReallocationSuccess, async (VMCustomerReallocation sender) => {
				await DisplayAlert ("", "Saved Successfully", "OK");	
			});
			MessagingCenter.Subscribe<VMCustomerReallocation> (this, Strings.Display_Message, async (VMCustomerReallocation sender) => {
				{
					await DisplayAlert ("Alert", GlobalVariables.DisplayMessage, "OK");
				}
			});

		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMCustomerReallocation> (this, Strings.CustomerReallocationSuccess);
			base.OnDisappearing ();
		}

	}
}

