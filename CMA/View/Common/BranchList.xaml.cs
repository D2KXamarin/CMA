using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class BranchList : ContentPage
	{
		public BranchList ()
		{
			InitializeComponent ();

			VMBranchList vm = BindingContext as VMBranchList;
			vm.PSearchValue = "";

			lvBranchList.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				if (vm.PSelectedBranch != null) {
					GlobalVariables.BranchCode = vm.PSelectedBranch.BranchCode;
					GlobalVariables.BranchName = vm.PSelectedBranch.BranchName;
					Navigation.PopAsync ();
				} else
					DisplayAlert ("", "Please Select Branch", "Ok");
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<VMBranchList> (this, Strings.Display_Message, async (VMBranchList sender) => {
				await DisplayAlert ("", GlobalVariables.DisplayMessage, "OK");

			});
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			MessagingCenter.Unsubscribe<VMBranchList> (this, Strings.Display_Message);
		}

	}
}

