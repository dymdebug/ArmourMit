using System;
using System.Configuration;

namespace RMTFRK.Core.Helper.Net.QMail.Config
{
    internal class MailQueueElement : ConfigurationElement
    {

        private MailQueueElement()
        {
        }
        [ConfigurationProperty("queueSize", DefaultValue = int.MaxValue, IsRequired = false)]
        public virtual int QueueSize
        {
            get { return Convert.ToInt32(this["queueSize"]); }
            set { this["queueSize"] = value; }
        }

        [ConfigurationProperty("connectionName", IsRequired = true)]
        public virtual string ConnectionName
        {
            get { return this["connectionName"].ToString(); }
            set { this["connectionName"] = value; }
        }

    }

}
