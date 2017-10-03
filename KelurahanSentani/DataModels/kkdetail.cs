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

          private int  _kartukeluargaid;
           private int  _pendudukid;
      }
}


