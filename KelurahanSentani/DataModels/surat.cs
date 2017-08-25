using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("surat")] 
     public class surat:BaseNotifyProperty  
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

          [DbColumn("KodeSurat")] 
          public string KodeSurat 
          { 
               get{return _kodesurat;} 
               set{ 
                      _kodesurat=value; 
                     OnPropertyChange("KodeSurat");
                     }
          } 

          [DbColumn("JenisSurat")] 
          public string JenisSurat 
          { 
               get{return _jenissurat;} 
               set{ 
                      _jenissurat=value; 
                     OnPropertyChange("JenisSurat");
                     }
          } 

          [DbColumn("FormatSurat")] 
          public string FormatSurat 
          { 
               get{return _formatsurat;} 
               set{ 
                      _formatsurat=value; 
                     OnPropertyChange("FormatSurat");
                     }
          } 

          private int  _id;
           private string  _kodesurat;
           private string  _jenissurat;
           private string  _formatsurat;
      }
}


