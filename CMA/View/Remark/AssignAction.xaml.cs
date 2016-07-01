using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace CMA
{
	public partial class AssignAction : ContentPage
	{
		int GetAction = 0;
		public AssignAction ()
		{
			InitializeComponent ();

			GetAction = 0;
			LoadData ();

			btnSearchPAS.Clicked += async (object sender, EventArgs e) => {
				(BindingContext as VMAssignAction).CurrentMode="EDIT_STAKE";
				GlobalVariables.StakeholderType = "P";
				GetAction = 2;
				await Navigation.PushAsync (new AssignActionStakeholderList ());
			};

			btnSearchSAS.Clicked += async (object sender, EventArgs e) => {
				(BindingContext as VMAssignAction).CurrentMode="EDIT_STAKE";
				GlobalVariables.StakeholderType = "S";
				GetAction = 2;
				await Navigation.PushAsync (new AssignActionStakeholderList ());
			};

			btnSearchIS.Clicked += async (object sender, EventArgs e) => {
				(BindingContext as VMAssignAction).CurrentMode="EDIT_STAKE";
				GlobalVariables.StakeholderType = "I";
				GetAction = 2;
				await Navigation.PushAsync (new AssignActionStakeholderList ());
			};

			btnNext.Clicked += (object sender, EventArgs e) => {
				(BindingContext as VMAssignAction).CurrentIndex++;
				VMAssignAction vm = BindingContext as VMAssignAction;
				vm.LoadAssignActionStakeholderList ();
				LoadAssignAction ();
			};

			btnPrev.Clicked += (object sender, EventArgs e) => {
				(BindingContext as VMAssignAction).CurrentIndex--;
				VMAssignAction vm = BindingContext as VMAssignAction;
				vm.LoadAssignActionStakeholderList ();
				LoadAssignAction ();
			};

			btnAddMore.Clicked += (object sender, EventArgs e) => {

				VMAssignAction vm = BindingContext as VMAssignAction;
				vm.ClearAll ();
				vm.ChangeControlsState (true);
				this.PrimaryActionStakeholderPicker.Items.Clear ();
				this.SecondaryActionStakeholderPicker.Items.Clear ();
				this.InfoActionStakeholderPicker.Items.Clear ();
				GlobalVariables.AssignActionID++;


			};

			btnCancel.Clicked += (object sender, EventArgs e) => {
				VMAssignAction vm = BindingContext as VMAssignAction;
				if (vm.CurrentMode=="ADD") {
					this.txtAction.Text = "";
					this.dtpActionDate.Date = DateTime.Now;
					this.PrimaryActionStakeholderPicker.Items.Clear ();
					this.SecondaryActionStakeholderPicker.Items.Clear ();
					this.InfoActionStakeholderPicker.Items.Clear ();
				}
			};
		}

		public async Task LoadData ()
		{
			VMAssignAction vm = BindingContext as VMAssignAction;
			await vm.LoadAssignActionStakeholderList ();
			LoadAssignAction ();

		}

		void LoadAssignAction ()
		{
			try {
				VMAssignAction vm = BindingContext as VMAssignAction;
				vm.LoadAssignAction ();

				if (vm != null) {
					this.PrimaryActionStakeholderPicker.Items.Clear ();
					vm.strPriStakeholder = "";
					if (vm.PPriActionStakeholder != null) {
						foreach (var currentValue in vm.PPriActionStakeholder) {
							PrimaryActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strPriStakeholder += "," + currentValue.UserLoginID;
						}
						PrimaryActionStakeholderPicker.SelectedIndex = 0;

						vm.strPriStakeholder = vm.strPriStakeholder.Remove (0, 1);
					}

					this.SecondaryActionStakeholderPicker.Items.Clear ();
					vm.strSecStakeholder = "";
					if (vm.PSecActionStakeholder != null) {
						foreach (var currentValue in vm.PSecActionStakeholder) {
							SecondaryActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strSecStakeholder += "," + currentValue.UserLoginID;
						}
						SecondaryActionStakeholderPicker.SelectedIndex = 0;
						vm.strSecStakeholder = vm.strSecStakeholder.Remove (0, 1);
					}

					this.InfoActionStakeholderPicker.Items.Clear ();
					vm.strInfoStakeholder = "";
					if (vm.PInfoStakeholder != null) {

						foreach (var currentValue in vm.PInfoStakeholder) {
							InfoActionStakeholderPicker.Items.Add (currentValue.UserName);
							vm.strInfoStakeholder += "," + currentValue.UserLoginID;
						}
						InfoActionStakeholderPicker.SelectedIndex = 0;	
						vm.strInfoStakeholder = vm.strInfoStakeholder.Remove (0, 1);
					}

				}
			} catch {
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if(GetAction == 2)
				LoadAssignAction ();

			MessagingCenter.Subscribe<VMAssignAction> (this, Strings.AssignActionInsertUpdateSuccess, async (VMAssignAction sender) => {
				await DisplayAlert ("", "Saved Successfully", "OK");	
			});


			MessagingCenter.Subscribe<VMAssignAction> (this, Strings.Display_Message, async (VMAssignAction sender) => {
				await DisplayAlert ("", GlobalVariables.DisplayMessage, "OK");	
			});
		}


		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMAssignAction> (this, Strings.AssignActionInsertUpdateSuccess);
			MessagingCenter.Unsubscribe<VMAssignAction> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

