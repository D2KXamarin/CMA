using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class ActionEventDiaryList : ContentPage
	{
		int OnLoadFlag = 0;
		public ActionEventDiaryList ()
		{
			InitializeComponent ();

			RemarkDetailsList.ItemTapped += (object sender, ItemTappedEventArgs e) => 
			{
				ActionEventDiaryListModel C = (ActionEventDiaryListModel)RemarkDetailsList.SelectedItem;
				GlobalVariables.RemarkID= C.EventID;
				OnLoadFlag = 1;
				this.Navigation.PushAsync (new ActionEventDiary ());
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			GlobalVariables.DetailsIndex = 2;
			if (OnLoadFlag == 0 ) {
				VMActionEventDiaryList vm = BindingContext as VMActionEventDiaryList;
				vm.LoadActionEventDiaryList();
			}
			OnLoadFlag = 0;
		}
	}
}

