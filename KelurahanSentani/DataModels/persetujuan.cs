using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("persetujuan")] 
     public class persetujuan:BaseNotifyProperty  
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

          [DbColumn("IdSurat")] 
          public int IdSurat 
          { 
               get{return _idsurat;} 
               set{ 
                      _idsurat=value; 
                     OnPropertyChange("IdSurat");
                     }
          } 

          [DbColumn("IdPejabat")] 
          public int IdPejabat 
          { 
               get{return _idpejabat;} 
               set{ 
                      _idpejabat=value; 
                     OnPropertyChange("IdPejabat");
                     }
          } 

          private int  _id;
           private int  _idsurat;
           private int  _idpejabat;
      }
}


