using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CMA
{
	public class VMServerToLocal :INotifyPropertyChanged
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

		public VMServerToLocal ()
		{
			
		}

		private static string _PBranch = string.Empty;

		public string PBranch {
			get{ return _PBranch; }
			set {
				_PBranch = value;
				OnPropertyChanged ("PBranch");
				ChangeControlState ();

			}
		}

		private static string _PCustomer = string.Empty;

		public string PCustomer {
			get{ return _PCustomer; }
			set {
				_PCustomer = value;
				OnPropertyChanged ("PCustomer");
				ChangeControlState ();

			}
		}

		private static string _PCustomerSearchValue = string.Empty;

		public string PCustomerSearchValue {
			get{ return _PCustomerSearchValue; }
			set {
				_PCustomerSearchValue = value;
				OnPropertyChanged ("PCustomerSearchValue");

				FilterCustomerList ();
			}
		}

		private bool _IsEnableCustomer = false;

		public bool IsEnableCustomer {
			get{ return _IsEnableCustomer; }
			set {
				_IsEnableCustomer = value;
				OnPropertyChanged ("IsEnableCustomer");
			}
		}



		private bool _IsChangeButtonEnable = false;

		public bool IsChangeButtonEnable {
			get{ return _IsChangeButtonEnable; }
			set {
				_IsChangeButtonEnable = value;
				OnPropertyChanged ("IsChangeButtonEnable");
			}
		}

		private bool _IsCancelButtonEnable = false;

		public bool IsCancelButtonEnable {
			get{ return _IsCancelButtonEnable; }
			set {
				_IsCancelButtonEnable = value;
				OnPropertyChanged ("IsCancelButtonEnable");
			}
		}

		public string _PSelectedCustomerCnt = string.Empty;

		public string PSelectedCustomerCnt {
			get { return _PSelectedCustomerCnt; }
			set {
				_PSelectedCustomerCnt = value;
				OnPropertyChanged ("PSelectedCustomerCnt");
			}
		}

		public bool _IsVisibleSelectedCustomerCnt = false;

		public bool IsVisibleSelectedCustomerCnt {
			get{ return _IsVisibleSelectedCustomerCnt; }
			set {
				_IsVisibleSelectedCustomerCnt = value;
				OnPropertyChanged ("IsVisibleSelectedCustomerCnt");
			}
		}

		private ObservableCollection <CustomerListDataSyncModel> _PCustomerList = new ObservableCollection<CustomerListDataSyncModel> ();

		public ObservableCollection<CustomerListDataSyncModel> PCustomerList {
			get {
				return _PCustomerList;
			}
			set {
				_PCustomerList = value;
				OnPropertyChanged ("PCustomerList");
			}
		}

		public List<CustomerListDataSyncModel> MainCustomerList = new List<CustomerListDataSyncModel> ();

		public void ChangeControlState ()
		{
			IsEnableCustomer = !string.IsNullOrEmpty (PBranch);

			IsChangeButtonEnable = !string.IsNullOrEmpty (PCustomer);
			IsCancelButtonEnable = !string.IsNullOrEmpty (PCustomer);

		}

		public async Task LoadCustomerList ()
		{
			
			try {
				var result = await APIRequest.Instance.GetCustomerList (new CustomerListRequestModel {
					BranchCode = GlobalVariables.BranchCode,
					CustomerID = "",
					SearchString = ""
				});

				if (result != null) {

					var responseModelList = JsonConvert.DeserializeObject<Custumercollection> (result);
					List<CustomerListModel> CustomerList = new  List<CustomerListModel> (responseModelList.CustomerList);

					MainCustomerList.Clear ();
					foreach (CustomerListModel objCustomerList in CustomerList) {
						MainCustomerList.Add (
							new CustomerListDataSyncModel () { 
								CustomerID = objCustomerList.CustomerID,
								CustomerEntityID = objCustomerList.CustomerEntityID,
								CustomerName = objCustomerList.CustomerName,
								IsCheched = false
							});
					}

					PCustomerList = new ObservableCollection<CustomerListDataSyncModel> (MainCustomerList);
				} else {

					GlobalVariables.DisplayMessage = "Error";
				
				}
			} catch (Exception ex) {
				GlobalVariables.DisplayMessage = "Error";
			}

		}

		void FilterCustomerList ()
		{
			PCustomerList = new ObservableCollection<CustomerListDataSyncModel> 
				((from Customer in MainCustomerList
					where Customer.CustomerID.ToUpper ().Contains (PCustomerSearchValue.ToUpper ()) 
							|| Customer.CustomerName.ToUpper ().Contains (PCustomerSearchValue.ToUpper ())
			        select Customer).ToList ());
		}

	}
}

