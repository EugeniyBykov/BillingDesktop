using Quartz;
using Server.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server.Jobs
{
   public class EmailSender : IJob
    {
        DataContex db = new DataContex();

        public async Task Execute(IJobExecutionContext context)
        {
           
                string psw = GetPass();
                DateTime today = DateTime.Now;
                var mails = db.Projects.Where(p=>p.NextPay != null).ToList();
                List<string> sendList = new List<string>();
                

                for (int i = 0; i < mails.Count; i++)
                    if ( DaysBetween((DateTime)mails[i].NextPay, today) == 5)   /* if (DateTime) mails[i].NextPay != null *//*&&*/
                       sendList.Add(mails[i].SendAddress);

                // меняем статус оплаты проектов по которым не внесена оплата 

            for (int i = 0; i < mails.Count; i++)
                if (DaysBetween((DateTime)mails[i].NextPay, today) <= 0)
                    mails[i].Status = "Не оплачен";
            db.SaveChanges(); 

                    foreach (var item in sendList)
            {
               
                using (MailMessage message = new MailMessage("EugeniyBykov@gmail.com", item))
                {
                    message.Subject = "Оплата";
                    message.Body = "Оплата по вашему тарифу закначивается через 5 дней. Во избежания отключения услуг просьба внести оплату";
                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = true,
                        Host = "smtp.gmail.com",
                        Port = 587,
                        Credentials = new NetworkCredential("EugeniyBykov@gmail.com", psw)
                    })
                    {
                        await client.SendMailAsync(message);
                    }
                }         
            }


        }

        private int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d1.Subtract(d2);
            return (int)span.TotalDays;
        }

         private string GetPass()
        {
            string path = @"..\..\Data\pw.txt";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string pw = sr.ReadLine();
            sr.Close();
            fs.Close();
            return pw;
        }
    }
}
