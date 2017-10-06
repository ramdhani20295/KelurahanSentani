using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace KelurahanSentani.DataModels 
{ 
     [TableName("surat")] 
     public class surat:BaseNotifyProperty  
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

          [DbColumn("NoSurat")] 
          public string NoSurat 
          { 
               get{return _nosurat;} 
               set{ 
                      _nosurat=value; 
                     OnPropertyChange("NoSurat");
                     }
          } 

          [DbColumn("TanggalBuat")] 
          public DateTime TanggalBuat 
          { 
               get{return _tanggalbuat;} 
               set{ 
                      _tanggalbuat=value; 
                     OnPropertyChange("TanggalBuat");
                     }
          } 

          [DbColumn("BerlakuHingga")] 
          public DateTime BerlakuHingga 
          { 
               get{return _berlakuhingga;} 
               set{ 
                      _berlakuhingga=value; 
                     OnPropertyChange("BerlakuHingga");
                     }
          } 

          [DbColumn("PermohonanId")] 
          public int PermohonanId 
          { 
               get{return _permohonanid;} 
               set{ 
                      _permohonanid=value; 
                     OnPropertyChange("PermohonanId");
                     }
          } 

          [DbColumn("UserId")] 
          public string AdminUserId 
          { 
               get{return _AdminId;} 
               set{ 
                      _AdminId=value; 
                     OnPropertyChange("UserId");
                     }
          }


        [DbColumn("JenisSurat")]
        [JsonConverter(typeof(StringEnumConverter))]
        public JenisSurat JenisSurat
        {
            get { return _jenissurat; }
            set
            {
                _jenissurat = value;
                OnPropertyChange("JenisSurat");
            }
        }

        public object DataSurat { get; set; }

        private int  _id;
           private string  _nosurat;
           private DateTime  _tanggalbuat;
           private DateTime  _berlakuhingga;
           private int  _permohonanid;
           private string  _AdminId;
        private JenisSurat _jenissurat;
    }
}


