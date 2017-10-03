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

          [DbColumn("surat_id")] 
          public int surat_id 
          { 
               get{return _surat_id;} 
               set{ 
                      _surat_id=value; 
                     OnPropertyChange("surat_id");
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

          private int  _idanggotapindah;
           private int  _surat_id;
           private string  _nik;
      }
}


