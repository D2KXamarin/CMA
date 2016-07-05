using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class PropertyDetails : ContentPage
	{
		public PropertyDetails (int SecurityEntityId)
		{
			InitializeComponent ();
			VMPropertyDetails vm = BindingContext as VMPropertyDetails;
			vm.SecurityEntityId = SecurityEntityId;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			VMPropertyDetails vm = BindingContext as VMPropertyDetails;
			vm.LoadSecurityPropertyDetail ();
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

		}
	}
}

