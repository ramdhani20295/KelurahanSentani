using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani.Models
{
    public class KelurahanInfo: BaseNotifyProperty
    {
        public string Provinsi { get { return "Papua"; } }
        public string Kecamatan { get { return "Sentani"; } }
        public string Kelurahan { get { return "Sentani Kota"; } }
        public string KodePos { get { return "999352"; } }
        public string Kabupaten { get { return "Kab. Jayapura"; } }

    }
}