using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class AssignActionStakeholderList : ContentPage
	{
		public AssignActionStakeholderList ()
		{
			InitializeComponent ();

			btnCancel.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PopAsync();
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			MessagingCenter.Subscribe<VMAssignActionStakeholderList> (this, Strings.AssignActionStakeholderListSuccess, async (VMAssignActionStakeholderList sender) => {
				await Navigation.PopAsync();
			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMAssignActionStakeholderList> (this, Strings.AssignActionStakeholderListSuccess);
			base.OnDisappearing ();
		}
	}
}

