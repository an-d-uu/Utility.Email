using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;

namespace Utility.Email
{
    public class SendEmail
    {
        public static void Send(string mailToAddresses, string mailFromAddress, string mailSubject, string mailBody, string mailHost, string file)
        {
            mailObject mail = new mailObject
            {
                mailAttachmentPath = new List<string>
                {
                    file
                },
                mailToAddresses = mailToAddresses,
                mailFromAddress = mailFromAddress,
                mailSubject = mailSubject,
                mailBody = mailBody,
                smtpHost = mailHost
            };

            Send(mail);
        }

        public static void Send(mailObject mail)
        {
            string from = mail.mailFromAddress;
            MailMessage message = new MailMessage();
            foreach (string item in mail.mailToAddresses.Split(';')) { message.To.Add(new MailAddress(item.Trim())); }
            message.From = new MailAddress(from);
            message.Subject = mail.mailSubject;
            message.Body = mail.mailBody;

            message = addAttachments(message, mail.mailAttachmentPath);

            SmtpClient client = new SmtpClient(mail.smtpHost);
            // Credentials are necessary if the server requires the client
            // to authenticate before it will send email on the client's behalf.
            client.UseDefaultCredentials = mail.smtpUseDefaultCredentials;

            Send(message, client);
        }

        public static void Send(mailObject mail, SmtpClient smtpClient)
        {
            string from = mail.mailFromAddress;
            MailMessage message = new MailMessage();
            foreach (string item in mail.mailToAddresses.Split(';')) { message.To.Add(new MailAddress(item.Trim())); }
            message.From = new MailAddress(from);
            message.Subject = mail.mailSubject;
            message.Body = mail.mailBody;

            message = addAttachments(message, mail.mailAttachmentPath);

            Send(message, smtpClient);
        }
        public static void Send(MailMessage mailMessage, SmtpClient smtpClient, List<string> mailAttachmentPath)
        {
            mailMessage = addAttachments(mailMessage, mailAttachmentPath);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch
            {
                throw;
            }
        }

        public static void Send(MailMessage mailMessage, SmtpClient smtpClient)
        {
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch
            {
                throw;
            }
        }

        private static MailMessage addAttachments(MailMessage mailMessage, List<string> mailAttachmentPath)
        {
            MailMessage returnMessage = mailMessage;


            if (!(mailAttachmentPath == null))
            {
                foreach (string file in mailAttachmentPath)
                {
                    try
                    {
                        if (System.IO.File.Exists(file))
                        {
                            // Create  the file attachment for this email message.
                            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                            // Add time stamp information for the file.
                            ContentDisposition disposition = data.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(file);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                            // Add the file attachment to this email message.
                            returnMessage.Attachments.Add(data);
                        }
                    }
                    catch
                    {
                        //suppress errors for right now
                        returnMessage = mailMessage;
                    }
                }
            }

            return returnMessage;
        }
    }
}
