using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
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
          public string NoRW 
          { 
               get{return _norw;} 
               set{ 
                      _norw=value; 
                     OnPropertyChange("NoRW");
                     }
          } 

          [DbColumn("KetuaRW")] 
          public int KetuaRW 
          { 
               get{return _ketuarw;} 
               set{ 
                      _ketuarw=value; 
                     OnPropertyChange("KetuaRW");
                     }
          } 

          [DbColumn("SekertarisRW")] 
          public int SekertarisRW 
          { 
               get{return _sekertarisrw;} 
               set{ 
                      _sekertarisrw=value; 
                     OnPropertyChange("SekertarisRW");
                     }
          } 

          private int  _id;
           private string  _norw;
           private int  _ketuarw;
           private int  _sekertarisrw;
      }
}


