using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace CMA
{
	public partial class InstitutionalMemoryDiary : ContentPage
	{
		int OnLoadFlag = 0; 
		public InstitutionalMemoryDiary ()
		{
			InitializeComponent ();

			//LoadData ();
		}


		public async Task LoadData()
		{
			VMInstitutionalMemoryDiary vm = BindingContext as VMInstitutionalMemoryDiary;
			if (vm != null) {
				await vm.LoadDefaultStatus ();

				if (vm.PDefaultStatusList != null) {
					pickerDefaultStatus.Items.Clear ();
					pickerDefaultStatus.Items.Add ("-Select-");
					foreach (DefaultStatusModel defaultStatusModel in vm.PDefaultStatusList) {
						pickerDefaultStatus.Items.Add (defaultStatusModel.DefaultStatusName);
					}
					pickerDefaultStatus.SelectedIndex = 0;
				}

				vm.LoadAccountList ();

				if (vm.PAccountList != null) {
					pickerAccountID.Items.Clear ();
					pickerAccountID.Items.Add ("-Select-");
					foreach (AccountDetailModel accountDetailModel in vm.PAccountList) {
						pickerAccountID.Items.Add (accountDetailModel.AccountID);
					}
					pickerAccountID.SelectedIndex = 0;
				}
			}
		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			GlobalVariables.DetailsIndex = 0;

			if (OnLoadFlag == 0) {
				LoadData ();
			}

			OnLoadFlag = 0;

			MessagingCenter.Subscribe<VMInstitutionalMemoryDiary> (this, Strings.RemarkSaveSuccess, async (VMInstitutionalMemoryDiary sender) => {
				bool answer = await DisplayAlert (null, "Do you want to assign an action?", "Yes", "No");
				if (answer) {
					///Navigate to Assign Action
					await	Navigation.PushAsync (new AssignAction ());
				}
				(BindingContext as VMInstitutionalMemoryDiary).ClearAll ();
			});

			MessagingCenter.Subscribe<VMInstitutionalMemoryDiary> (this, Strings.Display_Message, async (VMInstitutionalMemoryDiary sender) => {
				await DisplayAlert (null, GlobalVariables.DisplayMessage, "OK");
			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMInstitutionalMemoryDiary> (this, Strings.RemarkSaveSuccess);
			MessagingCenter.Unsubscribe<VMInstitutionalMemoryDiary> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

