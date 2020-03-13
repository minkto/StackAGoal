using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StackAGoal.Services
{
    /// <summary>
    /// This service will be used to actually setup and send
    /// emails.
    /// </summary>
    public class EmailService : IEmailSender
    {
        private string host;
        private string username;
        private string password;
        private int port;
        private bool enableSSL;

        /// <summary>
        /// Creates an instance of the Email Service.
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="port">Port Number</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="enableSSL">Enable SSL</param>
        public EmailService(string host,int port,string username,string password, bool enableSSL)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
            this.enableSSL = enableSSL;
        }

        /// <summary>
        /// This will send an email using the configuration that has been setup for this 
        /// service.
        /// </summary>
        /// <param name="email">Email to send to.</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="htmlMessage">HTML content</param>
        /// <returns>Returns a task which sends the email.</returns>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(host, port);
                smtpClient.EnableSsl = enableSSL;
                smtpClient.Credentials = new NetworkCredential(username, password);

                MailMessage mail = new MailMessage(username, email, subject, htmlMessage);
                mail.IsBodyHtml = true;

                return smtpClient.SendMailAsync(mail);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }       
        }
    }
}
