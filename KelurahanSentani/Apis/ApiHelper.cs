using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }


    public class PersetujuanCompleted
    {
        public bool IsCompleted { get; set; }
        public List<LevelStruktur> NotApproved { get; set; }
        public List<LevelStruktur> Approved { get; set; }
        public bool IAproved { get; internal set; }
    }

}