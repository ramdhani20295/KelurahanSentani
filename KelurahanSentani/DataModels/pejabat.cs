using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KelurahanSentani.DataModels 
{ 
     [TableName("pejabat")] 
     public class pejabat:BaseNotifyProperty  
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

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
                     }
          } 

          [DbColumn("Level")] 
          [JsonConverter(typeof(StringEnumConverter))]
          public LevelStruktur Level 
          { 
               get{return _level;} 
               set{ 
                      _level=value; 
                     OnPropertyChange("Level");
                     }
          } 

          [DbColumn("InstansiID")] 
          public int InstansiID 
          { 
               get{return _instansiid;} 
               set{ 
                      _instansiid=value; 
                     OnPropertyChange("InstansiID");
                     }
          } 

          [DbColumn("Jabatan")] 
          public string Jabatan 
          { 
               get{return _jabatan;} 
               set{ 
                      _jabatan=value; 
                     OnPropertyChange("Jabatan");
                     }
          } 

          [DbColumn("Status")] 
          public string Status 
          { 
               get{return _status;} 
               set{ 
                      _status=value; 
                     OnPropertyChange("Status");
                     }
          } 

          [DbColumn("usersId")] 
          public string usersId 
          { 
               get{return _usersid;} 
               set{ 
                      _usersid=value; 
                     OnPropertyChange("usersId");
                     }
          }

        public object Instansi { get; set; }

        private int  _id;
           private string  _nama;
           private string  _alamat;
           private LevelStruktur  _level;
           private int  _instansiid;
           private string  _jabatan;
           private string  _status;
           private string  _usersid;
      }
}


