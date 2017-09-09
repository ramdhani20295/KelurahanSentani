using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani.DataModels
{
    public class KelurahanInfo:BaseNotifyProperty  
    {
        public string Kelurahan{get { return "Bandara"; }}
        public string Kecamatan { get { return "Sentani"; } }
        public string Kabupaten { get { return "Kabupaten Jayapura"; } }
        public string Provinsi { get { return "Papua"; } }
        public string KodePos { get { return "991232"; } }
    }
}