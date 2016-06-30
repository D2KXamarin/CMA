using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CMA
{
	public partial class Login : ContentPage
	{
		public Login()
		{

			InitializeComponent();
			Clearall ();
			var tgr = new TapGestureRecognizer();
			forgetPasswordLabel.GestureRecognizers.Add(tgr);

			tgr.Tapped += (object sender, EventArgs e) => {
				// Application.Current.MainPage = new ForgetPassword();
				DisplayAlert("","Click","OK");



			};

			btnLogin.Clicked += async (object sender, EventArgs e) => {

				VMLogin VMLogin = BindingContext as VMLogin;



				var authenticationResult = await APIRequest.Instance.Login (new LoginRequestModel {
					UserLoginId = VMLogin.PUserID,
					Password = VMLogin.PPassord
				});
				try{
					if (authenticationResult != null) {

						JArray j = JArray.Parse (authenticationResult);

						LoginResponseModel loginResponseModel = null;
						//int ResultCode = JsonResult.StatusCode;

						int flag = 0;
						foreach (JObject o in j.Children<JObject>()) {

							foreach (JProperty p in o.Properties()) {
								if (p.Name.ToUpper () == "STATUSCODE") {
									if (p.Value.ToString () == "1")
										loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel> (o.ToString ());
									else
										loginResponseModel = null;		

									flag = 1;
									break;
								}
							}

							if (flag == 1 && loginResponseModel != null) {
								GlobalVariables.UserLoginID = VMLogin.PUserID;
								GlobalVariables.UserLocationCode=loginResponseModel.UserLocationCode;
								GlobalVariables.UserLocationName=loginResponseModel.UserLocationName;
								GlobalVariables.UserLocation=loginResponseModel.UserLocation;
								Application.Current.MainPage = new MenuPage ();
								//Navigation.PushAsync(new MenuPage());
							} else {
								await DisplayAlert (null, "Invalid Login", "OK");
							}
						}
					}
				}catch{
					DisplayAlert("","Entered ID or Password is Incorrect","Ok");
				}

			
			};

		}
		public void Clearall(){
			GlobalVariables.UserLoginID = null;
			GlobalVariables.BranchName = string.Empty;
			GlobalVariables.CustomerID= null;
			GlobalVariables.BranchCode=null;
			GlobalVariables.CustomerEntityID= null;
			GlobalVariables.AccountEntityID= null;
			GlobalVariables.AccountID = null;;
			GlobalVariables.CustomerName   = string.Empty;
			GlobalVariables.RemarkID = "0";
			GlobalVariables.AssignActionID= 0;
			txtUserName.Text = "";
			txtPassword.Text = "";
		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			MessagingCenter.Subscribe<VMLogin> (this, Strings.Display_Message, async (VMLogin sender) => {
				await DisplayAlert ("", GlobalVariables.DisplayMessage, "OK");
			});
		}
		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<VMLogin> (this, Strings.Display_Message);

			base.OnDisappearing ();
		}
		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async() => {
				var result = await this.DisplayAlert("Alert!", "Do you want to exit?", "Yes", "No");
				if (result)
				{


					var sessionManager = DependencyService.Get<ISessionManager>();

					sessionManager.Exit();

				}
			});

			return true;
		}
	}
}
