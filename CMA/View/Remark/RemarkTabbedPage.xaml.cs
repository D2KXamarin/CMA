using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class RemarkTabbedPage : TabbedPage
	{
		int OnLoadStatus = 0;
		public RemarkTabbedPage ()
		{
			InitializeComponent ();


			ToolMenuList.Clicked += (object sender, EventArgs e) => {
				//Navigation.PopToRootAsync ();
				Navigation.PushAsync (new Operations ());
				OnLoadStatus=0;
			};

			this.CurrentPageChanged += (object sender, EventArgs e) => {
				this.Title = CurrentPage.Title;
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (OnLoadStatus == 0) {
				int currentIndex = GlobalVariables.DetailsIndex;
				try {
					Children.Clear();
				} catch (Exception ex) {
					
				}


				Children.Add (new InstitutionalMemoryDiary (){ Title = "Institutional Memory Diary" });
				Children.Add (new ViewPreviousRemark (){ Title = "View Remark" });
				Children.Add (new ActionEventDiaryList (){ Title = "Action Event List" });

				GlobalVariables.DetailsIndex = currentIndex;
				CurrentPage = this.Children [GlobalVariables.DetailsIndex];
				this.Title = CurrentPage.Title;
			}
			OnLoadStatus = 1;
		}
	}
}


