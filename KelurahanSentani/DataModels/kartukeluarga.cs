using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("kartukeluarga")] 
     public class kartukeluarga:KelurahanInfo
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

          [DbColumn("NoKK")] 
          public string NoKK 
          { 
               get{return _nokk;} 
               set{ 
                      _nokk=value; 
                     OnPropertyChange("NoKK");
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

        
          [PrimaryKey("RTId")] 
          [DbColumn("RTId")] 
          public int RTId 
          { 
               get{return _rtid;} 
               set{ 
                      _rtid=value; 
                     OnPropertyChange("RTId");
                     }
          }

        public List<penduduk> DaftarKeluarga { get;  set; }
        public penduduk KepalaKeluarga { get; set; }
        public rw RW { get;  set; }
        public rt RT { get;  set; }

        private int  _id;
           private string  _nokk;
           private string  _alamat;
           private int  _rtid;
      }
}


