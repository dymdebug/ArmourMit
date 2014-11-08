using System;
using RMTFRK.Core.Helper.Net.QMail.Base;

namespace RMTFRK.Core.Helper.Net.QMail.EventHandler
{
    /// <summary> 
    /// The custom Event Args is rised while the times that the mail trys to send is exceed. 
    /// </summary> 
    public class MailExceedMailMaxTryTimesEventArgs : EventArgs
    {
        private Email _email;
        public Email Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }

}
