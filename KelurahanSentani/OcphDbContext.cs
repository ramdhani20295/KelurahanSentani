using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using KelurahanSentani.DataModels;

namespace KelurahanSentani
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {

            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IRepository<penduduk> Penduduk { get { return new Repository<penduduk>(this); } }
        public IRepository<pendudukdetail> PendudukDetail { get { return new Repository<pendudukdetail>(this); } }
        public IRepository<kartukeluarga> KartuKeluarga { get { return new Repository<kartukeluarga>(this); } }
        public IRepository<kkdetail> KKDetail { get { return new Repository<kkdetail>(this); } }
        public IRepository<rw> RW { get { return new Repository<rw>(this); } }
        public IRepository<rt> RT { get { return new Repository<rt>(this); } }
        public IRepository<pejabat> Pejabat { get { return new Repository<pejabat>(this); } }



        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

      

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
