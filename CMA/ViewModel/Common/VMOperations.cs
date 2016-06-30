using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace CMA
{
	public class VMOperations:INotifyPropertyChanged
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

		public VMOperations ()
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

		private bool _IsVisibleBranchBtn = true;

		public bool IsVisibleBranchBtn {
			get{ return _IsVisibleBranchBtn; }
			set {
				_IsVisibleBranchBtn = value;
				OnPropertyChanged ("IsVisibleBranchBtn");
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

		private bool _IsEnableCustomer = false;

		public bool IsEnableCustomer {
			get{ return _IsEnableCustomer; }
			set {
				_IsEnableCustomer = value;
				OnPropertyChanged ("IsEnableCustomer");
			}
		}

		private static string _PAccountNo = string.Empty;

		public string PAccountNo {
			get{ return _PAccountNo; }
			set {
				_PAccountNo = value;
				OnPropertyChanged ("PAccountNo");
				ChangeControlState ();
			}
		}

		private bool _IsEnableAcid = false;

		public bool IsEnableAcid {
			get{ return _IsEnableAcid; }
			set {
				_IsEnableAcid = value;
				OnPropertyChanged ("IsEnableAcid");
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

		private bool _IsVisibleAcid = false;

		public bool IsVisibleAcid {
			get{ return _IsVisibleAcid; }
			set {
				_IsVisibleAcid = value;
				OnPropertyChanged ("IsVisibleAcid");
			}
		}

		public void ChangeControlState ()
		{
			IsEnableCustomer = !string.IsNullOrEmpty (PBranch);
			IsEnableAcid = !string.IsNullOrEmpty (PCustomer);
			IsChangeButtonEnable = !string.IsNullOrEmpty (PCustomer);
			IsCancelButtonEnable = !string.IsNullOrEmpty (PCustomer);
			IsVisibleAcid = !string.IsNullOrEmpty (PAccountNo);
		}
	}
}

