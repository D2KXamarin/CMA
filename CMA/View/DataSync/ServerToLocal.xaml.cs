using System;
using System.Collections.Generic;
using XLabs.Forms.Controls;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace CMA
{
	public partial class ServerToLocal : ContentPage
	{
		int SelectedCnt = 0;

		public ServerToLocal ()
		{
			InitializeComponent ();



			btnSearchBranch.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PushAsync (new BranchList ());
			};


			btnOk.Clicked += async (object sender, EventArgs e) => {
				if (!GlobalVariables.IsOffline) {
					VMServerToLocal vm = BindingContext as VMServerToLocal;
					List<CustomerListDataSyncModel> CustomerListDataSyncModel =
						(from customer in vm.PCustomerList
							where customer.IsCheched == true
							select customer).ToList ();
					
					ServerToLocalDataSync serverToLocalDataSync = new ServerToLocalDataSync ();

					bool result = await serverToLocalDataSync.Process (CustomerListDataSyncModel);
					if (result)
					{
						GlobalVariables.IsOffline = true;
						await DisplayAlert ("Success", "Process Completed Successfully", "OK");
					}
					else
						await DisplayAlert ("Failure", "Error... Please try again", "OK");
				} else
					await DisplayAlert ("", "Data available in Offline", "OK");
			};
		}

		public async Task OnLoad ()
		{
			try {
				
				VMServerToLocal vm = BindingContext as VMServerToLocal;
				vm.PCustomerList.Clear ();
				SelectedCnt = 0;
				string CurrentBranch = GlobalVariables.BranchCode + "-" + GlobalVariables.BranchName;
				if (!string.IsNullOrEmpty (GlobalVariables.BranchCode.Trim ()) && CurrentBranch != vm.PBranch) {
					vm.PBranch = CurrentBranch;
					await vm.LoadCustomerList ();
				}
				vm = null;
				
			} catch {
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			OnLoad ();
		}
	}
}