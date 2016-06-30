using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CMA
{
	public partial class ActionEventDiary : ContentPage
	{
		public ActionEventDiary ()
		{
			InitializeComponent ();

			LoadData ();
		}

		public async Task LoadData()
		{
			VMActionEventDiary vm = BindingContext as VMActionEventDiary;
			await vm.LoadActionDiary ();

			foreach(EventStatusModel ESV in vm.StatusMaster)
			{
				pickerStatus.Items.Add(ESV.StatusValue);
			}

			pickerStatus.SelectedIndex = 0;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			MessagingCenter.Subscribe<VMActionEventDiary> (this, Strings.ActionEvent_Success, async (VMActionEventDiary sender) => {
				{
					await DisplayAlert("Success",Strings.ActionEvent_Success,"OK");
				}
			});

			MessagingCenter.Subscribe<VMActionEventDiary> (this, Strings.ActionEvent__FAILURE, async (VMActionEventDiary sender) => {
				{
					await DisplayAlert ("Error", Strings.ActionEvent__FAILURE, "OK");
				}
			});

		}
		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMActionEventDiary> (this, Strings.ActionEvent_Success);
			MessagingCenter.Unsubscribe<VMActionEventDiary> (this, Strings.ActionEvent__FAILURE);
			base.OnDisappearing ();

		}

	}
}

