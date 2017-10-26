using KelurahanSentani.DataModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace KelurahanSentani.Apis
{
    public class ApiHelper
    {
        public static PersetujuanCompleted CekPersetujuan(int idPermohonan, LevelStruktur pejabatlevel)
        {
            var model = new PersetujuanCompleted();
            model.NotApproved = new List<LevelStruktur>();
            model.Approved = new List<LevelStruktur>();
            using (var db = new OcphDbContext())
            {
                var persetujuans = from a in db.Persetujuan.Where(O => O.PermohonanId == idPermohonan)
                                   join b in db.Pejabat.Select() on a.IdPejabat equals b.Id
                                   select new persetujuan { Id = a.Id, IdPejabat = a.IdPejabat, PermohonanId = idPermohonan, Pejabat = b };

                foreach(LevelStruktur item in typeof(LevelStruktur).GetEnumValues())
                {
                    var dataIsFound = persetujuans.Where(O => O.Pejabat.Level == item).FirstOrDefault();
                    if (dataIsFound != null)
                    {
                        model.Approved.Add(item);
                        if (item == pejabatlevel)
                            model.IAproved = true;
                    }
                    else
                        model.NotApproved.Add(item);
                }
                if (model.NotApproved.Count <=0)
                    model.IsCompleted = true;
            }
            return model;
        }
        public static PersetujuanCompleted CekPersetujuan(int idPermohonan)
        {
            var model = new PersetujuanCompleted();
            model.NotApproved = new List<LevelStruktur>();
            model.Approved = new List<LevelStruktur>();
            using (var db = new OcphDbContext())
            {
                var persetujuans = from a in db.Persetujuan.Where(O => O.PermohonanId == idPermohonan)
                                   join b in db.Pejabat.Select() on a.IdPejabat equals b.Id
                                   select new persetujuan { Id = a.Id, IdPejabat = a.IdPejabat, PermohonanId = idPermohonan, Pejabat = b };

                foreach (LevelStruktur item in typeof(LevelStruktur).GetEnumValues())
                {
                    var dataIsFound = persetujuans.Where(O => O.Pejabat.Level == item).FirstOrDefault();
                    if (dataIsFound != null)
                    {
                        model.Approved.Add(item);
                    }
                    else
                        model.NotApproved.Add(item);
                }
                if (model.NotApproved.Count <= 0)
                    model.IsCompleted = true;
            }
            return model;
        }

        public static Task SendEmailAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            try
            {
                string email = "aplikasikelurahan@gmail.com";
                string password = "r20021995";
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(message.Destination));
                msg.Subject = message.Subject;
                msg.Body = message.Body;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

            return Task.FromResult(0);
        }
    }


    public class PersetujuanCompleted
    {
        public bool IsCompleted { get; set; }
        public List<LevelStruktur> NotApproved { get; set; }
        public List<LevelStruktur> Approved { get; set; }
        public bool IAproved { get; internal set; }
    }

}