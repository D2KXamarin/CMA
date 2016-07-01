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
			if (GlobalVariables.StakeholderType == "P") {
				Title="Primary Stakeholder";
			}
			else if (GlobalVariables.StakeholderType == "S") {
				Title="Secondary Stakeholder";
			}
			else if (GlobalVariables.StakeholderType == "I") {
				Title="Info Stakeholder";
			}
			btnOk.Clicked += async (object sender, EventArgs e) => {
				await	Navigation.PopAsync ();
			};
			btnCancel.Clicked += async (object sender, EventArgs e) => {
				await	Navigation.PopAsync ();
			};
		}
	}
}

