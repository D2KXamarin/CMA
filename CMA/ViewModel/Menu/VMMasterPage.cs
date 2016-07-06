using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;



namespace CMA
{
	public class VMMasterPage:INotifyPropertyChanged
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

		private ObservableCollection<MasterPageModel> _masterPageItems;

		public ObservableCollection<MasterPageModel> masterPageItems {
			get { return _masterPageItems; }
			set {
				_masterPageItems = value;
				OnPropertyChanged ("masterPageItems");
			}
		}

		public VMMasterPage ()
		{
			LoadMainMenu ();
		}

		private async Task LoadMainMenu ()
		{
			masterPageItems = new ObservableCollection<MasterPageModel> ();




			masterPageItems.Add (new MasterPageModel {
				Title = "Home",

				TargetType = typeof(HomePage),
				MenuGroup = "Home"
				//	hasSubMenu = false
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "Select Operations",

				TargetType = typeof(Operations),
				MenuGroup = "Home"
				//	hasSubMenu = false
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "Detail List",
				Index = 0,
				TargetType = typeof(DetailsTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});
//
			masterPageItems.Add (new MasterPageModel {
				Title = "Loan Details",
				Index = 0,
				TargetType = typeof(DetailsTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});
//
//
			masterPageItems.Add (new MasterPageModel {
				Title = "CoBorrGuar Details",
				Index = 1,
				TargetType = typeof(DetailsTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});
			masterPageItems.Add (new MasterPageModel {
				Title = "Security Details",
				Index = 2,
				TargetType = typeof(DetailsTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});
			masterPageItems.Add (new MasterPageModel {
				Title = "Recovery Details",
				Index = 3,
				TargetType = typeof(DetailsTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});
			masterPageItems.Add (new MasterPageModel {
				Title = "Remark",
				Index = 0,
				TargetType = typeof(RemarkTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "Institutional Memory Diary",
				Index = 0,
				TargetType = typeof(RemarkTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "View Previous Remark",
				Index = 1,
				TargetType = typeof(RemarkTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "View/Select Remark",
				Index = 2,
				TargetType = typeof(RemarkTabbedPage),
				MenuGroup = "Details"
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "Customer Reallocation",
				Index = 3,
				TargetType = typeof(CustomerReallocation),
				MenuGroup = "Home"
				//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageModel {
				Title = "ChangePassword",

				TargetType = typeof(ChangePassword),
				MenuGroup = "Home"
				//	hasSubMenu = false
				//SubMenu = subMenuList
			});

			if (!GlobalVariables.IsOffline)
				masterPageItems.Add (new MasterPageModel {
					Title = "Download for Offline",

					TargetType = typeof(ServerToLocal),
					MenuGroup = "Home"
					//	hasSubMenu = false
					//SubMenu = subMenuList
				});

			if (GlobalVariables.IsOffline)
				masterPageItems.Add (new MasterPageModel {
					Title = "Upload To Server",

					TargetType = typeof(ServerToLocal),
					MenuGroup = "Home"
					//	hasSubMenu = false
					//SubMenu = subMenuList
				});
		}
	}
}

