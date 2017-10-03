using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani.Models
{
    public class KelurahanInfo: BaseNotifyProperty
    {
        public string Provinsi { get { return "Papau"; } }
        public string Kecamatan { get { return "Sentani Kota"; } }
        public string Kelurahan { get { return "Hawai"; } }
        public string KodePos { get { return "992123"; } }
        public string Kabupaten { get { return "Kab. Jayapura"; } }

    }
}