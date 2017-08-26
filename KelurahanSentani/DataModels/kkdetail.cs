using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("kkdetail")] 
     public class kkdetail:BaseNotifyProperty  
   {
          [DbColumn("KartuKeluargaId")] 
          public int KartuKeluargaId 
          { 
               get{return _kartukeluargaid;} 
               set{ 
                      _kartukeluargaid=value; 
                     OnPropertyChange("KartuKeluargaId");
                     }
          } 

          [DbColumn("PendudukId")] 
          public int PendudukId 
          { 
               get{return _pendudukid;} 
               set{ 
                      _pendudukid=value; 
                     OnPropertyChange("PendudukId");
                     }
          } 

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

          private int  _kartukeluargaid;
           private int  _pendudukid;
           private int  _id;
      }
}


