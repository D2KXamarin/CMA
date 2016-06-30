using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class ActionEventDiaryList : ContentPage
	{
		public ActionEventDiaryList ()
		{
			InitializeComponent ();

			RemarkDetailsList.ItemTapped += (object sender, ItemTappedEventArgs e) => 
			{
				ActionEventDiaryListModel C = (ActionEventDiaryListModel)RemarkDetailsList.SelectedItem;
				GlobalVariables.RemarkID= C.EventID;
				this.Navigation.PushAsync (new ActionEventDiary ());
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			GlobalVariables.DetailsIndex = 2;
		}
	}
}

