using System;
using System.Collections.Generic;
using RMTFRK.Core.Helper.Net.QMail.Base;

namespace RMTFRK.Core.Helper.Net.QMail.EventHandler
{
    /// <summary> 
    /// Custom Event Args for Mail event 
    /// </summary> 
    /// <remarks></remarks> 
    public class MailDequeueMailEventArgs : EventArgs
    {

        private List<Email> _emailList;
        public List<Email> EmailList
        {
            get { return _emailList; }
            set { _emailList = value; }
        }
    }

}
