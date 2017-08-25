using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("person")] 
     public class person:BaseNotifyProperty  
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

          [DbColumn("NIK")] 
          public string NIK 
          { 
               get{return _nik;} 
               set{ 
                      _nik=value; 
                     OnPropertyChange("NIK");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{ 
                      _tempatlahir=value; 
                     OnPropertyChange("TempatLahir");
                     }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{ 
                      _tanggallahir=value; 
                     OnPropertyChange("TanggalLahir");
                     }
          } 

          [DbColumn("Agama")] 
          public string Agama 
          { 
               get{return _agama;} 
               set{ 
                      _agama=value; 
                     OnPropertyChange("Agama");
                     }
          } 

          [DbColumn("JK")] 
          public string JK 
          { 
               get{return _jk;} 
               set{ 
                      _jk=value; 
                     OnPropertyChange("JK");
                     }
          } 

          [DbColumn("Pekerjaan")] 
          public string Pekerjaan 
          { 
               get{return _pekerjaan;} 
               set{ 
                      _pekerjaan=value; 
                     OnPropertyChange("Pekerjaan");
                     }
          } 

          [DbColumn("Pendidikan")] 
          public string Pendidikan 
          { 
               get{return _pendidikan;} 
               set{ 
                      _pendidikan=value; 
                     OnPropertyChange("Pendidikan");
                     }
          } 

          [DbColumn("KKId")] 
          public int KKId 
          { 
               get{return _kkid;} 
               set{ 
                      _kkid=value; 
                     OnPropertyChange("KKId");
                     }
          } 

          private int  _id;
           private string  _nik;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private string  _agama;
           private string  _jk;
           private string  _pekerjaan;
           private string  _pendidikan;
           private int  _kkid;
      }
}


