using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using ModernHttpClient;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net;


namespace CMA
{
	public class APIRequest
	{
		#region Single instance
		private static APIRequest _instance = null;
		private static object locker = new object ();

		public static APIRequest Instance {
			get {
				lock (locker) {
					if (_instance == null)
						_instance = new APIRequest ();
				}

				return _instance;
			}
		}
		#endregion
		public async Task<string> Login (LoginRequestModel loginRequestModel)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON
			string content = JsonConvert.SerializeObject (loginRequestModel);

			string URL = WebAPI.Instance.webLogin + "/" + content.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;
		}

		public async Task<string> GetBranchList (BranchListRequestModel branchListRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (branchListRequestModel);

			string URL = WebAPI.Instance.webLinkBranchList + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;

		}

		public async Task<string> GetCustomerList (CustomerListRequestModel customerListRequestModel)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON
			string PARAM=JsonConvert.SerializeObject(customerListRequestModel);
//			string Parameters = "{\"SearchData\" : \""+SearchValue+"\", \"BranchCode\" : \"\""+GlobalVariables.BranchCode+"\" }";

			string URL = WebAPI.Instance.webLinkCustomerList + "/" + PARAM.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string>  GetRecoveryDetailsList (RecoveryDetailsRequestModel recoveryDetailsRequestModel)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON
			string PARAM=JsonConvert.SerializeObject(recoveryDetailsRequestModel);
//			string Parameters = "{\"CustomerEntityID\" : \""+GlobalVariables.CustomerEntityID+"\", \"AccountEntityID\" : \"\""+GlobalVariables.AccountEntityID+"\" }";

			string URL = WebAPI.Instance.webLinkRecoveryDetails + "/" + PARAM.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}
		public async Task<string>  GetCustomerDetailsList (CustomerDetailsRequestModel customerDetailsRequestModel)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON
			string PARAM=JsonConvert.SerializeObject(customerDetailsRequestModel);
			//			string Parameters = "{\"CustomerEntityID\" : \""+GlobalVariables.CustomerEntityID+"\", \"AccountEntityID\" : \"\""+GlobalVariables.AccountEntityID+"\" }";

			string URL = WebAPI.Instance.webLinkGetCustomeDetails + "/" + PARAM.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}
		public async Task<string>  GetCoBorrGuarDetailsList  (CoBorrGuarDetailsRequestModel coBorrGuarDetailsRequestModel)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON
			string PARAM=JsonConvert.SerializeObject(coBorrGuarDetailsRequestModel);
			//			string Parameters = "{\"CustomerEntityID\" : \""+GlobalVariables.CustomerEntityID+"\", \"AccountEntityID\" : \"\""+GlobalVariables.AccountEntityID+"\" }";

			string URL = WebAPI.Instance.webLinkGetCoBorrGuarDetailsList + "/" + PARAM.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> GetLoanDetailsAPI (LoanDetailsRequestModel loanDetailsRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (loanDetailsRequestModel);

			string URL = WebAPI.Instance.webLinkLoanDetails + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			}
			return result;

		}

		public async Task<string> GetSecurityDetailsAPI (SecurityDetailsRequestModel securityDetailsRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (securityDetailsRequestModel);

			string URL = WebAPI.Instance.webLinkSecurityDetails + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;

		}

		public async Task<string> GetSecurityShareDetailsAPI (SecurityShareDetailRequestModel securityShareDetailRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (securityShareDetailRequestModel);

			string URL = WebAPI.Instance.webLinkSecurityShareDetails + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			}
			return result;

		}

		public async Task<string> GetSecurityPropertyDetailsAPI (PropertyDetailsRequestModel propertyDetailsRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (propertyDetailsRequestModel);

			string URL = WebAPI.Instance.webLinkSecurityPropertyDetails + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			}
			return result;

		}

		public async Task<string> GetActionEventDiaryListAPI (ActionEventDiaryListRequestModel actionEventDiaryDetailsRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (actionEventDiaryDetailsRequestModel);

			string URL = WebAPI.Instance.webLinkActionEventDiaryList + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;

		}
	
		public async Task<string> GetDefaultStatusMaster()
		{
			string URL = WebAPI.Instance.webLinkDefaultStatus;

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;
		}

		public async Task<string> RemarkInsertUpdate(RemarkInsertUpdateRequestModel remarkInsertUpdateRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (remarkInsertUpdateRequestModel);

			string URL = WebAPI.Instance.webLinkRemarkInsertUpdate + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;
		}

		public async Task<string> GetPreviousRemarkList (PreviousRemarkListRequestModel previousRemarkListRequestModel)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON

			string JsonParameters = JsonConvert.SerializeObject (previousRemarkListRequestModel);

			string URL = WebAPI.Instance.webLinkPreviousRemarksList + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}


		public async Task<string> GetLoadStakeholderList (StakeholderListRequestModel stakeholderListRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (stakeholderListRequestModel);

			string URL = WebAPI.Instance.webLinkCustomerReAllocationLoad + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;

		}
		public async Task<string> CustomerReAllocationUpdate(CustomerReallocationRequestModel customerReallocationRequestModel)
		{
			string URL = WebAPI.Instance.webLinkCustomerReAllocationUpdate;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(customerReallocationRequestModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> GetActionEventDiaryDetailsAPI ()
		{


			string Parameters = "{\"RemarkID\" : \"" + GlobalVariables.RemarkID + "\" }";

			string URL = WebAPI.Instance.webLinkActionEventDiaryDetails + "/" + Parameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> ActionEventUpdate  (ActionEventUpdate URequestModel)
		{
			string URL = WebAPI.Instance.webLinkActionEventUpdate + "/" + JsonConvert.SerializeObject(URequestModel).ToEncrypt ();
			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> AssignActionInsertUpdate(AssignActionInsertUpdateRequestModel assignActionInsertUpdateRequestModel)
		{
			string URL = WebAPI.Instance.webLinkAssignActionInsertUpdate;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(assignActionInsertUpdateRequestModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> GetLoadAssignActionStakeholderList (AssignActionRequestModel assignActionRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (assignActionRequestModel);

			string URL = WebAPI.Instance.webLinkAssignAction + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;

		}

		public async Task<string> SecurityShareDetailInsertUpdate(SecurityShareDetailInsertUpdateModel securityShareDetailInsertUpdateModel)
		{
			string URL = WebAPI.Instance.webLinkSecurityShareDetailUpdate + "/" ;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(securityShareDetailInsertUpdateModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;
		}

		public async Task<string> SecurityPropertyDetailInsertUpdate(SecurityPropertyDetailInsertUpdateModel securityPropertyDetailInsertUpdateModel)
		{
			string URL = WebAPI.Instance.webLinkSecurityPropertyDetailUpdate + "/" ;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(securityPropertyDetailInsertUpdateModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;
		}


		#region Sync
		public async Task<string>  GetCustomerDetailsSync(CustomerDetailsSyncRequestModel CustomerDetailsSyncRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (CustomerDetailsSyncRequestModel);

			string URL = WebAPI.Instance.webLinkCustomerDetailsSync + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","");

			return result;

		}

		public async Task<string>  GetDetailsSync(CustomerDetailsSyncRequestModel CustomerDetailsSyncRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (CustomerDetailsSyncRequestModel);

			string URL = WebAPI.Instance.webLinkDetailsSync + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","");

			return result;
		}

		public async Task<string>  GetActionDetailsSync(CustomerDetailsSyncRequestModel CustomerDetailsSyncRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (CustomerDetailsSyncRequestModel);

			string URL = WebAPI.Instance.webLinkActionDetailsSync + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","");

			return result;
		}

		public async Task<string>  GetSecurityGoldDetail(SecurityGoldDetailRequestModel securityGoldDetailRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (securityGoldDetailRequestModel);

			string URL = WebAPI.Instance.webLinkSecurityGoldDetail + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt();

			return result;
		}

		public async Task<string> SecurityGoldDetailInsertUpdate(SecurityGoldDetailInsertUpdateModel securityGoldDetailInsertUpdateModel)
		{
			string URL = WebAPI.Instance.webLinkSecurityGoldDetailUpdate + "/" ;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(securityGoldDetailInsertUpdateModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string>  GetSecurityVehicleDetail(SecurityVehicleDetailRequestModel securityVehicleDetailRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (securityVehicleDetailRequestModel);

			string URL = WebAPI.Instance.webLinkSecurityVehicleDetail + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt();

			return result;
		}

		public async Task<string> GetInsuranceCompanyList (InsuranceCompanyListRequestModel insuranceCompanyListRequestModel)
		{
			string JsonParameters = JsonConvert.SerializeObject (insuranceCompanyListRequestModel);

			string URL = WebAPI.Instance.webLinkSecurityVehicleInsuranceCoDetails + "/" + JsonParameters.ToEncrypt ();

			var result = await WebAPI.Instance.HttpRequestProcess ("GET", URL, null);

			if (result != null)
				return result.Replace("\"","").ToDecrypt ();

			return result;

		}

		public async Task<string> SecurityVehicleDetailInsertUpdate(SecurityVehicleDetailInsertUpdateModel securityVehicleDetailInsertUpdateModel)
		{
			string URL = WebAPI.Instance.webLinkSecurityVehicleDetailUpdate + "/" ;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(securityVehicleDetailInsertUpdateModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> ChangePasswordUpdate(ChangePasswordUpdateModel changePasswordUpdateModel)
		{
			string URL = WebAPI.Instance.webLinkChangePassword ;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(changePasswordUpdateModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		public async Task<string> DataSyncUploadDataToServer(UploadToServerRequestModel uploadToServerRequestModel)
		{
			string URL = WebAPI.Instance.webLinkUploadDataToServerSync;
			var result = await WebAPI.Instance.HttpRequestProcess ("POST", URL, JsonConvert.SerializeObject(uploadToServerRequestModel));
			if (result != null) {
				return result.Replace ("\"", "").ToDecrypt ();
			} 
			return null;

		}

		#endregion

	}
}