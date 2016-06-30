using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace CMA
{
	public class VMActionEventDiary : INotifyPropertyChanged
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

		public int EventID = 0;

		static string _PAssignedAction = string.Empty;

		public string PAssignedAction {
			get { return _PAssignedAction; }
			set {
				_PAssignedAction = value;
				OnPropertyChanged ("PAssignedAction");
			}
		}

		private int _PStatusIndex;

		public int PStatusIndex {
			get {
				return _PStatusIndex;
			}
			set {

				_PStatusIndex = value;
				OnPropertyChanged ("PStatusIndex");

			}
		}

		private DateTime _PCommecementDate;

		public DateTime PCommecementDate {
			get {
				return _PCommecementDate;
			}
			set {
				_PCommecementDate = value;
				OnPropertyChanged ("PCommecementDate");
			}
		}

		private bool _IsCommecementDateEnalble = false;

		public bool IsCommecementDateEnalble {
			get { return _IsCommecementDateEnalble; }
			set {
				_IsCommecementDateEnalble = value;
				OnPropertyChanged ("IsCommecementDateEnalble");
			}
		}

		private static string _CommencementComment = string.Empty;

		public string CommencementComment {
			get { return _CommencementComment; }
			set {
				_CommencementComment = value;
				OnPropertyChanged ("CommencementComment");

			}
		}

		private bool _IsEnalbleCommencementComment = false;

		public bool IsEnalbleCommencementComment {
			get { return _IsEnalbleCommencementComment; }
			set {
				_IsEnalbleCommencementComment = value;
				OnPropertyChanged ("IsEnalbleCommencementComment");
			}
		}

		private bool _IsEnalbleClosureDate = false;

		public bool IsEnalbleClosureDate {
			get { return _IsEnalbleClosureDate; }
			set {
				_IsEnalbleClosureDate = value;
				OnPropertyChanged ("IsEnalbleClosureDate");
			}
		}

		private static string _ClosureComment = string.Empty;

		public string ClosureComment {
			get { return _ClosureComment; }
			set {
				_ClosureComment = value;
				OnPropertyChanged ("ClosureComment");

			}
		}

		private bool _IsEnalbleClosureComment = false;

		public bool IsEnalbleClosureComment {
			get { return _IsEnalbleClosureComment; }
			set {
				_IsEnalbleClosureComment = value;
				OnPropertyChanged ("IsEnalbleClosureComment");
			}
		}

		private DateTime _PCloserDate;

		public DateTime PCloserDate {
			get {
				return _PCloserDate;
			}
			set {
				_PCloserDate = value;
				OnPropertyChanged ("PCloserDate");

			}
		}

		public List<EventStatusModel> StatusMaster = null;

		public void ChangeControlStatein ()
		{
			if (PStatusIndex == 1) {
				IsCommecementDateEnalble = true;
				IsEnalbleCommencementComment = true;
			} else {
				IsCommecementDateEnalble = false;
				IsEnalbleCommencementComment = false;
			}
		}

		public void ChangeControlStateCl ()
		{
			if (PStatusIndex == 2) {
				IsEnalbleClosureDate = true;
				IsEnalbleClosureComment = true;
			} else {
				IsEnalbleClosureDate = false;
				IsEnalbleClosureComment = false;
			}
		}

		public async Task LoadActionDiary ()
		{
			ActionEventModel AEM = null;
			try {

				if (GlobalVariables.IsOffline) {
					List<ActionEventModel> listActionEventDiary = DataSync.GetDataSyncActionEventDiaryDetails ();
					if (listActionEventDiary != null)
						AEM = listActionEventDiary [0];

				} else {
					var result = await APIRequest.Instance.GetActionEventDiaryDetailsAPI ();
					if (result != null) {
						ActionEventDiaryResponseModel responseModelList = JsonConvert.DeserializeObject<ActionEventDiaryResponseModel> (result);
						AEM = responseModelList.ActionEventDiaryDetails [0];
					}
				}

				if (AEM != null) {
					EventID = AEM.EventID;
					PAssignedAction = AEM.WhatIsToBeDone;
					String Status = AEM.Status;
					CommencementComment = AEM.CommenceComment;
					ClosureComment = AEM.ClosureComment;

					StatusMaster = new List<EventStatusModel> ();
					if (AEM.Status == "20") {
						StatusMaster.Add (new EventStatusModel () { StatusKey = 20, StatusValue = "Allotted" });
						StatusMaster.Add (new EventStatusModel () { StatusKey = 30, StatusValue = "In Process" });
						//						StatusMaster.Add(new EventStatusModel(){StatusKey = 30,StatusValue = "Done" });
					} else if (AEM.Status == "30") {
						StatusMaster.Add (new EventStatusModel () { StatusKey = 30, StatusValue = "In Process" });
						StatusMaster.Add (new EventStatusModel () { StatusKey = 10, StatusValue = "Done" });
						IsCommecementDateEnalble = true;
						IsEnalbleCommencementComment = true;

					} else if (AEM.Status == "10") {
						StatusMaster.Add (new EventStatusModel () { StatusKey = 10, StatusValue = "Done" });
					}


					if (!string.IsNullOrEmpty (AEM.CommencementDate)) {
						PCommecementDate = Convert.ToDateTime (AEM.CommencementDate);
					}
					if (!string.IsNullOrEmpty (AEM.ClosureDate)) {
						PCloserDate = Convert.ToDateTime (AEM.ClosureDate);
					}
				}

			} catch (Exception ex) {
			}



		}

		private Command _Save = null;

		public Command Save {
			get {
				return _Save ?? new Command (async delegate (object o) {


					ActionEventUpdate RequestModel = new ActionEventUpdate ();
					RequestModel.RemarkID = GlobalVariables.RemarkID;
					RequestModel.EventID = EventID;
					RequestModel.Status = StatusMaster [PStatusIndex].StatusKey;
					RequestModel.CommencementDt = PCommecementDate.ToString ("yyyy/MM/dd");
					RequestModel.CommencementRemark = CommencementComment;
					RequestModel.ClosureDt = PCloserDate.ToString ("yyyy/MM/dd");
					RequestModel.ClosureRemark = ClosureComment;


					if (GlobalVariables.IsOffline) {
						if (DataSync.DataSyncActionActionEventUpdate (RequestModel)) {
							MessagingCenter.Send<VMActionEventDiary> (this, Strings.ActionEvent_Success);
						} else {
							MessagingCenter.Send<VMActionEventDiary> (this, Strings.ActionEvent__FAILURE);
						}
					} else {
						var result = await APIRequest.Instance.ActionEventUpdate (RequestModel);

						if (result != null) {
							MessagingCenter.Send<VMActionEventDiary> (this, Strings.ActionEvent_Success);
						} else {
							MessagingCenter.Send<VMActionEventDiary> (this, Strings.ActionEvent__FAILURE);
						}
					}

					//					ActivityIndicator = false;
				});

			}
		}

	}
}

