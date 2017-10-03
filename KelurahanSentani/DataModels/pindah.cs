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
          [DbColumn("NoKK")] 
          public string NoKK 
          { 
               get{return _nokk;} 
               set{ 
                      _nokk=value; 
                     OnPropertyChange("NoKK");
                     }
          } 

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

          private string  _nokk;
           private string  _alamatbaru;
           private string  _nik;
           private int  _suratid;
      }
}


