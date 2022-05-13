using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DbHelper;
using Ordermanager.Dal.Dal.BaseDal;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;

namespace Ordermanager.Dal
{
    public interface IBaseDal<T> : IDal<T> where T : BaseModel, new()
    {

    }
    /// <summary>
    /// basedal是继承IDal的类，实现了最基本的几个方法crud，其他的实体直接继承BaseDal就可以了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> : IBaseDal<T> where T : BaseModel, new()
    {
        private readonly BaseOverAllDal<T> _baseOverAllDal;

        public BaseDal(BaseOverAllDal<T> baseOverAllDal)
        {
            _baseOverAllDal = baseOverAllDal;
        }
        public long add(T t)
        {
            return _baseOverAllDal.Insert(t);
        }

        public bool delete(T t)
        {
            return _baseOverAllDal.Delete(t);
        }

        public bool update(T t)
        {
            return _baseOverAllDal.Update(t);
        }

        public T @select(string id)
        {

            return _baseOverAllDal.GetFromDapper(id);
        }

        public IEnumerable<T> selectAll()
        {
            return _baseOverAllDal.GetAll();
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
