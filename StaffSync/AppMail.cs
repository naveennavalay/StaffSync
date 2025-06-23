using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace StaffSync
{
    public static class AppMail
    {
    //    myDBClass objDBClass = new myDBClass();
    //    OleDbConnection conn = null;
    //    DataSet dtDataset;
    //    clsStates objState = new clsStates();
    //    clsCountries objCountry = new clsCountries();

        public static void SendMail()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress("nnavalay@phlexglobal.com"));
            mail.From = new MailAddress("noreplay@phlexglobal.com", "No Replay");

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.office365.com";
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new NetworkCredential("generic@phlexglobal.com", "Cunesoft2017!");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            mail.Subject = "Test Mail";
            mail.Body = "Test Mail Body";
            mail.IsBodyHtml = true;
            client.Send(mail);

            //using (MailMessage mail1 = new MailMessage())
            //{
            //    mail1.From = new MailAddress("naveendnavalay@gmail.com");
            //    mail1.To.Add(new MailAddress("naveendnavalay@gmail.com"));
            //    mail1.Subject = "Test Mail";
            //    mail1.Body = "My Test Mail Body";
            //    mail1.IsBodyHtml = true;
            //    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
            //    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            //    {
            //        smtp.Credentials = new NetworkCredential("naveendnavalay@gmail.com", "Naveen_01");
            //        smtp.EnableSsl = true;
            //        smtp.Send(mail1);
            //    }
            //}
        }
    }
}
