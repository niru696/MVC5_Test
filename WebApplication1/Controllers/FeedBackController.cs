using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FeedBackController : Controller
    {
        //public static object WebConfig { get; private set; }
        //public static string EmailEnvironment => System.Collections.Generic.GetValue<string>(LocalEnvironmentEmailIDt);
        // GET: FeedBack
        [HttpPost]
        public ActionResult SendFeedbackData(FeedBackModel model)
        {
            string htmlstring = "<div><strong>Rating :  </strong>" + model.rating + "<br /><strong>Like :  </strong>" + model.YLike + "<br /><strong>Improvement :  </strong>" + model.YSuggestion + "<br /><strong>Name :  </strong>" + model.Name + "<br /><strong>Email :  </strong>" + model.Email + "<br /><strong>Phone :  </strong>" + model.Phone + "<br /> <strong>AccountNo :  </strong>" + model.Account + "<br /></div>";
            SendEmail(ConfigurationManager.AppSettings["To"], ConfigurationManager.AppSettings["CC"], "EMRArchive Feedback", htmlstring);
            return View();
        }

        //public static bool SendEmail(string To = "", string CC = "", string Subject = "", string htmldata = "")
        //{
        //    try
        //    {
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        To = "nkchoudhary696@gmail.com";
        //        CC = "";
        //        //string isLive = WebConfig.EmailEnvironment;

        //        //if (!string.IsNullOrEmpty(isLive))
        //        //{
        //        //    switch ()
        //        //    {
        //        //        case "LOCAL":
        //        //            To = WebConfig.LocalEnvironmentEmailID;
        //        //            CC = "";
        //        //            break;
        //        //        case "NOMAIL":
        //        //            return false;
        //        //    }
        //        //}

        //        MailMessage msg = new MailMessage();
        //        msg.From = new MailAddress("Form");
        //        msg.Subject = Subject;
        //        msg.IsBodyHtml = true;
        //        string[] arrTo = To.Split(',');

        //        foreach (var toaddress in arrTo)
        //        {
        //            if (toaddress.Length > 0)
        //                msg.To.Add(toaddress);
        //        }

        //        if (!string.IsNullOrEmpty(CC))
        //        {
        //            string[] arrCc = CC.Split(',');
        //            foreach (var ccaddress in arrCc)
        //            {
        //                if (ccaddress.Length > 0)
        //                    msg.CC.Add(ccaddress);
        //            }
        //        }
        //        msg.Headers.Add("Content-type", "text/html");

        //        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmldata, null, "text/html");
        //        msg.AlternateViews.Add(htmlView);

        //        SmtpClient client = default(SmtpClient);
        //        client = new SmtpClient();
        //        client.Host = "SMTPEmailHost";//WebConfig.SMTPEmailHost;

        //        bool isOthRequired = "SMTPOth";//WebConfig.SMTPOth;
        //        if (isOthRequired)
        //        {
        //            client.Credentials = new NetworkCredential("SMTPEmailUserName", "SMTPEmailPassword", "SMTPEmailPassword");
        //            //client.UseDefaultCredentials = false;
        //        }
        //        else
        //            client.UseDefaultCredentials = true;

        //        client.EnableSsl = WebConfig.SMTPEmailSSL;
        //        client.Port = WebConfig.SMTPEmailPort;

        //        client.Send(msg);

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public static bool SendEmail(string SentTo, string Text, string Subject = "", string htmldata = "")
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("nkchoudhary696@gmail.com");
            msg.To.Add(SentTo);
            msg.Subject = Subject;
            msg.Body = Text;
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.mail.ru", 25);



            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("n", "TestPassword");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        }
}