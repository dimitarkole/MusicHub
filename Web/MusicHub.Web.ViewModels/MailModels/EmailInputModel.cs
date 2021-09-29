namespace MusicHub.Web.ViewModels.MailModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EmailInputModel
    {
        public string ToMail { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }
    }
}
