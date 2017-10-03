using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("rw")] 
     public class rw:BaseNotifyProperty  
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

          [DbColumn("NoRW")] 
          public string Nama
          { 
               get{return _norw;} 
               set{ 
                      _norw=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("PejabatId")] 
          public int PejabatId 
          { 
               get{return _pejabatid;} 
               set{ 
                      _pejabatid=value; 
                     OnPropertyChange("PejabatId");
                     }
          }

        public pejabat Pejabat { get;  set; }
        public List<rt> DaftarRT { get;  set; }
      //  public object Nama { get; internal set; }

        private int  _id;
           private string  _norw;
           private int  _pejabatid;
      }
}


