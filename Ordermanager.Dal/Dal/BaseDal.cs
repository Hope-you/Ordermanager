using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DbHelper;
using Ordermanager.Model;

namespace Ordermanager.Dal
{
    //public interface IBaseDal<T> : IDal<T> where T:BaseModel,new()
    //{

    //}
    public class BaseDal<T> : IDal<T> where T : BaseModel, new()
    {

        public long add(T t)
        {
            return DbContext.DbConnection.Insert(t);
        }

        public bool delete(T t)
        {
            return DbContext.DbConnection.Delete(t);
        }

        public bool update(T t)
        {
            return DbContext.DbConnection.Update(t);
        }

        public T @select(string id)
        {
            return DbContext.DbConnection.Get<T>(id);
        }

        public IEnumerable<T> selectAll()
        {
            return DbContext.DbConnection.GetAll<T>();
        }



        public async Task<long> addAsync(T t)
        {
            return await DbContext.DbConnection.InsertAsync(t);
        }

        public async Task<bool> deleteAsync(T t)
        {
            return await DbContext.DbConnection.DeleteAsync(t);
        }

        public async Task<bool> updateAsync(T t)
        {
            return await DbContext.DbConnection.UpdateAsync(t);
        }

        public async Task<T> selectAsync(string id)
        {
            return await DbContext.DbConnection.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> selectAllAsync()
        {
            return await DbContext.DbConnection.GetAllAsync<T>();
        }
    }
}
