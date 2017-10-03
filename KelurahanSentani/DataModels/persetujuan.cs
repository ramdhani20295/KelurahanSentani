using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
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

          [DbColumn("IdPejabat")] 
          public int IdPejabat 
          { 
               get{return _idpejabat;} 
               set{ 
                      _idpejabat=value; 
                     OnPropertyChange("IdPejabat");
                     }
          } 

          [DbColumn("PermohonanId")] 
          public int PermohonanId 
          { 
               get{return _permohonanid;} 
               set{ 
                      _permohonanid=value; 
                     OnPropertyChange("PermohonanId");
                     }
          }

        public pejabat Pejabat { get;  set; }
        public permohonan Permohonan { get;  set; }

        private int  _id;
           private int  _idpejabat;
           private int  _permohonanid;
      }
}


