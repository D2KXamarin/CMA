using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CMA
{
	public class VMCoBorrGuarDetails:INotifyPropertyChanged
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

		public VMCoBorrGuarDetails ()
		{
			LoadCoBorrGuarDetails ();
		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private ObservableCollection <CoBorrGuarDetailsModel> _PCoBorrGuarDetailsList;

		public ObservableCollection<CoBorrGuarDetailsModel> PCoBorrGuarDetailsList {
			get {

				return _PCoBorrGuarDetailsList;
			}
			set {
				_PCoBorrGuarDetailsList = value;
				OnPropertyChanged ("PCoBorrGuarDetailsList");

			}

		}

		public async Task LoadCoBorrGuarDetails ()
		{
			ActivityIndicator = true;

			try {
				var result = await APIRequest.Instance.GetCoBorrGuarDetailsList (new CoBorrGuarDetailsRequestModel {
					CustomerEntityID = GlobalVariables.CustomerEntityID,
					AccountEntityID = GlobalVariables.AccountEntityID
				});

				if (result != null) {
					//result = result.ToDecrypt ();
					var	CoBorrGuarDetailsList = JsonConvert.DeserializeObject<CoBorrGuarDetailsResponseModel> (result);
					//MessagingCenter.Send<VMAddInventory> (this, Strings.LOAD_SUCCESS);
					PCoBorrGuarDetailsList = new  ObservableCollection<CoBorrGuarDetailsModel> (CoBorrGuarDetailsList.CoBorrGuar);


				} 
			} catch {
			}
			ActivityIndicator = false;
		}
	}
}

