using System;

namespace RMTFRK.Core.Helper.Net.QMail.EventHandler
{
    /// <summary> 
    /// Custom Event Args for Mail event 
    /// </summary> 
    /// <remarks></remarks> 
    public class MailExceedQueueSizeEventArgs : EventArgs
    {

        private int _queueMaxSize;
        public int QueueMaxSize
        {
            get { return _queueMaxSize; }
            set { _queueMaxSize = value; }
        }
    }

}
