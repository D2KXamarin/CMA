using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CMA
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListViewMainMenu { get { return listView; } }
		public MasterPage ()
		{
			InitializeComponent ();
		}
	}
}

