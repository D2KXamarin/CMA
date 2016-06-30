using System;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using CMA;

namespace CMA
{
	public class VMLoanDetails : INotifyPropertyChanged
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

		public VMLoanDetails ()
		{
			LoadLoanDetails ();
		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private ObservableCollection<LoanDetailsModel> _PLoanDetailsList;

		public ObservableCollection<LoanDetailsModel> PLoanDetailsList {
			get { 
				return _PLoanDetailsList;
				//LoadLoanDetails ();
			}
			set {
				_PLoanDetailsList = value;
				OnPropertyChanged ("PLoanDetailsList");
			}
		}

		public async Task LoadLoanDetails ()
		{
			ActivityIndicator = true;
			try {

				if (GlobalVariables.IsOffline) {
					PLoanDetailsList = new ObservableCollection<LoanDetailsModel> (DataSync.GetDataSyncLoanDetails (GlobalVariables.CustomerEntityID, GlobalVariables.AccountEntityID));
				} else {
					var result = await APIRequest.Instance.GetLoanDetailsAPI (new LoanDetailsRequestModel {
						CustomerEntityID = GlobalVariables.CustomerEntityID,
						AccountEntityID = GlobalVariables.AccountEntityID
					});

					if (result != null) {
						var loanDetailsRequestModel = JsonConvert.DeserializeObject<LoanDetailsResponseModel> (result);
						PLoanDetailsList = new ObservableCollection<LoanDetailsModel> (loanDetailsRequestModel.LoanDetails);
					} else {
						GlobalVariables.DisplayMessage = "Record not found";
						MessagingCenter.Send<VMLoanDetails> (this, Strings.Display_Message);
					}
				}
			} catch {
				GlobalVariables.DisplayMessage = "Error";
				MessagingCenter.Send<VMLoanDetails> (this, Strings.Display_Message);
			}
			ActivityIndicator = false;

		}
	}
}




