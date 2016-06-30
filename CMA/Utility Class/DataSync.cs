using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CMA
{
	public class DataSync
	{
		public DataSync ()
		{
		}

		public static CustomerDetailsResponseModel GetDataSyncCustomerDetails (string CustomerEntityID)
		{
			try {
				string strCustomerDetails = SQLiteDatabase.Instance.GetDataSyncCustomerDetails () [0].Data.ToDecrypt ();
				CustomerDetailsResponseModel customerDetailsResponseModel = JsonConvert.DeserializeObject<CustomerDetailsResponseModel> (strCustomerDetails);

				CustomerDetailsResponseModel objCustomerDetailsResponseModel = new CustomerDetailsResponseModel ();

				objCustomerDetailsResponseModel.CustomerDetails = (from customer in customerDetailsResponseModel.CustomerDetails
				                                                   where customer.CustomerEntityId == CustomerEntityID
				                                                   select customer).ToList ();

				objCustomerDetailsResponseModel.AccountDetails = (from customer in customerDetailsResponseModel.AccountDetails
				                                                  where customer.CustomerEntityId == CustomerEntityID
				                                                  select customer).ToList ();


				return objCustomerDetailsResponseModel;
			} catch {
				return null;
			}
		}

		public static List<LoanDetailsModel> GetDataSyncLoanDetails (string CustomerEntityID, string AccountEntityID)
		{
			try {


				string strDetails = SQLiteDatabase.Instance.GetDataSyncDetails () [0].Data.ToDecrypt ();
				DataSyncDetailsModel dataSyncDetailsModel = JsonConvert.DeserializeObject<DataSyncDetailsModel> (strDetails);

				if (!(string.IsNullOrEmpty (AccountEntityID) || AccountEntityID == "0"))
					return (from LoanDetails in dataSyncDetailsModel.LoanDetails
					        where LoanDetails.CustomerEntityID == CustomerEntityID && LoanDetails.TableName == "LoanDetails" && LoanDetails.AccountEntityID.ToString () == AccountEntityID
					        select LoanDetails).ToList ();

				return (from LoanDetails in dataSyncDetailsModel.LoanDetails
				        where LoanDetails.CustomerEntityID == CustomerEntityID && LoanDetails.TableName == "LoanDetails"
				        select LoanDetails).ToList ();




			} catch {
				return null;
			}
		}

		public static List<SecurityDetailsModel> GetDataSyncSecurityDetails (string CustomerEntityID, string AccountEntityID)
		{
			try {
				string strDetails = SQLiteDatabase.Instance.GetDataSyncDetails () [0].Data.ToDecrypt ();
				DataSyncDetailsModel dataSyncDetailsModel = JsonConvert.DeserializeObject<DataSyncDetailsModel> (strDetails);

				if (!(string.IsNullOrEmpty (AccountEntityID) || AccountEntityID == "0"))
					return (from SecurityDetails in dataSyncDetailsModel.SecurityDetails
					        where SecurityDetails.CustomerEntityID == CustomerEntityID && SecurityDetails.TableName == "SecurityDetails" && SecurityDetails.AccountEntityID == AccountEntityID
					        select SecurityDetails).ToList ();

				return (from SecurityDetails in dataSyncDetailsModel.SecurityDetails
				        where SecurityDetails.CustomerEntityID == CustomerEntityID && SecurityDetails.TableName == "SecurityDetails"
				        select SecurityDetails).ToList ();

			} catch {
				return null;
			}
		}

		public static List<ActionEventDiaryListModel> GetDataSyncActionEventDiaryList (string CustomerEntityID, string AccountEntityID)
		{
			try {
				string strActionDetails = SQLiteDatabase.Instance.GetDataSyncActionDetails () [0].Data.ToDecrypt ();
				DataSyncActionDetailsModel dataSyncActionDetailsModel = JsonConvert.DeserializeObject<DataSyncActionDetailsModel> (strActionDetails);

				List<ActionEventDiaryListModel> listActionEventDiary = null;
				if (!(string.IsNullOrEmpty (AccountEntityID) || AccountEntityID == "0"))
					listActionEventDiary = (from ActionEvent in dataSyncActionDetailsModel.GetActionEventDiaryList
					                        where ActionEvent.CustomerEntityID == CustomerEntityID && ActionEvent.TableName == "GetActionEventDiaryList" && ActionEvent.AccountEntityID == AccountEntityID
					                        select ActionEvent).ToList ();

				listActionEventDiary = (from ActionEvent in dataSyncActionDetailsModel.GetActionEventDiaryList
				                        where ActionEvent.CustomerEntityID == CustomerEntityID && ActionEvent.TableName == "GetActionEventDiaryList"
				                        select ActionEvent).ToList ();

				foreach (ActionEventUpdate actionEventUpdate in SQLiteDatabase.Instance.DataSyncGetActionEventList()) {
					string EventId = actionEventUpdate.EventID.ToString ();

					string Status = string.Empty;
					if (actionEventUpdate.Status == 20)
						Status = "Allotted";
					else if (actionEventUpdate.Status == 30)
						Status ="In Process";
					else if (actionEventUpdate.Status == 10)
						Status ="Done";
					
					listActionEventDiary.Where (item => item.EventID == EventId).ToList () [0].Status = Status;
				}

				return listActionEventDiary;
			} catch {
				return null;
			}
		}

		public static List<ActionEventModel> GetDataSyncActionEventDiaryDetails ()
		{
			try {
				string strEventDiaryDetails = SQLiteDatabase.Instance.GetDataSyncActionDetails () [0].Data.ToDecrypt ();
				DataSyncActionDetailsModel dataSyncActionDetailsModel = JsonConvert.DeserializeObject<DataSyncActionDetailsModel> (strEventDiaryDetails);

				List<ActionEventModel> listActionEventDiary = null;
				listActionEventDiary = (from ActionEventDairy in dataSyncActionDetailsModel.ActionEventDiaryDetails
				        where ActionEventDairy.EventID.ToString () == GlobalVariables.RemarkID
				        select ActionEventDairy).ToList ();

				foreach (ActionEventUpdate actionEventUpdate in SQLiteDatabase.Instance.DataSyncGetActionEvent(GlobalVariables.RemarkID)) {
					int EventId = actionEventUpdate.EventID;

					ActionEventModel AEU = listActionEventDiary.Where (item => item.EventID == EventId).ToList () [0];

					AEU.Status = actionEventUpdate.Status.ToString();
					AEU.CommencementDate = actionEventUpdate.CommencementDt;
					AEU.CommenceComment = actionEventUpdate.CommencementRemark;
					AEU.ClosureDate =actionEventUpdate.ClosureDt;
					AEU.ClosureComment = actionEventUpdate.ClosureRemark;

				}

				return listActionEventDiary;
			} catch {
				return null;
			}
		}

		public static bool DataSyncActionActionEventUpdate (ActionEventUpdate actionEventUpdate)
		{
			try {
				return SQLiteDatabase.Instance.DataSyncUpdateActionEvent (actionEventUpdate);
			} catch {
				return false;
			}
		}
	}


	public class ServerToLocalDataSync
	{
		public ServerToLocalDataSync ()
		{
			
		}

		public async Task<bool> Process (List<CustomerListDataSyncModel> listCustomer)
		{
			try {

				string CustomerEntityList = string.Empty;
				foreach (CustomerListDataSyncModel Customer in listCustomer) {
					CustomerEntityList = CustomerEntityList + "," + Customer.CustomerEntityID;
				}
				CustomerEntityList = CustomerEntityList.Remove (0, 1);

				List<ServerToLocalModel> listServerToLocal = new List<ServerToLocalModel> ();
				
				string resultCustomerDetailsSync = await APIRequest.Instance.GetCustomerDetailsSync (new CustomerDetailsSyncRequestModel (){ CustomerEntityID = CustomerEntityList });
				listServerToLocal.Add (new ServerToLocalModel () {
					TableName = "CustomerDetails",
					Data = resultCustomerDetailsSync,
					Status = false
				});
				
				string resultDetailsSync = await APIRequest.Instance.GetDetailsSync (new CustomerDetailsSyncRequestModel (){ CustomerEntityID = CustomerEntityList });
				listServerToLocal.Add (new ServerToLocalModel () {
					TableName = "Details",
					Data = resultDetailsSync,
					Status = false
				});

				string resultActionDetailsSync = await APIRequest.Instance.GetActionDetailsSync (new CustomerDetailsSyncRequestModel (){ CustomerEntityID = CustomerEntityList });
				listServerToLocal.Add (new ServerToLocalModel () {
					TableName = "ActionDetails",
					Data = resultActionDetailsSync,
					Status = false
				});

				List<DataSyncStatusModel> listDataSyncStatusModel = new List<DataSyncStatusModel> ();

				for (int listCustomerIndex = 0; listCustomerIndex < listCustomer.Count; listCustomerIndex++) {
					DataSyncStatusModel objDataSyncStatusModel = new DataSyncStatusModel ();

					objDataSyncStatusModel.BranchCode = GlobalVariables.BranchCode;
					objDataSyncStatusModel.BranchName = GlobalVariables.BranchName;
					objDataSyncStatusModel.CustomerEntityID = listCustomer [listCustomerIndex].CustomerEntityID;
					objDataSyncStatusModel.CustomerID = listCustomer [listCustomerIndex].CustomerID;
					objDataSyncStatusModel.CustomerName = listCustomer [listCustomerIndex].CustomerName;
					objDataSyncStatusModel.ServerToLocalStatus = true;
					objDataSyncStatusModel.LocalToServerStatus = false;
					objDataSyncStatusModel.LocalUpdateStatus = false;

					listDataSyncStatusModel.Add (objDataSyncStatusModel);
				}


				if (SQLiteDatabase.Instance.InsertServerToLocal (listServerToLocal))
				if (SQLiteDatabase.Instance.InsertDataSyncStatusModel (listDataSyncStatusModel))
					return true;
				
				return false;
			} catch {
				return false;
			}

		}


	}
}