using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Email
{
    public class mailObject
    {
        public List<string> mailAttachmentPath { get; set; }
        public string mailToAddresses { get; set; }
        public string mailFromAddress { get; set; }
        public string mailSubject { get; set; }
        public string mailBody { get; set; }
        public string smtpHost  { get; set; }
        public bool smtpUseDefaultCredentials { get; set; }

        public mailObject() : base()
        {
            mailAttachmentPath = new List<string>();
            mailToAddresses = string.Empty;
            mailFromAddress = string.Empty;
            mailSubject = string.Empty;
            mailBody = string.Empty;
            smtpHost = string.Empty;
            smtpUseDefaultCredentials = true;
        }
    }
}
