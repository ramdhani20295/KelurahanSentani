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
     [TableName("penduduk")] 
     public class penduduk:BaseNotifyProperty  
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

          [DbColumn("NIK")] 
          public string NIK 
          { 
               get{return _nik;} 
               set{ 
                      _nik=value; 
                     OnPropertyChange("NIK");
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

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{ 
                      _tempatlahir=value; 
                     OnPropertyChange("TempatLahir");
                     }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{ 
                      _tanggallahir=value; 
                     OnPropertyChange("TanggalLahir");
                     }
          } 

          [DbColumn("Agama")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Kepercayaan Agama 
          { 
               get{return _agama;} 
               set{ 
                      _agama=value; 
                     OnPropertyChange("Agama");
                     }
          } 

          [DbColumn("JK")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Kelamin JK 
          { 
               get{return _jk;} 
               set{ 
                      _jk=value; 
                     OnPropertyChange("JK");
                     }
          } 

          [DbColumn("Pekerjaan")] 
          public string Pekerjaan 
          { 
               get{return _pekerjaan;} 
               set{ 
                      _pekerjaan=value; 
                     OnPropertyChange("Pekerjaan");
                     }
          } 

          [DbColumn("Pendidikan")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Pendidikan Pendidikan 
          { 
               get{return _pendidikan;} 
               set{ 
                      _pendidikan=value; 
                     OnPropertyChange("Pendidikan");
                     }
          }

        public kartukeluarga KartuKeluarga { get; internal set; }

        private int  _id;
           private string  _nik;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private Kepercayaan  _agama;
           private Kelamin  _jk;
           private string  _pekerjaan;
           private Pendidikan  _pendidikan;
      }
}


