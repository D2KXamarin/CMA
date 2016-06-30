using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class AccountList : ContentPage
	{
		public AccountList ()
		{
			InitializeComponent ();
			VMAccountList vm = BindingContext as VMAccountList;
			vm.PSearchValue = "";

			lvAccountList.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				if (vm.PSelectedAccount != null) {
					GlobalVariables.AccountEntityID = vm.PSelectedAccount.AccountEntityID;
					GlobalVariables.AccountID = vm.PSelectedAccount.AccountID;

					Navigation.PopAsync ();
				} else
					DisplayAlert ("", "Please Select Account", "Ok");
			};
		}
	}
}

