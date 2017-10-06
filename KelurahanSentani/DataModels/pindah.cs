using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("pindah")] 
     public class pindah:BaseNotifyProperty  
   {
         
          [DbColumn("Alamatbaru")] 
          public string Alamatbaru 
          { 
               get{return _alamatbaru;} 
               set{ 
                      _alamatbaru=value; 
                     OnPropertyChange("Alamatbaru");
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

          [PrimaryKey("SuratId")] 
          [DbColumn("SuratId")] 
          public int SuratId 
          { 
               get{return _suratid;} 
               set{ 
                      _suratid=value; 
                     OnPropertyChange("SuratId");
                     }
          }

        public surat Surat { get; set; }
        public penduduk Penduduk { get;  set; }
        public kartukeluarga KartuKeluarga { get; set; }
        public IEnumerable<anggotapindah> AnggotaPindah { get; set; }

        private string  _nokk;
           private string  _alamatbaru;
           private string  _nik;
           private int  _suratid;
      }
}


