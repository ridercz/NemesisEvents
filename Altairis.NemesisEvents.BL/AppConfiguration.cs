using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.NemesisEvents.BL
{
    public class AppConfiguration
    {
        public string BaseUrl { get; set; }

        public string ApplicationName { get; set; }

        public string PageTitleDefault { get; set; }

        public string PageTitleFormat { get; set; }

        public string AmazonAccessKeyId { get; set; }

        public string AmazonSecretAccessKey { get; set; }

        public string MailPickupDirectory { get; set; }

        public string SqlConnectionString { get; set; }

        public string AttachmentsStoragePublicUrl { get; set; }

        public string AttachmentsStorageConnectionString { get; set; }
    }
}
