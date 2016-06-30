using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class ViewPreviousRemark : ContentPage
	{
		int OnLoadFlag = 0;
		public ViewPreviousRemark ()
		{
			InitializeComponent ();

			lvPreviousRemarks.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				GlobalVariables.RemarkID = ((PreviousRemarkListModel)lvPreviousRemarks.SelectedItem).RemarkID;
				OnLoadFlag = 1;
				Navigation.PushAsync(new AssignAction()); 
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			GlobalVariables.DetailsIndex = 1;

			if (OnLoadFlag == 0) {
				VMViewPreviousRemark vm = BindingContext as VMViewPreviousRemark;
				vm.LoadPreviousRemarks ();
			}

			OnLoadFlag = 0;
		}
	}
}

