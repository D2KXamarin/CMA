using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class DetailsTabbedPage :TabbedPage
	{
		public DetailsTabbedPage ()
		{
			InitializeComponent ();

			ToolMenuList.Clicked += async (object sender, EventArgs e) => {
				await this.Navigation.PushAsync (new Operations ());
			};

			this.CurrentPageChanged += (object sender, EventArgs e) => {
				this.Title = CurrentPage.Title;
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			CurrentPage = this.Children [GlobalVariables.DetailsIndex];
			this.Title = CurrentPage.Title;

		}

		protected override bool OnBackButtonPressed ()
		{
			((MasterDetailPage)Application.Current.MainPage).IsPresented = true;
			return true;
		}
	}
}