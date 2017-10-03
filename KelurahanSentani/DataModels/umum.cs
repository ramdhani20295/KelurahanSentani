using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("umum")] 
     public class umum:BaseNotifyProperty
   {
          [DbColumn("surat_Id")] 
          public int SuratId 
          { 
               get{return _surat_id;} 
               set{ 
                      _surat_id=value; 
                     OnPropertyChange("surat_Id");
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

        [DbColumn("Kop")]
        public string Kop
        {
            get { return _kop; }
            set
            {
                _kop = value;
                OnPropertyChange("Kop");
            }
        }


        [DbColumn("keterangan")] 
          public string keterangan 
          { 
               get{return _keterangan;} 
               set{ 
                      _keterangan=value; 
                     OnPropertyChange("keterangan");
                     }
          } 

        public surat Surat { get; set; }
        public penduduk Penduduk { get;  set; }

        private int  _surat_id;
           private string  _nik;
           private string  _keterangan;
        private string _kop;
    }
}


