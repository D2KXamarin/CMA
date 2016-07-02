using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CMA
{
	public class VMGoldDetals:INotifyPropertyChanged
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

		public VMGoldDetals ()
		{

		}
		public int SecurityEntityId ;



		static string _PQuantity = string.Empty;

		public string PQuantity {
			get { return _PQuantity; }
			set {
				_PQuantity = value;
				OnPropertyChanged ("PQuantity");
			}
		}

		static string _PCarat = string.Empty;

		public string PCarat {
			get { return _PCarat; }
			set {
				_PCarat = value;
				OnPropertyChanged ("PCarat");
			}
		}

		static string _PMargin = string.Empty;

		public string PMargin {
			get { return _PMargin; }
			set {
				_PMargin = value;
				OnPropertyChanged ("PMargin");
			}
		}

		static string _PNature = string.Empty;

		public string PNature {
			get { return _PNature; }
			set {
				_PNature = value;
				OnPropertyChanged ("PNature");
			}
		}

		public async Task LoadGoldDetails (){
			SecurityGoldDetailModel SGM = null;

			var result = await APIRequest.Instance.GetSecurityGoldDetail (new SecurityGoldDetailRequestModel {
				UserLoginID = GlobalVariables.UserLoginID,
				SecurityEntityID =SecurityEntityId
			});

			if (result != null) {
				SecurityGoldDetailResponceModel responseModelList = JsonConvert.DeserializeObject<SecurityGoldDetailResponceModel>(result);
				SGM = responseModelList.SecurityGoldDetail [0];
			}

			if (SGM != null) {
				PQuantity =Convert.ToString(SGM.Quantity);
				PCarat =Convert.ToString(SGM.Karat);
				PMargin =Convert.ToString (SGM.Margin);
		
			}
		}
	}
}

