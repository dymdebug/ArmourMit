using System;

namespace RMTFRK.Core.Helper.Net.QMail.EventHandler
{
    public class MailErrorEventArgs : EventArgs
    {
        private Exception _exception;

        private MailErrorType _errorType;
        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        public MailErrorType ErrorType
        {
            get { return _errorType; }
            set { _errorType = value; }
        }
    }

}
