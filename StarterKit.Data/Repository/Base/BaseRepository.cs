using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace StarterKit.Data.Repository
{
    public  class BaseRepository<T> : IRepositoryInterface<T>
    {
        public IDbConnection conn = new SqlConnection("");

        public int? Create(T model) => conn.Insert<T>(model);

        public void Delete(int id) => conn.Delete<T>(id);

        public T GetItem(int id) => conn.Get<T>(id);

        public IList<T> ListAll() => conn.GetList<T>().AsList<T>();

        public int Update(T model) => conn.Update<T>(model);

        void IRepositoryInterface<T>.Create(T model) => conn.Insert<T>(model);

    }
}
