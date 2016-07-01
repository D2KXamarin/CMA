using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace CMA
{
	public class VMActionEventDiaryList:INotifyPropertyChanged
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

		public VMActionEventDiaryList ()
		{
//			LoadActionEventDiaryList ();
		}

		private ObservableCollection <ActionEventDiaryListModel> _PRemarkDetailsList;

		public ObservableCollection<ActionEventDiaryListModel> PRemarkDetailsList {
			get {

				return _PRemarkDetailsList;
			}
			set {
				_PRemarkDetailsList = value;
				OnPropertyChanged ("PRemarkDetailsList");
			}

		}

		public async Task LoadActionEventDiaryList ()
		{
			//			ActivityIndicator = true;
			try {
				if (GlobalVariables.IsOffline) {
					PRemarkDetailsList = new ObservableCollection<ActionEventDiaryListModel> (DataSync.GetDataSyncActionEventDiaryList (GlobalVariables.CustomerEntityID, GlobalVariables.AccountEntityID));
				} else {
					var result = await APIRequest.Instance.GetActionEventDiaryListAPI (new ActionEventDiaryListRequestModel {
						CustomerEntityID = GlobalVariables.CustomerEntityID,
						AccountEntityID = GlobalVariables.AccountEntityID
					});

					if (result != null) {
						//result = result.ToDecrypt ();
						var	actionEventDiaryDetailsResponseModel = JsonConvert.DeserializeObject<ActionEventDiaryListResponseModel> (result);
						//MessagingCenter.Send<VMAddInventory> (this, Strings.LOAD_SUCCESS);
						PRemarkDetailsList = new  ObservableCollection<ActionEventDiaryListModel> (actionEventDiaryDetailsResponseModel.GetActionEventDiaryList);

						//MessagingCenter.Send<VMAddInventory> (this, Strings.InventoryDetails_SUCCESS);
					} 
					//			ActivityIndicator = false;
				}
			} catch (Exception ex) {
			}
		}
	}
}

