using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class StakeholderList : ContentPage
	{
		public StakeholderList ()
		{
			InitializeComponent ();

			btnOk.Clicked += async (object sender, EventArgs e) => {
				await	Navigation.PopAsync ();
			};
		}
	}
}

