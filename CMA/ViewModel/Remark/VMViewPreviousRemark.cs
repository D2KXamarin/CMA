using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CMA
{
	public class VMViewPreviousRemark:INotifyPropertyChanged
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

		public VMViewPreviousRemark ()
		{
			
		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private PreviousRemarkListResponseModel _PreviousRemarkDetails = new PreviousRemarkListResponseModel ();

		public PreviousRemarkListResponseModel PreviousRemarkDetails {
			get { 
				return _PreviousRemarkDetails;
			}
			set {
				_PreviousRemarkDetails = value;

				OnPropertyChanged ("PreviousRemarkDetails");
			}
		}

		private ObservableCollection<PreviousRemarkListModel> _PPreviousRemarkList;

		public ObservableCollection<PreviousRemarkListModel> PPreviousRemarkList {
			get { 
				return _PPreviousRemarkList;
			}
			set {
				_PPreviousRemarkList = value;
				OnPropertyChanged ("PPreviousRemarkList");
			}
		}

		public async Task LoadPreviousRemarks ()
		{
			ActivityIndicator = true;
			var result = await APIRequest.Instance.GetPreviousRemarkList (new PreviousRemarkListRequestModel {
				CustomerEntityID = GlobalVariables.CustomerEntityID,
				AccountEntityID = GlobalVariables.AccountEntityID
			});

			try {
				if (result != null) {
					

					var previousRemarkListRequestModel = JsonConvert.DeserializeObject<PreviousRemarkListResponseModel> (result);
					PPreviousRemarkList = new ObservableCollection<PreviousRemarkListModel> (previousRemarkListRequestModel.GetPreviousRemark);
				} 


			} 
			catch(Exception ex) {

			}
			ActivityIndicator = false;

		}
	}
}

