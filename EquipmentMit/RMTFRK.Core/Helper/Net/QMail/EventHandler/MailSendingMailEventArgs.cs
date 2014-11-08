using System.ComponentModel;
using RMTFRK.Core.Helper.Net.QMail.Base;

namespace RMTFRK.Core.Helper.Net.QMail.EventHandler
{
    /// <summary> 
    /// Custom Event Args for Mail event 
    /// </summary> 
    public class MailSendingMailEventArgs : CancelEventArgs
    {
        private Email _email;
        public Email Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }

}
