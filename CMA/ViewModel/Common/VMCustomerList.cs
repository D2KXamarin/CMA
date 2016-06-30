using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using CMA;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using SQLite;
using System.Linq;


namespace CMA
{
	public class VMCustomerList:INotifyPropertyChanged
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

		public VMCustomerList ()
		{

		}

		private static string _PSearch = "";

		public string PSearch {
			get{ return _PSearch; }
			set {
				if(value.Length<=1)
					value = value.Trim();
				if (value.Length <= 20) {
					_PSearch = value;
					PCustomerList.Clear ();
				}
				else
				{
					GlobalVariables.DisplayMessage = "Please enter less than 20 characters for search";
					MessagingCenter.Send<VMCustomerList>(this, Strings.Display_Message);

				}
				OnPropertyChanged ("PSearch");
				ChangeControlState ();
			}
		}

		private bool _IsEnableSearch;

		public  bool IsEnableSearch { 
			get { return _IsEnableSearch; }
			set { 
				_IsEnableSearch = value;
				OnPropertyChanged ("IsEnableSearch");
			} 
		}

		private bool _IsVisibleSearch = !GlobalVariables.IsOffline;

		public  bool IsVisibleSearch { 
			get { return _IsVisibleSearch; }
			set { 
				_IsVisibleSearch = value;
				OnPropertyChanged ("IsVisibleSearch");
			} 
		}

		public void ChangeControlState ()
		{
			if (PSearch=="") {

				IsEnableSearch = false;


			} else if (PSearch.Length >0) {
				IsEnableSearch = true;
			}

		}

		private CustomerListModel _PSelectedCustomer = null;

		public CustomerListModel PSelectedCustomer {
			get {
				return _PSelectedCustomer;
			}
			set {

				_PSelectedCustomer = value;


				OnPropertyChanged ("PSelectedCustomer");
				ChangeControlState1 ();
			}

		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private bool _IsEnableSave;

		public  bool IsEnableSave { 
			get { return _IsEnableSave; }
			set { 
				_IsEnableSave = value;
				OnPropertyChanged ("IsEnableSave");


			} 
		}

		public void ChangeControlState1 ()
		{
			if (PCustomerList == null) {

				IsEnableSave = false;

			} else if (PCustomerList != null) {
				IsEnableSave = true;
			}
		}

		private ObservableCollection <CustomerListModel> _PCustomerList=new ObservableCollection<CustomerListModel>();

		public ObservableCollection<CustomerListModel> PCustomerList {
			get {

				return _PCustomerList;
			}
			set {
				_PCustomerList = value;
				OnPropertyChanged ("PCustomerList");
				ChangeControlState1 ();
			}

		}

		private Command _SearchCustomer = null;

		public Command SearchCustomer {
			get {
				return _SearchCustomer ?? new Command (async delegate(object o) {
					ActivityIndicator=true;
					if (PSearch.Length>=3) {
						try {
							var result = await APIRequest.Instance.GetCustomerList (new CustomerListRequestModel {
								BranchCode = GlobalVariables.BranchCode,
								CustomerID = GlobalVariables.CustomerID,
								SearchString = PSearch
							});

							if (result != null) {

								var responseModelList = JsonConvert.DeserializeObject<Custumercollection> (result);
								PCustomerList = new  ObservableCollection<CustomerListModel> (responseModelList.CustomerList);
							}
							else {
								GlobalVariables.DisplayMessage = "Record Not Found";

								MessagingCenter.Send<VMCustomerList> (this, Strings.Display_Message);
							}
						}
						catch {
							GlobalVariables.DisplayMessage = "Error";
							MessagingCenter.Send<VMCustomerList> (this, Strings.Display_Message);
						}
					} 


					else {

						GlobalVariables.DisplayMessage = "Please enter minimum 3 characters for search";
						MessagingCenter.Send<VMCustomerList> (this, Strings.Display_Message);
					}
					ActivityIndicator=false;
				});

			}
		}

		public async Task SaveCustomer(){
			if(PSelectedCustomer!=null){
				
				CustomerDetailsResponseModel customerDetailsResponseModel = null;
				if (GlobalVariables.IsOffline) {
					customerDetailsResponseModel = DataSync.GetDataSyncCustomerDetails (PSelectedCustomer.CustomerEntityID);
				} else {
					string result = await APIRequest.Instance.GetCustomerDetailsList (new CustomerDetailsRequestModel {
						CustomerEntityID = PSelectedCustomer.CustomerEntityID
					});

					if(result!= null)
						customerDetailsResponseModel = JsonConvert.DeserializeObject<CustomerDetailsResponseModel> (result);
				}

				if (customerDetailsResponseModel != null) {

					try {
						if (SQLiteDatabase.Instance.InsertCustomerDetails (customerDetailsResponseModel)) {
							GlobalVariables.CustomerEntityID = PSelectedCustomer.CustomerEntityID;
							GlobalVariables.CustomerID = PSelectedCustomer.CustomerID;
							GlobalVariables.CustomerName = PSelectedCustomer.CustomerName;


							MessagingCenter.Send<VMCustomerList> (this, Strings.CustomerList_Success);
						}
					} catch(Exception ex) {
						GlobalVariables.DisplayMessage = "Error";
						MessagingCenter.Send<VMCustomerList> (this, Strings.Display_Message);
					}
				}
			}
			else{
				GlobalVariables.DisplayMessage = "Please Select Customer";
				MessagingCenter.Send<VMCustomerList> (this, Strings.Display_Message);
			}
		}

	}
}

