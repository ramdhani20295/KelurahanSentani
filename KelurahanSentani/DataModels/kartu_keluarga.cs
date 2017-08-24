using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("kartu_keluarga")] 
     public class kartu_keluarga:BaseNotifyProperty  
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

          [DbColumn("NoKK")] 
          public string NoKK 
          { 
               get{return _nokk;} 
               set{ 
                      _nokk=value; 
                     OnPropertyChange("NoKK");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
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

          [DbColumn("RTId")] 
          public int RTId 
          { 
               get{return _rtid;} 
               set{ 
                      _rtid=value; 
                     OnPropertyChange("RTId");
                     }
          } 

          [DbColumn("KodePos")] 
          public string KodePos 
          { 
               get{return _kodepos;} 
               set{ 
                      _kodepos=value; 
                     OnPropertyChange("KodePos");
                     }
          } 

          private int  _id;
           private string  _nokk;
           private string  _alamat;
           private int  _rwid;
           private int  _rtid;
           private string  _kodepos;
      }
}


