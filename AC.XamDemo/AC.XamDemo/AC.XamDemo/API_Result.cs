using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AC.XamDemo
{
    // Example domain object used for presentation.  
    public class API_Result : INotifyPropertyChanged
    {
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _GetUserResult;
        public string GetUserResult
        {
            get { return _GetUserResult; }
            set
            {
                if (_GetUserResult != value)
                {
                    _GetUserResult = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _SaveUserResult;
        public string SaveUserResult
        {
            get { return _SaveUserResult; }
            set
            {
                if (_SaveUserResult != value)
                {
                    _SaveUserResult = value;
                    RaisePropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberNameAttribute] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
