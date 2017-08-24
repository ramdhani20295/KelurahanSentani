using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("person_detail")] 
     public class person_detail:BaseNotifyProperty  
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

          [DbColumn("IdPerson")] 
          public int IdPerson 
          { 
               get{return _idperson;} 
               set{ 
                      _idperson=value; 
                     OnPropertyChange("IdPerson");
                     }
          } 

          [DbColumn("StatusPerkawinan")] 
          public string StatusPerkawinan 
          { 
               get{return _statusperkawinan;} 
               set{ 
                      _statusperkawinan=value; 
                     OnPropertyChange("StatusPerkawinan");
                     }
          } 

          [DbColumn("HubunganDalamKeluarga")] 
          public string HubunganDalamKeluarga 
          { 
               get{return _hubungandalamkeluarga;} 
               set{ 
                      _hubungandalamkeluarga=value; 
                     OnPropertyChange("HubunganDalamKeluarga");
                     }
          } 

          [DbColumn("Kewarganegaraan")] 
          public string Kewarganegaraan 
          { 
               get{return _kewarganegaraan;} 
               set{ 
                      _kewarganegaraan=value; 
                     OnPropertyChange("Kewarganegaraan");
                     }
          } 

          [DbColumn("Dokumen")] 
          public string Dokumen 
          { 
               get{return _dokumen;} 
               set{ 
                      _dokumen=value; 
                     OnPropertyChange("Dokumen");
                     }
          } 

          [DbColumn("Ayah")] 
          public string Ayah 
          { 
               get{return _ayah;} 
               set{ 
                      _ayah=value; 
                     OnPropertyChange("Ayah");
                     }
          } 

          [DbColumn("Ibu")] 
          public string Ibu 
          { 
               get{return _ibu;} 
               set{ 
                      _ibu=value; 
                     OnPropertyChange("Ibu");
                     }
          } 

          private int  _id;
           private int  _idperson;
           private string  _statusperkawinan;
           private string  _hubungandalamkeluarga;
           private string  _kewarganegaraan;
           private string  _dokumen;
           private string  _ayah;
           private string  _ibu;
      }
}


