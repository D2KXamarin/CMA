using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using CMA;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;




namespace CMA
{
	public class VMInsuranceCompany:INotifyPropertyChanged
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

		public VMInsuranceCompany ()
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

		private bool _IsSearchEnalble = false;

		public  bool IsSearchEnalble { 
			get { return _IsSearchEnalble; }
			set { 
				_IsSearchEnalble = value;
				OnPropertyChanged ("IsSearchEnalble");
			} 
		}

		private ObservableCollection<InsuranceCompanyMasterModel> _PCompanyList = new ObservableCollection<InsuranceCompanyMasterModel> ();

		public ObservableCollection<InsuranceCompanyMasterModel> PCompanyList {
			get { 
				return _PCompanyList;
			}
			set {
				_PCompanyList = value;
				OnPropertyChanged ("PCompanyList");
			}
		}

		private static string _PSearchValue = string.Empty;

		public string PSearchValue {
			get{ return _PSearchValue; }
			set {
				if(value.Length<=1)
					value = value.Trim();
				if (value.Length <= 20) {
					_PSearchValue = value;
					PCompanyList.Clear ();
				}
				else
				{
					GlobalVariables.DisplayMessage = "Please enter less than 20 characters for search";
					MessagingCenter.Send<VMInsuranceCompany>(this, Strings.Display_Message);

				}
				OnPropertyChanged ("PSearchValue");
				ChangeControlState ();
			}
		}

		public void ChangeControlState ()
		{
			if (PSearchValue == "") {

				IsSearchEnalble = false;

			} else if (PSearchValue.Length > 0) {
				IsSearchEnalble = true;
			}
		}

		private static string _PSearch = string.Empty;

		public string PSearch {
			get { return _PSearch; }
			set {
				_PSearch = value;
				OnPropertyChanged ("PSearch");
			}
		}

		private InsuranceCompanyMasterModel _PSelectedCompany = null;

		public InsuranceCompanyMasterModel PSelectedCompany {
			get { return _PSelectedCompany; }
			set {
				_PSelectedCompany = value;
				OnPropertyChanged ("PSelectedCompany");
			}
		}

		private Command _SearchButton = null;

		public Command SearchButton {
			get {
				return _SearchButton ?? new Command (async delegate(object o) {
					ActivityIndicator = true;
					if (PSearchValue.Length >= 3) {
						try {
							var result = await APIRequest.Instance.GetInsuranceCompanyList (new InsuranceCompanyListRequestModel {
								UserLoginId = GlobalVariables.UserLoginID,
								SearchString = PSearchValue
							});

							if (result != null) {
								var insuranceCompanyMasterResponceModel = JsonConvert.DeserializeObject<InsuranceCompanyMasterResponceModel> (result);
								PCompanyList = new ObservableCollection<InsuranceCompanyMasterModel> (insuranceCompanyMasterResponceModel.InsuranceCompanyMaster);
							}

						} catch {
							GlobalVariables.DisplayMessage = "Selected Insurance Company Not Found";
							MessagingCenter.Send<VMInsuranceCompany> (this, Strings.Display_Message);
						}
					} else {
						GlobalVariables.DisplayMessage = "Please enter minimum 3 characters for search";
						MessagingCenter.Send<VMInsuranceCompany> (this, Strings.Display_Message);
					}

					ActivityIndicator = false;
				});

			}
		}




	}
}


