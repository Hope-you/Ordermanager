using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ordermanager.Model;

namespace Ordermanager.Dal
{
    public interface IDal<T> where T : BaseModel, new()
    {
        
        public long add( T t);
        public bool delete(T t);
        public bool update(T t);
        public T select(string id);
        public IEnumerable<T> selectAll();


        public Task<long> addAsync(T t);
        public Task<bool> deleteAsync(T t);
        public Task<bool> updateAsync(T t);
        public Task<T> selectAsync(string id);
        public Task<IEnumerable<T>> selectAllAsync();
    }
}
