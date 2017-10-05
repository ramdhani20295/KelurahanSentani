using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("anggotapindah")] 
     public class anggotapindah:BaseNotifyProperty  
   {
          [PrimaryKey("idAnggotaPindah")] 
          [DbColumn("idAnggotaPindah")] 
          public int idAnggotaPindah 
          { 
               get{return _idanggotapindah;} 
               set{ 
                      _idanggotapindah=value; 
                     OnPropertyChange("idAnggotaPindah");
                     }
          } 

          [DbColumn("suratid")] 
          public int surat_id 
          { 
               get{return _surat_id;} 
               set{ 
                      _surat_id=value; 
                     OnPropertyChange("suratid");
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


        [DbColumn("PermohonanId")]
        public int PermohonanId
        {
            get { return _permohonanId; }
            set
            {
                _permohonanId = value;
                OnPropertyChange("PermohonanId");
            }
        }

        public penduduk Penduduk { get;  set; }

        private int  _idanggotapindah;
           private int  _surat_id;
           private string  _nik;
        private int _permohonanId;
    }
}


