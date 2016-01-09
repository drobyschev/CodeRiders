
//-----------------------------------------------------------------------
// <copyright file="EmailSettings.cs" company="Code Riders Ltd 2016">
//     Copyright (c) Code Riders. All rights reserved.
// </copyright>
// <author>Andrey Drobyshev</author>
//-----------------------------------------------------------------------

namespace WebStore.Domain.Email
{
    class EmailSettings
    {
        public EmailSettings()
        {
            Username = "SmtpUsername";
            Password = "SmtpPassword";
            Host = "smtp.example.com";
            Port = 587;
            AddressTo = "orders@example.com";
            AddressFrom = "webstore@example.com";
            UseSSL = true;
            WriteAsFile = true;
            FileLocation = @"C:\web_store_emails";
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string AddressTo { get; set; }
        public string AddressFrom { get; set; }
        public bool UseSSL { get; set; }
        public bool WriteAsFile { get; set; }
        public string FileLocation { get; set; }
    }
}
