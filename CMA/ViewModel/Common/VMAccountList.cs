using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using CMA;
using System.Linq;
namespace CMA
{
	public class VMAccountList:INotifyPropertyChanged
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

		public VMAccountList ()
		{
			LoadAccountList ();
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


		private static string _PSearchValue = string.Empty;

		public string PSearchValue {
			get{ return _PSearchValue; }
			set {

				_PSearchValue = value;


				OnPropertyChanged ("PSearchValue");
				ChangeControlState ();
				FilterList ();
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


		private ObservableCollection<AccountDetailModel> _PAccountList;

		public ObservableCollection<AccountDetailModel> PAccountList {
			get { 
				if ( _PAccountList== null) {
					LoadAccountList ();
				}
				return _PAccountList;
			}
			set {
				_PAccountList = value;
				OnPropertyChanged ("PAccountList");
			}
		}
		private ObservableCollection<AccountDetailModel> _MainAccountList;

		public ObservableCollection<AccountDetailModel> MainAccountList {
			get { 
				return _MainAccountList;
			}
			set {
				_MainAccountList = value;
				_PAccountList = value;
				OnPropertyChanged ("MainAccountList");
			}

		}
		private Command filterTextChangedCommand = null;

		public Command FilterTextChangedCommand {
			get {


				return filterTextChangedCommand ?? new Command (delegate(object o) {

					FilterList ();

				});
			}
		}
		private void FilterList ()
		{
			if (!string.IsNullOrEmpty (PSearchValue)) {
				var list = from main in MainAccountList
						where main.AccountID.Contains(PSearchValue)
					select main;

				PAccountList = new ObservableCollection<AccountDetailModel> (list);

			} else {
				PAccountList = MainAccountList;
			}
		}


		private AccountDetailModel  _PSelectedAccount;

		public AccountDetailModel PSelectedAccount {
			get { return _PSelectedAccount; }
			set {
				_PSelectedAccount = value;
				OnPropertyChanged ("PSelectedAccount");
			}
		}

		private void LoadAccountList()
		{
			List<AccountDetailModel> listAccountDetailModel = SQLiteDatabase.Instance.GetAccountList ();

			MainAccountList = new ObservableCollection<AccountDetailModel> (listAccountDetailModel);
			PAccountList = MainAccountList;
		}


	}
}



