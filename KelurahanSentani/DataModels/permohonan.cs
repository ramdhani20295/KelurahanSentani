using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using KelurahanSentani.Apis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KelurahanSentani.DataModels 
{ 
     [TableName("permohonan")] 
     public class permohonan:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("NomorPermohonan")] 
          public string NomorPermohonan 
          { 
               get{return _nomorpermohonan;} 
               set{ 
                      _nomorpermohonan=value; 
                     OnPropertyChange("NomorPermohonan");
                     }
          } 

          [DbColumn("PendudukId")] 
          public int PendudukId 
          { 
               get{return _pendudukid;} 
               set{ 
                      _pendudukid=value; 
                     OnPropertyChange("PendudukId");
                     }
          } 

          [PrimaryKey("RTId")] 
          [DbColumn("RTId")] 
          public int RTId 
          { 
               get{return _rtid;} 
               set{ 
                      _rtid=value; 
                     OnPropertyChange("RTId");
                     }
          } 

          [DbColumn("Isi")] 
          public string Isi 
          { 
               get{return _isi;} 
               set{ 
                      _isi=value; 
                     OnPropertyChange("Isi");
                     }
          } 

          [DbColumn("JenisSurat")] 
          [JsonConverter(typeof(StringEnumConverter))]
          public JenisSurat JenisSurat 
          { 
               get{return _jenissurat;} 
               set{ 
                      _jenissurat=value; 
                     OnPropertyChange("JenisSurat");
                     }
          } 

            [DbColumn("Status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusPermohonan Status
        {
            get { return status; }
            set { status= value;
                OnPropertyChange("Status");
            }
        }


        [DbColumn("EmailPemohon")]
        public string EmailPemohon
        {
            get { return _emailPemohon; }
            set { _emailPemohon = value;
                OnPropertyChange("EmailPemohon");
            }
        }
      
        [DbColumn("Tanggal")]
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; OnPropertyChange("Tanggal"); }
        }

        public penduduk Penduduk { get; set; }
        public PersetujuanCompleted StatusPersetujuan { get;  set; }

        public List<anggotapindah> DataPindah { get; set; }

        private StatusPermohonan status;
        private int  _id;
           private string  _nomorpermohonan;
           private int  _pendudukid;
           private int  _rtid;
           private string  _isi;
           private JenisSurat  _jenissurat;
        private DateTime tanggal;
        private string _emailPemohon;
    }
}


