using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
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

          [DbColumn("JenisSuratID")] 
          public int JenisSuratID 
          { 
               get{return _jenissuratid;} 
               set{ 
                      _jenissuratid=value; 
                     OnPropertyChange("JenisSuratID");
                     }
          } 

          [DbColumn("NoSurat")] 
          public string NoSurat 
          { 
               get{return _nosurat;} 
               set{ 
                      _nosurat=value; 
                     OnPropertyChange("NoSurat");
                     }
          } 

          [DbColumn("TanggalBuat")] 
          public DateTime TanggalBuat 
          { 
               get{return _tanggalbuat;} 
               set{ 
                      _tanggalbuat=value; 
                     OnPropertyChange("TanggalBuat");
                     }
          } 

          [DbColumn("PersonID")] 
          public int PersonID 
          { 
               get{return _personid;} 
               set{ 
                      _personid=value; 
                     OnPropertyChange("PersonID");
                     }
          } 

          [DbColumn("BerlakuHingga")] 
          public DateTime BerlakuHingga 
          { 
               get{return _berlakuhingga;} 
               set{ 
                      _berlakuhingga=value; 
                     OnPropertyChange("BerlakuHingga");
                     }
          } 

          private int  _id;
           private int  _jenissuratid;
           private string  _nosurat;
           private DateTime  _tanggalbuat;
           private int  _personid;
           private DateTime  _berlakuhingga;
      }
}


