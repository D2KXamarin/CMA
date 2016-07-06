using System;

using Xamarin.Forms;

namespace CMA
{
	public class LocalToServer : ContentPage
	{
		public LocalToServer ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


