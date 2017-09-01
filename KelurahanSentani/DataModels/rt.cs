using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("rt")] 
     public class rt:BaseNotifyProperty  
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

          [DbColumn("NoRT")] 
          public string Nama
          { 
               get{return _nort;} 
               set{ 
                      _nort=value; 
                     OnPropertyChange("NoRT");
                     }
          } 

          [DbColumn("RWId")] 
          public int RWId 
          { 
               get{return _rwid;} 
               set{ 
                      _rwid=value; 
                     OnPropertyChange("RWId");
                     }
          } 

          [PrimaryKey("PejabatId")] 
          [DbColumn("PejabatId")] 
          public int PejabatId 
          { 
               get{return _pejabatid;} 
               set{ 
                      _pejabatid=value; 
                     OnPropertyChange("PejabatId");
                     }
          }

        public pejabat Pejabat { get; internal set; }

        private int  _id;
           private string  _nort;
           private int  _rwid;
           private int  _pejabatid;
      }
}


