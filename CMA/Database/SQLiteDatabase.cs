using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CMA
{
	public class SQLiteDatabase
	{
		private static SQLiteDatabase _instance = null;

		SQLiteConnection database;

		private static object locker = new object ();

		public static SQLiteDatabase Instance {
			get {
				lock (locker) {
					if (_instance == null)
						_instance = new SQLiteDatabase ();
				}

				return _instance;
			}
		}

		public SQLiteDatabase ()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
		}

		public bool IsTableExist (string tableName)
		{
			try {
				return database.GetTableInfo (tableName).Count > 0;
			} catch (Exception e) {
				return false;
			}
		}

		public bool InsertCustomerDetails (CustomerDetailsResponseModel customerDetailsResponseModel)
		{
			try {

				database.CreateTable<CustomerDetailsModel> ();
				database.CreateTable<AccountDetailModel> ();

				try {
					database.DeleteAll<CustomerDetailsModel> ();
				} catch {
				}

				try {
					database.DeleteAll<AccountDetailModel> ();
				} catch {
				}

				if (customerDetailsResponseModel.CustomerDetails != null) {
					foreach (CustomerDetailsModel cm in customerDetailsResponseModel.CustomerDetails) {
						database.Insert (cm);
					}
				}

				if (customerDetailsResponseModel.AccountDetails != null) {
					foreach (AccountDetailModel Account in customerDetailsResponseModel.AccountDetails) {
						database.Insert (Account);
					}
				}


				return true;
			} catch {
				return false;
			}
		}

		public List<AccountDetailModel> GetAccountList ()
		{
			try {
				lock (locker) {
					return (from i in database.Table<AccountDetailModel> ()
					        select i).ToList ();
			
				}
			} catch {
				return null;
			}
		}

		public bool InsertDefaultStatus (List<DefaultStatusModel> defaultStatusModel)
		{
			try {
				database.CreateTable<DefaultStatusModel> ();

				if (defaultStatusModel != null) {
					foreach (DefaultStatusModel DSM in defaultStatusModel) {
						database.Insert (DSM);
					}
				}
				return true;
			} catch (Exception ex) {
				return false;
			}
		}

		public List<DefaultStatusModel> GetDefaultStatusMaster ()
		{
			try {
				lock (locker) {
					return (from i in database.Table<DefaultStatusModel> ()
					        select i).ToList ();

				}
			} catch {
				return null;
			}
		}

		public bool InsertStakHolderDetails (StakeholderListResponseModel stakeholderListResponseModel)
		{
			try {

				database.CreateTable<StakeholderListModel> ();

				try {
					database.DeleteAll<StakeholderListModel> ();
				} catch {
				}

				if (stakeholderListResponseModel.StakeholderList != null) {
					foreach (StakeholderListModel cm in stakeholderListResponseModel.StakeholderList) {
						database.Insert (cm);
					}
				}

				return true;
			} catch (Exception ex) {
				return false;
			}
		}

		public List<StakeholderListModel> GetPrimaryActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<StakeholderListModel> ()
				        where ((i.StakeholderType == "P") || (i.StakeholderType == null))
				        select i).ToList ();
			}
		}



		public List<StakeholderListModel> GetSecondaryActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<StakeholderListModel> ()
				        where ((i.StakeholderType == "S") || (i.StakeholderType == null))
				        select i).ToList ();

			}
		}

		public List<StakeholderListModel> GetInfoStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<StakeholderListModel> ()
				        where ((i.StakeholderType == "I") || (i.StakeholderType == null))
				        select i).ToList ();
			}
		}

		public bool SaveStakeholder (List<StakeholderListModel> stakeholderListModel)
		{
			lock (locker) {
				if (stakeholderListModel != null) {
					try {
						database.UpdateAll (stakeholderListModel, false);

					} catch (Exception Ex) {

					}
					return true;

				} else {
					return false;
				}
			}
		}

		public List<StakeholderListModel> GetAllocatedPrimaryActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<StakeholderListModel> ()
				        where (i.StakeholderType == "P")
				        select i).ToList ();
			}
		}

		public List<StakeholderListModel> GetAllocatedSecondaryActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<StakeholderListModel> ()
				        where (i.StakeholderType == "S")
				        select i).ToList ();

			}
		}

		public List<StakeholderListModel>  GetAllocatedInfoStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<StakeholderListModel> ()
				        where (i.StakeholderType == "I")
				        select i).ToList ();

			}
		}

		public List<AllocatedStakeholder> GetAllocatedAssignPrimaryActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<AllocatedStakeholder> ()
				        where (i.StakeholderType == "P")
				        select i).ToList ();


			}
		}

		public List<AllocatedStakeholder> GetAllocatedAssignSecondaryActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<AllocatedStakeholder> ()
				        where (i.StakeholderType == "S")
				        select i).ToList ();
			}
		}

		public List<AllocatedStakeholder> GetAllocatedAssignInfoActionStakeholder ()
		{
			lock (locker) {
				return (from i in database.Table<AllocatedStakeholder> ()
				        where (i.StakeholderType == "I")
				        select i).ToList ();
			}
		}

		public List<AssignedStakeholder> GetAssignedPrimaryActionStakeholder ()
		{
			lock (locker) {
//				return (from i in database.Table<AssignedStakeholder> ()
//					where (i.StakeholderType == "P"  )
//					select i).ToList ();

				return database.Query<AssignedStakeholder> 
					("select * from [AssignedStakeholder] " +
				"where StakeholderType='P' and EventID=" + GlobalVariables.AssignActionID);
			}
		}

		public List<AssignedStakeholder> GetAssignedSecondaryActionStakeholder ()
		{
			lock (locker) {
				
				return database.Query<AssignedStakeholder> 
					("select * from [AssignedStakeholder] " +
				"where StakeholderType='S' and EventID=" + GlobalVariables.AssignActionID);
			}
		}

		public List<AssignedStakeholder> GetAssignedInfoActionStakeholder ()
		{
			lock (locker) {
				return database.Query<AssignedStakeholder> 
					("select * from [AssignedStakeholder] " +
				"where StakeholderType='I' and EventID=" + GlobalVariables.AssignActionID);
			}
		}

		public bool InsertAssignActionStakHolderDetails (AssignActionResponseModel assignActionResponseModel)
		{
			try {

				database.CreateTable<AssignActionModel> ();
				database.CreateTable<AllocatedStakeholder> ();
				database.CreateTable<AssignedStakeholder> ();
				try {
					database.DeleteAll<AssignActionModel> ();
					database.DeleteAll<AllocatedStakeholder> ();
					database.DeleteAll<AssignedStakeholder> ();
				} catch {
				}

				if (assignActionResponseModel.AssignAction != null) {
					foreach (AssignActionModel am in assignActionResponseModel.AssignAction) {
						database.Insert (am);
					}
				}

				if (assignActionResponseModel.AllocatedStakeholder != null) {
					foreach (AllocatedStakeholder am in assignActionResponseModel.AllocatedStakeholder) {
						database.Insert (am);
					}
				}

				if (assignActionResponseModel.AssignedStakeholder != null) {
					foreach (AssignedStakeholder am in assignActionResponseModel.AssignedStakeholder) {
						database.Insert (am);
					}
				}


				return true;
			} catch {
				return false;
			}
		}

		public bool InsertUpdateAssignActionStakHolder (List<AssignedStakeholder>  listAssignedStakeholder)
		{
			try {
				database.Query<AssignedStakeholder> ("delete from AssignedStakeholder " +
				"where EventID = " + GlobalVariables.AssignActionID + " and StakeholderType='" + GlobalVariables.StakeholderType + "'");
			} catch {
			}

			if (listAssignedStakeholder != null) {
				foreach (AssignedStakeholder am in listAssignedStakeholder) {
					database.Insert (am);
				}
			}

			return true;
		}


		#region Sync

		public bool IsDataInLocal ()
		{
			try {
				List<DataSyncStatusModel> DataSyncStatusModel = database.Query<DataSyncStatusModel> 
					("select * from [DataSyncStatusModel] ");

				return (from customer in DataSyncStatusModel
				        where customer.ServerToLocalStatus == true && customer.LocalToServerStatus == false
				        select customer).ToList ().Count > 0;

			} catch (Exception ex) {
				return false;
			}
		}

		public bool InsertServerToLocal (List<ServerToLocalModel> listServerToLocalModel)
		{
			try {

				database.CreateTable<ServerToLocalModel> ();
				try {
					database.DeleteAll<ServerToLocalModel> ();
				} catch {
				}

				if (listServerToLocalModel != null) {
					foreach (ServerToLocalModel serverToLocalModel in listServerToLocalModel) {
						database.Insert (serverToLocalModel);
					}
				}
				return true;
			} catch {
				return false;
			}
		}

		public bool InsertDataSyncStatusModel (List<DataSyncStatusModel> listDataSyncStatusModel)
		{
			try {

				database.CreateTable<DataSyncStatusModel> ();
				try {
					database.DeleteAll<DataSyncStatusModel> ();
				} catch {
				}

				if (listDataSyncStatusModel != null) {
					foreach (DataSyncStatusModel dataSyncStatusModel in listDataSyncStatusModel) {
						database.Insert (dataSyncStatusModel);
					}
				}
				return true;
			} catch {
				return false;
			}
		}

		public List<DataSyncBranchModel> GetDataSyncBranch ()
		{
			try {
				return database.Query<DataSyncBranchModel> 
					("select BranchCode,BranchName from [DataSyncStatusModel] LIMIT 1");


			} catch (Exception ex) {
				return null;
			}
		}

		public List<CustomerListModel> GetDataSyncCustomerList ()
		{
			try {
				//return database.Query<CustomerListModel> 
				List<DataSyncStatusModel> listDataSyncStatusModel = (from i in database.Table<DataSyncStatusModel> ()
				                                                     where (i.LocalToServerStatus == false)
				                                                     select i).ToList ();

				List<CustomerListModel> listCustomerListModel = new List<CustomerListModel> ();
				foreach (DataSyncStatusModel item in listDataSyncStatusModel) {
					listCustomerListModel.Add (new CustomerListModel () {
						CustomerID = item.CustomerID,
						CustomerEntityID = item.CustomerEntityID,
						CustomerName = item.CustomerName
					});
				}

				return listCustomerListModel;

			} catch (Exception ex) {
				return null;
			}
		}

		public List<DataSyncStatusModel> GetDataSyncStatusModel ()
		{
			try {
				return database.Query<DataSyncStatusModel> 
					("select * from [DataSyncStatusModel]");


			} catch (Exception ex) {
				return null;
			}
		}

		public List<LocalDataModel> GetDataSyncCustomerDetails ()
		{
			try {
				return database.Query<LocalDataModel> 
					("select Data from ServerToLocalModel where TableName = 'CustomerDetails'");


			} catch (Exception ex) {
				return null;
			}
		}

		public List<ServerToLocalModel> GetDataSyncDetails ()
		{
			try {
				return database.Query<ServerToLocalModel> 
					("select * from ServerToLocalModel where TableName = 'Details'");


			} catch (Exception ex) {
				return null;
			}
		}

		public List<ServerToLocalModel> GetDataSyncActionDetails ()
		{
			try {
				return database.Query<ServerToLocalModel> 
					("select * from ServerToLocalModel where TableName = 'ActionDetails'");


			} catch (Exception ex) {
				return null;
			}
		}

		public List<ActionEventUpdate> DataSyncGetActionEventList()
		{
			try {
				return database.Query<ActionEventUpdate> 
					("select * from ActionEventUpdate");	
			} catch (Exception ex) {
				return null;
			}

		}

		public List<ActionEventUpdate> DataSyncGetActionEvent(string EventID)
		{
			try {
				return database.Query<ActionEventUpdate> 
					("select * from ActionEventUpdate Where EventID="+EventID);	
			} catch (Exception ex) {
				return null;
			}

		}

		public bool DataSyncUpdateActionEvent (ActionEventUpdate actionEventUpdate)
		{
			try {
				database.CreateTable<ActionEventUpdate> ();

				int status = database.Update (actionEventUpdate);

				if (status == 0) {
					database.Insert (actionEventUpdate);	
				}

				return true;
			} catch (Exception ex) {
				return false;
			}
		}

		#endregion

	}
}

