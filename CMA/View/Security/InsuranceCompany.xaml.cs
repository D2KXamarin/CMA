using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class InsuranceCompany : ContentPage
	{
		public InsuranceCompany ()
		{
			InitializeComponent ();
			VMInsuranceCompany vm = BindingContext as VMInsuranceCompany;
			vm.PSearchValue= "";
			lvCompanyList.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				if (vm.PSelectedCompany != null) {
					GlobalVariables.CompanyName = vm.PSelectedCompany.CompanyName;
					Navigation.PopAsync ();
				} else
					DisplayAlert ("", "Please Select Insurance Company", "Ok");
			};
		}


		protected override void OnAppearing ()
		{
			MessagingCenter.Subscribe<VMInsuranceCompany> (this, Strings.Display_Message, async (VMInsuranceCompany sender) => {
				await DisplayAlert ("", GlobalVariables.DisplayMessage, "OK");

			});
		}
		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMInsuranceCompany> (this, Strings.Display_Message);
		}
	}
}

