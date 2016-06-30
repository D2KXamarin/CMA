using System;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using CMA;

namespace CMA
{
	public class VMSecurityDetails : INotifyPropertyChanged
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

		public VMSecurityDetails ()
		{
			LoadSecurityDetails ();
		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private ObservableCollection<SecurityDetailsModel> _PSecurityDetailsList;

		public ObservableCollection<SecurityDetailsModel> PSecurityDetailsList {
			get { 
				return _PSecurityDetailsList;

			}
			set {
				_PSecurityDetailsList = value;
				OnPropertyChanged ("PSecurityDetailsList");
			}
		}

		public async Task LoadSecurityDetails ()
		{
			ActivityIndicator = true;
			try {
				if (GlobalVariables.IsOffline) {
					PSecurityDetailsList = new ObservableCollection<SecurityDetailsModel> (DataSync.GetDataSyncSecurityDetails (GlobalVariables.CustomerEntityID, GlobalVariables.AccountEntityID));
				} else {
					var result = await APIRequest.Instance.GetSecurityDetailsAPI (new SecurityDetailsRequestModel {
						CustomerEntityID = GlobalVariables.CustomerEntityID,
						AccountEntityID = GlobalVariables.AccountEntityID
					});

					if (result != null) {
						var securityDetailsRequestModel = JsonConvert.DeserializeObject<SecurityDetailsResponseModel> (result);
						PSecurityDetailsList = new ObservableCollection<SecurityDetailsModel> (securityDetailsRequestModel.SecurityDetails);
					} 
				}
			
			} catch {
				GlobalVariables.DisplayMessage = "Record not found";
				MessagingCenter.Send<VMSecurityDetails> (this, Strings.Display_Message);
			}
			ActivityIndicator = false;
		}
	}
}


