using System;
using System.ComponentModel;

namespace RMTFRK.Core.Helper.Net.QMail.EventHandler
{
    public class MailDeletingMailEventArgs : CancelEventArgs
    {

        private Guid _emailID;
        public Guid EmailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }
    }

}
