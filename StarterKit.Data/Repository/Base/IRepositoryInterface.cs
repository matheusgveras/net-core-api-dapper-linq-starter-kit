using System;
using System.Collections.Generic;

namespace StarterKit.Data.Repository
{
    public interface IRepositoryInterface<T>
    {
        void Create(T model);
        IList<T> ListAll();
        T GetItem(int id);
        int Update(T model);
        void Delete(int id);
    }
   
}
