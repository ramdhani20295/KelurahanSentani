using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
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
          public string NoRT 
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

          [DbColumn("KetuaRT")] 
          public int KetuaRT 
          { 
               get{return _ketuart;} 
               set{ 
                      _ketuart=value; 
                     OnPropertyChange("KetuaRT");
                     }
          } 

          [DbColumn("SekertarisRT")] 
          public int SekertarisRT 
          { 
               get{return _sekertarisrt;} 
               set{ 
                      _sekertarisrt=value; 
                     OnPropertyChange("SekertarisRT");
                     }
          } 

          private int  _id;
           private string  _nort;
           private int  _rwid;
           private int  _ketuart;
           private int  _sekertarisrt;
      }
}


