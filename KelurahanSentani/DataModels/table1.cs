using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("table1")] 
     public class table1:BaseNotifyProperty  
   {
          [DbColumn("surat_Id")] 
          public int surat_Id 
          { 
               get{return _surat_id;} 
               set{ 
                      _surat_id=value; 
                     OnPropertyChange("surat_Id");
                     }
          } 

          private int  _surat_id;
      }
}


