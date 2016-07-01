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

			ActionLoadData ();
			ValidateDate ();
			btnCancle.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PopAsync ();	
			};
		}
		public void ValidateDate(){
			DtPCommencementDate.SetValue (DatePicker.MaximumDateProperty, DateTime.Now.AddDays (1));
			DtPCommencementDate.SetValue (DatePicker.MinimumDateProperty, DateTime.Now.AddDays (-2));
			DtPClosureDate.SetValue (DatePicker.MaximumDateProperty, DateTime.Now.AddDays (1));
			DtPClosureDate.SetValue (DatePicker.MinimumDateProperty, DateTime.Now.AddDays (-2));

	
		}


	public async Task ActionLoadData()
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
					await	Navigation.PopAsync();
				}
			});

			MessagingCenter.Subscribe<VMActionEventDiary> (this, Strings.ActionEvent__FAILURE, async (VMActionEventDiary sender) => {
				{
					await DisplayAlert ("Error", GlobalVariables.DisplayMessage, "OK");
				}
			});
			MessagingCenter.Subscribe<VMActionEventDiary> (this, Strings.Display_Message, async (VMActionEventDiary sender) => {
				{
					await DisplayAlert ("Alert!",GlobalVariables.DisplayMessage, "OK");
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

