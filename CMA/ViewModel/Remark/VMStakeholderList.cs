using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using XLabs;

namespace CMA
{
	public class VMStakeholderList:INotifyPropertyChanged
	{
		public VMStakeholderList ()
		{
			GetStakeholderList ();
		}

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

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private ObservableCollection<StakeholderListModel> _PStakeholderList;

		public ObservableCollection<StakeholderListModel> PStakeholderList {
			get { 
				return _PStakeholderList;
			}
			set {
				_PStakeholderList = value;
				OnPropertyChanged ("PStakeholderList");
			}
		}

		public void GetStakeholderList ()
		{
			List<StakeholderListModel> stakeholderListModel = new List<StakeholderListModel> ();
			if (GlobalVariables.StakeholderType == "P") {
				stakeholderListModel = SQLiteDatabase.Instance.GetPrimaryActionStakeholder (); 
			} else if (GlobalVariables.StakeholderType == "S") {
				stakeholderListModel = SQLiteDatabase.Instance.GetSecondaryActionStakeholder ();
			} else {
				stakeholderListModel = SQLiteDatabase.Instance.GetInfoStakeholder ();
			}

			PStakeholderList = new ObservableCollection<StakeholderListModel> (stakeholderListModel);
		}

		public void SaveStakeholderList ()
		{
			foreach (StakeholderListModel currobj in PStakeholderList) {	
				if (currobj.IsAllocate) {
					currobj.StakeholderType = currobj.IsAllocate ? GlobalVariables.StakeholderType : null;
				}
			}

			SQLiteDatabase.Instance.SaveStakeholder (PStakeholderList.ToList ()); 
		}

		private Command _OkCommand;

		public Command OkCommand {
			get { 
				return _OkCommand ?? new Command (async delegate(object o) {
					SaveStakeholderList ();
				});
			}
		}


	}
}

