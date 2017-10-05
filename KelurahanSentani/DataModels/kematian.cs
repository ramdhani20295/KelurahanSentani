using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("kematian")] 
     public class kematian:BaseNotifyProperty  
   {
          [DbColumn("surat_Id")] 
          public int surat_Id 
          { 
               get{return _surat_id;} 
               set{ 
                      _surat_id=value; 
                     OnPropertyChange("surat_Id");
                     }
          } 

          [DbColumn("NoKK")] 
          public string NoKK 
          { 
               get{return _nokk;} 
               set{ 
                      _nokk=value; 
                     OnPropertyChange("NoKK");
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

          [DbColumn("tmptkematian")] 
          public string tmptkematian 
          { 
               get{return _tmptkematian;} 
               set{ 
                      _tmptkematian=value; 
                     OnPropertyChange("tmptkematian");
                     }
          } 

          [DbColumn("tglkematian")] 
          public DateTime tglkematian 
          { 
               get{return _tglkematian;} 
               set{ 
                      _tglkematian=value; 
                     OnPropertyChange("tglkematian");
                     }
          } 

          [DbColumn("sebabkematian")] 
          public string sebabkematian 
          { 
               get{return _sebabkematian;} 
               set{ 
                      _sebabkematian=value; 
                     OnPropertyChange("sebabkematian");
                     }
          }

        public surat Surat { get; set; }
        public penduduk Penduduk { get; set; }

        private int  _surat_id;
           private string  _nokk;
           private string  _nik;
           private string  _tmptkematian;
           private DateTime  _tglkematian;
           private string  _sebabkematian;
      }
}


