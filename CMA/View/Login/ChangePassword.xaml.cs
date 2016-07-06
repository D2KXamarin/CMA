using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMA
{
	public partial class ChangePassword : ContentPage
	{
		public ChangePassword()
		{
			InitializeComponent();
		}
		protected override void OnAppearing ()
		{
			MessagingCenter.Subscribe<VMChangePassword> (this, Strings.ChangePassword_Success, async (VMChangePassword sender) => {
				{
					await DisplayAlert("Success",GlobalVariables.DisplayMessage,"OK");
				}
			});

			MessagingCenter.Subscribe<VMChangePassword> (this, Strings.ChangePassword__FAILURE, async (VMChangePassword sender) => {
				{
					await DisplayAlert ("Error",Strings.ChangePassword__FAILURE, "OK");
				}
			});
			MessagingCenter.Subscribe<VMChangePassword> (this, Strings.Display_Message, async (VMChangePassword sender) => {
				{
					await DisplayAlert ("Alert!",GlobalVariables.DisplayMessage, "OK");
				}
			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMChangePassword> (this, Strings.ChangePassword_Success);
			MessagingCenter.Unsubscribe<VMChangePassword> (this, Strings.ChangePassword__FAILURE);
			MessagingCenter.Unsubscribe<VMChangePassword> (this, Strings.Display_Message);
		}

		protected override bool OnBackButtonPressed ()
		{
			((MasterDetailPage)Application.Current.MainPage).IsPresented = true;
			return true;
		}
	}
}
