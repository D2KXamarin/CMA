using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;

namespace CMA
{
	public partial class CustomerList : ContentPage
	{
		public CustomerList ()
		{
			 
			InitializeComponent ();
			VMCustomerList vm=BindingContext as VMCustomerList;
			vm.PSearch = "";

			OCustomerList.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				vm.SaveCustomer();
			};

		}

		private void LoadOfflineCustomerList()
		{
			VMCustomerList vm=BindingContext as VMCustomerList;

			List<CustomerListModel> listCustomer = SQLiteDatabase.Instance.GetDataSyncCustomerList ();

			if (listCustomer != null)
				vm.PCustomerList = new System.Collections.ObjectModel.ObservableCollection<CustomerListModel> (listCustomer);

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			LoadOfflineCustomerList ();
			MessagingCenter.Subscribe(this, Strings.Display_Message, async (VMCustomerList sender) => {
				await DisplayAlert (null, GlobalVariables.DisplayMessage, "Ok");

			});

			MessagingCenter.Subscribe(this, Strings.CustomerList_Success, async (VMCustomerList sender) => {
				//await DisplayAlert (null, GlobalVariables.DisplayMessage, "Ok");
				await Navigation.PopAsync();
			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMCustomerList> (this, Strings.Display_Message);
			MessagingCenter.Unsubscribe<VMCustomerList> (this, Strings.CustomerList_Success);
			base.OnDisappearing ();
		}
	}
}

