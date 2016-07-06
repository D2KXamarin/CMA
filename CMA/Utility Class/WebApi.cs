using System;
using System.Threading.Tasks;

using System.Text;
using ModernHttpClient;
using System.Net.Http;

namespace CMA
{
    public class WebAPI
    {
        private string webLinkHost = "http://103.1.114.64:8011/CMAAPI/";
//        private HttpClient _client;
		public readonly string webLogin;
		public readonly string webLinkChangePassword;

        public readonly string webLinkBranchList;

        public readonly string webLinkCustomerList;
        public readonly string webLinkLoanDetails;
        public readonly string webLinkGetCoBorrGuarDetailsList;
        public readonly string webLinkSecurityDetails;
        public readonly string webLinkRecoveryDetails;
        public readonly string webLinkGetCustomeDetails;
        public readonly string webLinkActionEventDiaryDetails;
        public readonly string webLinkDefaultStatus;
        public readonly string webLinkRemarkInsertUpdate;
        public readonly string webLinkPreviousRemarksList;
        public readonly string webLinkCustomerReAllocationLoad;
        public readonly string webLinkCustomerReAllocationUpdate;

        public readonly string webLinkActionEventDiaryList;
        public readonly string webLinkActionEventUpdate;

		public readonly string webLinkAssignActionInsertUpdate;
		public readonly string webLinkAssignAction;

		public readonly string webLinkCustomerDetailsSync;
		public readonly string webLinkDetailsSync;
		public readonly string webLinkActionDetailsSync;
		public readonly string webLinkSecurityGoldDetail;
		public readonly string webLinkSecurityGoldDetailUpdate;

		public readonly string webLinkSecurityVehicleDetail;
		public readonly string webLinkSecurityVehicleDetailUpdate;
		public readonly string webLinkSecurityVehicleInsuranceCoDetails;


		public readonly string webLinkSecurityShareDetails;
		public readonly string webLinkSecurityPropertyDetails;
		public readonly string webLinkSecurityShareDetailUpdate;
		public readonly string webLinkSecurityPropertyDetailUpdate;

        private static WebAPI _instance = null;

        private WebAPI()
        {
			webLogin = webLinkHost + "CrisMAc/LoginAuth/";
			webLinkChangePassword = webLinkHost + "CrisMAc/App_ChangePassword";

            webLinkBranchList = webLinkHost + "CrisMAc/App_GetBranchList";

            webLinkCustomerList = webLinkHost + "CrisMAc/APP_GetCustomeList";
            webLinkLoanDetails = webLinkHost + "CrisMAc/APP_GetLoanDetails";
            webLinkGetCoBorrGuarDetailsList = webLinkHost + "CrisMAc/APP_GetCoBorrGuar";
            webLinkSecurityDetails = webLinkHost + "CrisMAc/APP_GetSecurityDetails";
            webLinkRecoveryDetails = webLinkHost + "CrisMAc/APP_GetRecoveryDetails";
            webLinkGetCustomeDetails = webLinkHost + "CrisMAc/APP_GetCustomeDetails";

            webLinkActionEventDiaryList = webLinkHost + "CrisMAc/APP_GetActionEventDiaryList";
            webLinkDefaultStatus = webLinkHost + "CrisMAc/APP_DefaultStatus";
            webLinkRemarkInsertUpdate = webLinkHost + "CrisMAc/APP_RemarkInsertUpdate";
			webLinkPreviousRemarksList = webLinkHost + "CrisMAc/APP_GetPreviousRemark";

            webLinkActionEventDiaryDetails = webLinkHost + "CrisMAc/APP_GetActionEventDiaryDetails";
            webLinkActionEventUpdate = webLinkHost + "CrisMAc/APP_ActionEventUpdate";

            webLinkCustomerReAllocationLoad = webLinkHost + "CrisMAc/APP_StakeHolderList";
            webLinkCustomerReAllocationUpdate = webLinkHost + "CrisMAc/APP_CustomerReAllocationUpdate";

			webLinkAssignAction = webLinkHost + "CrisMAc/APP_GetAssignAction";
			webLinkAssignActionInsertUpdate = webLinkHost + "CrisMAc/APP_AssignActionInsertUpdate/";

           // _client = new HttpClient(new NativeMessageHandler() { DisableCaching = true });

			webLinkCustomerDetailsSync = webLinkHost + "CrisMAc/APP_GetCustomerDetailsSync";
			webLinkDetailsSync = webLinkHost + "CrisMAc/APP_GetDetailsSync";
			webLinkActionDetailsSync = webLinkHost + "CrisMAc/APP_GetActionDetailsSync";

			webLinkSecurityGoldDetail = webLinkHost + "CrisMAc/APP_GetSecurityGoldDetail";
			webLinkSecurityGoldDetailUpdate = webLinkHost + "CrisMAc/APP_SecurityGoldDetailInsertUpdate";

			webLinkSecurityVehicleDetail = webLinkHost + "CrisMAc/APP_GetSecurityVehicleDetail";
			webLinkSecurityVehicleDetailUpdate = webLinkHost + "CrisMAc/APP_SecurityVehicleDetailInsertUpdate";
			webLinkSecurityVehicleInsuranceCoDetails = webLinkHost + "CrisMAc/App_GetInsuranceCompanyList";


			webLinkSecurityShareDetails = webLinkHost + "CrisMAc/APP_GetSecurityShareDetail";
			webLinkSecurityPropertyDetails = webLinkHost + "CrisMAc/APP_GetSecurityPropertyDetail";
			webLinkSecurityShareDetailUpdate = webLinkHost + "CrisMAc/APP_SecurityShareDetailInsertUpdate";
			webLinkSecurityPropertyDetailUpdate = webLinkHost + "CrisMAc/APP_SecurityPropertyDetailInsertUpdate";
        }
        private static object locker = new object();

        public static WebAPI Instance
        {
            get
            {
                lock (locker)
                {
                    if (_instance == null)
                        _instance = new WebAPI();
                }

                return _instance;
            }
        }
        /// <summary>
        /// Https the request process.
        /// </summary>
        /// <returns>Process the Http Request.</returns>
        /// <param name="Method">Http Request Method.</param>
        /// <param name="URL">Http Request URL</param>
        /// <param name="content">Parameters to POST request.</param>
        public async Task<string> HttpRequestProcess(string Method, string URL, string content)
        {
           var client = new HttpClient();

            var WebURL = new Uri(string.Format(URL, string.Empty));

            try
            {
                // For GET request.
                if (Method.ToUpper() == "GET")
                {
					var response = await client.GetAsync(WebURL);
                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();

                        return content;
                    }
                    else
                        return response.IsSuccessStatusCode.ToString();
                }
                // For POST request.
                else if (Method.ToUpper() == "POST")
                {
//
//                    //client.BaseAddress = new Uri(webLinkHost);
//
//
//                    string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}";

					var Parameters = new StringContent(content, Encoding.UTF8, "application/json");
                    //var Parameters = new StringContent (content, Encoding.UTF8,"application/json");
					var response = await client.PostAsync(URL, Parameters);

                    //return response;
                    //response.IsSuccessStatusCode
                    if (true)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        return content;
                    }
                    else
                        return response.IsSuccessStatusCode.ToString();
                }
                else
                    return "Error : Invalid Request Method ";

            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                //client = null;
            }
        }
    }
}



