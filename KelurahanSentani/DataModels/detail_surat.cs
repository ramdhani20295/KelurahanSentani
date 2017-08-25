using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("detail_surat")] 
     public class detail_surat:BaseNotifyProperty  
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

          [DbColumn("Approved RT")] 
          public int ApprovedRT 
          { 
               get{return _approvedrt;} 
               set{ 
                      _approvedrt=value; 
                     OnPropertyChange("ApprovedRT");
                     }
          } 

          [DbColumn("ApprovedRW")] 
          public int ApprovedRW 
          { 
               get{return _approvedrw;} 
               set{ 
                      _approvedrw=value; 
                     OnPropertyChange("ApprovedRW");
                     }
          } 

          [DbColumn("ApprovedKelurahan")] 
          public int ApprovedKelurahan 
          { 
               get{return _approvedkelurahan;} 
               set{ 
                      _approvedkelurahan=value; 
                     OnPropertyChange("ApprovedKelurahan");
                     }
          } 

          private int  _id;
           private int  _jenissuratid;
           private string  _nosurat;
           private DateTime  _tanggalbuat;
           private int  _personid;
           private int  _approvedrt;
           private int  _approvedrw;
           private int  _approvedkelurahan;
      }
}


