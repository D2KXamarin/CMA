using System;
using System.ComponentModel;

namespace CMA
{
	public class VMCommon :INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this,
					new PropertyChangedEventArgs (propertyName));
			}
		}
		#endregion

		public VMCommon ()
		{
		}
	}
}

