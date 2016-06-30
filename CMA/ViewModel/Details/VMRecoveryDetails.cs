using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace CMA
{
	public class VMRecoveryDetails:INotifyPropertyChanged
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
		public VMRecoveryDetails ()
		{
			LoadRecoveryDetails ();
		}
		private ObservableCollection <RecoveryDetailsModel> _PRecoveryDetailsList ;

		public ObservableCollection<RecoveryDetailsModel> PRecoveryDetailsList {
			get {

				return _PRecoveryDetailsList;
			}
			set {
				_PRecoveryDetailsList = value;
				OnPropertyChanged ("PRecoveryDetailsList");
			}

		}
		public async Task LoadRecoveryDetails ()
		{
//			ActivityIndicator = true;

			var result = await APIRequest.Instance.GetRecoveryDetailsList(new RecoveryDetailsRequestModel {
				CustomerEntityID=GlobalVariables.CustomerEntityID,
				AccountEntityID = GlobalVariables.AccountEntityID});

			if (result != null) {
				//result = result.ToDecrypt ();
			var	RecoveryDetailsList1= JsonConvert.DeserializeObject<RecoveryDetailsResponseModel> (result);
				//MessagingCenter.Send<VMAddInventory> (this, Strings.LOAD_SUCCESS);
				PRecoveryDetailsList = new  ObservableCollection<RecoveryDetailsModel>(RecoveryDetailsList1.RecoveryDetails);

				//MessagingCenter.Send<VMAddInventory> (this, Strings.InventoryDetails_SUCCESS);
				} 
//			ActivityIndicator = false;
			}
		}
	}


