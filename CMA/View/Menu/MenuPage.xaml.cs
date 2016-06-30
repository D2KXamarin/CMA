using System;
using System.Collections.Generic;

using Xamarin.Forms;


namespace CMA
{
	public partial class MenuPage : MasterDetailPage
	{
		public MenuPage ()
		{
			InitializeComponent ();


			MasterPage.ListViewMainMenu.ItemSelected += OnItemSelected;


			if (Device.OS == TargetPlatform.Android) 
			{
				MasterPage.Icon = "menu.png";
			}
		}
		private	async void Logout()
		{
			var result = await this.DisplayAlert("Alert!", "Do you want to logout?", "Yes", "No");
			if (result)
			{
				Application.Current.MainPage = new HomePage ();
			}
		}

		void  OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ListView LV = (ListView)sender;


			var item = e.SelectedItem as MasterPageModel;

			if(item!=null)
			{

				if (item.Title.ToUpper () == "Logout".ToUpper ()) {
					//MasterPage.ListViewMainMenu.SelectedItem = null;
					IsPresented = false;
					Logout ();

				} else {

					if (item.MenuGroup.ToUpper() == "DETAILS") {
						GlobalVariables.DetailsIndex = item.Index; 
					}
					Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
					IsPresented = false;

				}

				LV.SelectedItem = null;
			}

		}

	}
}


