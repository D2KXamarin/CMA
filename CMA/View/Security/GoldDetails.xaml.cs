using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class GoldDetails : ContentPage
	{
		public GoldDetails (int SecurityEntityId)
		{
			InitializeComponent ();
			VMGoldDetals vm = BindingContext as VMGoldDetals;
			vm.SecurityEntityId = SecurityEntityId;
		}
		protected override void OnAppearing ()
		{
			VMGoldDetals vm = BindingContext as VMGoldDetals;
			vm.LoadGoldDetails ();
		}
	}
}

