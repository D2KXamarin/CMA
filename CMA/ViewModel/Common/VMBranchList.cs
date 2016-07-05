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
	public class VMBranchList:INotifyPropertyChanged
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

		public VMBranchList ()
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

		private ObservableCollection<BranchListModel> _PBranchList = new ObservableCollection<BranchListModel> ();

		public ObservableCollection<BranchListModel> PBranchList {
			get { 
				return _PBranchList;
			}
			set {
				_PBranchList = value;
				OnPropertyChanged ("PBranchList");
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
					PBranchList.Clear ();
				}
				else
				{
					GlobalVariables.DisplayMessage = "Please enter less than 20 characters for search";
					MessagingCenter.Send<VMBranchList>(this, Strings.Display_Message);

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

		private BranchListModel _PSelectedBranch = null;

		public BranchListModel PSelectedBranch {
			get { return _PSelectedBranch; }
			set {
				_PSelectedBranch = value;
				OnPropertyChanged ("PSelectedBranch");
			}
		}

		private Command _SearchButton = null;

		public Command SearchButton {
			get {
				return _SearchButton ?? new Command (async delegate(object o) {
					ActivityIndicator = true;
					if (PSearchValue.Length >= 3) {
						try {
							var result = await APIRequest.Instance.GetBranchList (new BranchListRequestModel {
								UserLoginId = GlobalVariables.UserLoginID,
								SearchString = PSearchValue
							});

							if (result != null) {
								var branchListResponseModel = JsonConvert.DeserializeObject<BranchListResponseModel> (result);
								PBranchList = new ObservableCollection<BranchListModel> (branchListResponseModel.BranchList);
							}

						} catch {
							GlobalVariables.DisplayMessage = "Selected Branch Not Found";
							MessagingCenter.Send<VMBranchList> (this, Strings.Display_Message);
						}
					} else {
						GlobalVariables.DisplayMessage = "Please enter minimum 3 characters for search";
						MessagingCenter.Send<VMBranchList> (this, Strings.Display_Message);
					}

					ActivityIndicator = false;
				});

			}
		}




	}
}


