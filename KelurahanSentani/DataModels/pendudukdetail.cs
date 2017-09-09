using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace KelurahanSentani.DataModels 
{ 
     [TableName("pendudukdetail")] 
     public class pendudukdetail:BaseNotifyProperty  
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

          [DbColumn("StatusPerkawinan")] 
          public StatusPerkawinan StatusPerkawinan 
          { 
               get{return _statusperkawinan;} 
               set{ 
                      _statusperkawinan=value; 
                     OnPropertyChange("StatusPerkawinan");
                     }
          } 

          [DbColumn("HubunganDalamKeluarga")] 
          public Hubungan HubunganDalamKeluarga 
          { 
               get{return _hubungandalamkeluarga;} 
               set{ 
                      _hubungandalamkeluarga=value; 
                     OnPropertyChange("HubunganDalamKeluarga");
                     }
          } 

          [DbColumn("Kewarganegaraan")] 
          public Kewarganegaraan Kewarganegaraan 
          { 
               get{return _kewarganegaraan;} 
               set{ 
                      _kewarganegaraan=value; 
                     OnPropertyChange("Kewarganegaraan");
                     }
          } 

          [DbColumn("Paspor")] 
          public string Paspor 
          { 
               get{return _paspor;} 
               set{ 
                      _paspor=value; 
                     OnPropertyChange("Paspor");
                     }
          }

        [DbColumn("DokumenLain")]
        public string DokumenLain
        {
            get { return _dokumen; }
            set
            {
                _dokumen = value;
                OnPropertyChange("DokumenLain");
            }
        }

        [DbColumn("Ayah")] 
          public string Ayah 
          { 
               get{return _ayah;} 
               set{ 
                      _ayah=value; 
                     OnPropertyChange("Ayah");
                     }
          } 

          [DbColumn("Ibu")] 
          public string Ibu 
          { 
               get{return _ibu;} 
               set{ 
                      _ibu=value; 
                     OnPropertyChange("Ibu");
                     }
          } 

          private int  _id;
           private StatusPerkawinan  _statusperkawinan;
           private Hubungan _hubungandalamkeluarga;
           private Kewarganegaraan _kewarganegaraan;
           private string _dokumen;
           private string  _ayah;
           private string  _ibu;
        private string _paspor;
    }
}


