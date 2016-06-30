using System;
using System.ComponentModel;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace CMA
{
    public class VMLogin : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        public VMLogin()
        {
            ChangeControlState2();
            //ChangeControlState ();
        }

        #endregion

        private static string _PUserID = string.Empty;
        private static string _PPassord = string.Empty;

        public string PUserID
        {
            get { return _PUserID; }
            set
            {
                if ((value != null && value.Length <= 20) || value == null)
                {
                    _PUserID = value;
                }
                OnPropertyChanged("PUserID");
                ChangeControlState();
                ChangeControlState2();
            }
        }
        public string PPassord
        {
            get { return _PPassord; }
            set
            {
                _PPassord = value;
                OnPropertyChanged("PPassord");
                ChangeControlState1();
            }
        }

        private bool _IsPassordEnalble;

        public bool IsPassordEnalble
        {
            get { return _IsPassordEnalble; }
            set
            {
                _IsPassordEnalble = value;
                OnPropertyChanged("IsPassordEnalble");
            }
        }

        private bool _PIsPassword = true;

        public bool PIsPassword
        {
            get { return _PIsPassword; }
            set
            {
                _PIsPassword = value;
                OnPropertyChanged("PIsPassword");
            }

        }

        private bool _PShowPassword = false;

        public bool PShowPassword
        {
            get { return _PShowPassword; }
            set
            {
                _PShowPassword = value;
                OnPropertyChanged("PShowPassword");
                PIsPassword = !value;

            }

        }
        //
        //		private bool _Forget;
        //
        //		public bool Forget {
        //			get{ return _Forget; }
        //			set {
        //				_Forget = value;
        //				OnPropertyChanged ("Forget");
        //			}
        //			
        //		}

        private bool _IsLoginEnalble = false;

        public bool IsLoginEnalble
        {
            get { return _IsLoginEnalble; }
            set
            {
                _IsLoginEnalble = value;
                OnPropertyChanged("IsLoginEnalble");
            }
        }





        public void ChangeControlState()
        {
            if (PUserID == "")
            {

                IsPassordEnalble = false;
                //Forget = false;
                IsLoginEnalble = false;
            }
            else if (PUserID.Length > 0)
            {
                IsPassordEnalble = true;
                //Forget = true;
                //IsLoginEnalble = true;


            }


        }
        public void ChangeControlState1()
        {
            if (PPassord == "")
            {
                IsLoginEnalble = false;
            }
            else if (PPassord.Length > 0)
            {
                IsLoginEnalble = true;
            }
        }
        public void ChangeControlState2()
        {
            if (PUserID == "" && PPassord.Length > 0)
            {
                IsLoginEnalble = false;
            }
            else if (PUserID.Length > 0 && PPassord.Length > 0)
            {
                IsLoginEnalble = true;
            }
        }


        //		public Command ForgetTapped 
        //		{
        //			get { 
        //				return new Command (async delegate(object obj) {
        //					PUserID = "Clicked ";	
        //				});
        //			}
        //		}
    }
}






