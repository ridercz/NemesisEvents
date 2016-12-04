﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.Web {
    public class AppConfig {

        public string BaseUrl { get; set; }

        public string ApplicationName { get; set; }

        public string TitleFormatString { get; set; }

        public string AmazonAccessKeyId { get; set; }

        public string AmazonSecretAccessKey { get; set; }

        public string MailPickupDirectory { get; set; }

        public string SqlConnectionString { get; set; }
    }
}
