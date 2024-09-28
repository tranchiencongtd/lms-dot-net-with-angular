using System.Linq.Expressions;

namespace HDNXUdemyData.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(long id);

        Task<T?> GetByIdAsync(long id);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);

        T GetObject(Expression<Func<T, bool>> expression);

        Task<T> GetObjectAsync(Expression<Func<T, bool>> expression);

        bool Add(T entity);

        Task<bool> AddAsync(T entity);

        bool AddMany(IEnumerable<T> entity);

        Task<bool> AddManyAsync(IEnumerable<T> entity);

        bool BulkInsert(List<T> pObjInsertList);

        T? GetObject(params object[] pKeys);

        bool UpdateStatus(long pKey, int pStatus);

        bool Update(T pObj);

        bool UpdateMany(IEnumerable<T> entity);

        Task<bool> BulkInsertAsync(List<T> pObjInsertList);

        Task<T?> GetObjectAsync(params object[] pKeys);

        Task<bool> UpdateAsync(T pObj);

        Task<T> AddReturnModelAsync(T entity);

        bool UpdateStatus(T pObj);

        Task<bool> UpdateStatusAsync(T pObj);

        Task<bool> DeleteByKey(long pKey);
    }
}