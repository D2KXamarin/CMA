using System;
using System.Collections.Generic;

using Xamarin.Forms;


namespace CMA
{
	public partial class Operations : ContentPage
	{
		public Operations ()
		{
			InitializeComponent ();


			btnSearchBranch.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PushAsync (new BranchList ());
			};

			btnCustomerList.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PushAsync (new CustomerList ());
			};

			btnACID.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PushAsync (new AccountList ());
			};

			btnOk.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PopAsync ();
			};

			btnCancel.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PopAsync ();
			};

			btnACIDClear.Clicked += (object sender, EventArgs e) => {
				VMOperations vm = BindingContext as VMOperations;
				vm.PAccountNo = "";
				GlobalVariables.AccountID = null;
				GlobalVariables.AccountEntityID = "0";
				vm = null;
			};

			btnCancel.Clicked += (object sender, EventArgs e) => {
				VMOperations vm = BindingContext as VMOperations;
				vm.PAccountNo = "";

				GlobalVariables.AccountID = null;

				vm.PBranch = "";

				GlobalVariables.BranchCode = null;
				GlobalVariables.BranchName = "";

				vm.PCustomer = "";

				GlobalVariables.CustomerID = null;
				GlobalVariables.CustomerName = "";

				vm = null;
			};

		}


		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			try {

				VMOperations vm = BindingContext as VMOperations;

				if (GlobalVariables.IsOffline) {
					vm.IsVisibleBranchBtn = false;

					List<DataSyncBranchModel> listDataSyncBranchModel = SQLiteDatabase.Instance.GetDataSyncBranch ();
					if (listDataSyncBranchModel != null) {
						GlobalVariables.BranchCode = listDataSyncBranchModel [0].BranchCode;
						GlobalVariables.BranchName = listDataSyncBranchModel [0].BranchName;
					}

					listDataSyncBranchModel=null;
				}


				string CurrentBranch = GlobalVariables.BranchCode + "-" + GlobalVariables.BranchName;
				if (!string.IsNullOrEmpty (GlobalVariables.BranchCode.Trim ()) && CurrentBranch != vm.PBranch) {
					vm.PBranch = CurrentBranch;

					GlobalVariables.CustomerEntityID = string.Empty;
					GlobalVariables.CustomerID = string.Empty;
					GlobalVariables.CustomerName = string.Empty;
					vm.PCustomer = string.Empty;

					GlobalVariables.AccountEntityID = "0";
					vm.PAccountNo = string.Empty;
				} else if (CurrentBranch == vm.PBranch) {

					string CurrentCustomer = GlobalVariables.CustomerID + "-" + GlobalVariables.CustomerName;

					if (!string.IsNullOrEmpty (GlobalVariables.CustomerEntityID.Trim ()) && CurrentCustomer != vm.PCustomer) {
						vm.PCustomer = CurrentCustomer;
						GlobalVariables.AccountEntityID = "0";
						vm.PAccountNo = string.Empty;		
					} else if (CurrentCustomer == vm.PCustomer) {
						vm.PAccountNo = GlobalVariables.AccountID;
					}
				}

				vm = null;
			} catch {
			}

		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
		}

		protected override bool OnBackButtonPressed ()
		{
			Application.Current.MainPage = new MenuPage ();
			return true;
		}


	}
}

