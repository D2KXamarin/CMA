using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMA
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            var tgr = new TapGestureRecognizer();
            forgetPasswordLabel.GestureRecognizers.Add(tgr);

            tgr.Tapped += (object sender, EventArgs e) => {
               // Application.Current.MainPage = new ForgetPassword();
                DisplayAlert("","Click","OK");



            };
        }
    }
}
