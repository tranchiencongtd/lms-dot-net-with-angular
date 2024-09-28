using EFCore.BulkExtensions;
using HDNXUdemyData.Entities;
using HDNXUdemyData.EntitiesContext;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.SystemExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System.Linq.Expressions;

namespace HDNXUdemyData.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntities
    {
        public readonly ProjectContext _projectContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectContext = new ProjectContext(_httpContextAccessor);
        }

        public T GetById(long id)
        {
            return _projectContext.Set<T>().Find(id)!;
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _projectContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _projectContext.Set<T>().Where(x => x.Status == (int)EStatus.Active);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _projectContext.Set<T>().Where(x => x.Status == (int)EStatus.Active).ToListAsync();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _projectContext.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var resultData = await _projectContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
            return resultData;
        }

        public T GetObject(Expression<Func<T, bool>> expression)
        {
            return _projectContext.Set<T>().Where(expression).FirstOrDefault()!;
        }

        public async Task<T> GetObjectAsync(Expression<Func<T, bool>> expression)
        {
            return await _projectContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        private bool Insert(T pObj)
        {
            try
            {
                _projectContext.Entry(pObj).State = EntityState.Added;
                return true;
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<T> AddReturnModelAsync(T entity)
        {
            using var transaction = await _projectContext.Database.BeginTransactionAsync();
            try
            {
                bool isOk = Insert(entity);
                if (!isOk)
                {
                    return entity;
                }
                await _projectContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return entity;
            }
            catch (Exception ex)
            {
                await _projectContext.Database.RollbackTransactionAsync();
                throw new ProjectException(ex.Message, ex);
            }
        }

        public bool Add(T entity)
        {
            using var transaction = _projectContext.Database.BeginTransaction();
            try
            {
                bool isOk = Insert(entity);
                if (!isOk)
                {
                    return false;
                }
                _projectContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _projectContext.Database.RollbackTransaction();
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<bool> AddAsync(T entity)
        {
            using var transaction = await _projectContext.Database.BeginTransactionAsync();
            try
            {
                bool isOk = Insert(entity);
                if (!isOk)
                {
                    return false;
                }
                await _projectContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _projectContext.Database.RollbackTransactionAsync();
                throw new ProjectException(ex.Message, ex);
            }
        }

        public bool AddMany(IEnumerable<T> entity)
        {
            using var transaction = _projectContext.Database.BeginTransaction();
            try
            {
                _projectContext.Set<T>().AddRange(entity);
                _projectContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _projectContext.Database.RollbackTransaction();
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<bool> AddManyAsync(IEnumerable<T> entity)
        {
            using var transaction = await _projectContext.Database.BeginTransactionAsync();
            try
            {
                await _projectContext.Set<T>().AddRangeAsync(entity);
                await _projectContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _projectContext.Database.RollbackTransactionAsync();
                throw new ProjectException(ex.InnerException == null ? ex.Message : ex.InnerException.Message, ex);
            }
        }

        public bool BulkInsert(List<T> pObjInsertList)
        {
            var transaction = _projectContext.Database.BeginTransaction();
            using (transaction)
            {
                try
                {
                    bool isOk = this.BulkInsertData(pObjInsertList);
                    if (isOk)
                    {
                        transaction.Commit();
                        return isOk;
                    }
                    else
                    {
                        transaction.Rollback();
                        return isOk;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ProjectException(ex.Message, ex);
                }
            }
        }

        public async Task<bool> BulkInsertAsync(List<T> pObjInsertList)
        {
            var transaction = await _projectContext.Database.BeginTransactionAsync();
            using (transaction)
            {
                try
                {
                    bool isOk = await this.BulkInsertDataAsync(pObjInsertList);
                    if (isOk)
                    {
                        await transaction.CommitAsync();
                        return isOk;
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return isOk;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ProjectException(ex.Message, ex);
                }
            }
        }

        private bool BulkInsertData(List<T> pObjInsertList)
        {
            bool isOk = false;
            try
            {
                LocalDateTime timeUpdate = LocalDateTime.FromDateTime(DateTime.UtcNow);
                pObjInsertList.ForEach(p =>
                {
                    p.CreateDate = timeUpdate;
                    p.UpdateDate = timeUpdate;
                });
                _projectContext.BulkInsert(pObjInsertList);
                return isOk;
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        private async Task<bool> BulkInsertDataAsync(List<T> pObjInsertList)
        {
            bool isOk = false;
            try
            {
                LocalDateTime timeUpdate = LocalDateTime.FromDateTime(DateTime.UtcNow);
                pObjInsertList.ForEach(p =>
                {
                    p.CreateDate = timeUpdate;
                    p.UpdateDate = timeUpdate;
                });
                await _projectContext.BulkInsertAsync(pObjInsertList);
                return isOk;
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        public T? GetObject(params object[] pKeys)
        {
            return _projectContext.Set<T>().Find(pKeys);
        }

        public async Task<T?> GetObjectAsync(params object[] pKeys)
        {
            return await _projectContext.Set<T>().FindAsync(pKeys);
        }

        public bool UpdateStatus(long pKey, int pStatus)
        {
            using var transaction = _projectContext.Database.BeginTransaction();
            try
            {
                var obj = GetById(pKey);
                obj.Status = pStatus;
                _projectContext.Entry(obj).State = EntityState.Modified;
                _projectContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _projectContext.Database.RollbackTransaction();
                throw new ProjectException(ex.Message, ex);
            }
        }

        private bool UpdateWithObject(T pObj, params string[] pNotUpdatedProperties)
        {
            try
            {
                var keyNames = GetPrimaryKey();
                var keyValues = keyNames.Select(p => pObj.GetType().GetProperty(p)?.GetValue(pObj, null)).ToArray();
                if (keyValues != null)
                {
                    T exist = GetObject(keyValues!)!;
                    if (exist != null)
                    {
                        _projectContext.Entry(exist).State = EntityState.Detached;
                        _projectContext.Attach(pObj);
                        var entry = _projectContext.Entry(pObj);
                        entry.State = EntityState.Modified;

                        if (pNotUpdatedProperties.Any())
                        {
                            foreach (string prop in pNotUpdatedProperties)
                            {
                                entry.Property(prop).IsModified = false;
                            }
                        }

                        return true;
                    }
                }
                else
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        public bool Update(T pObj)
        {
            return UpdateWithTransaction(pObj, "CreateDate", "CreateBy");
        }

        public async Task<bool> UpdateAsync(T pObj)
        {
            return await UpdateWithTransactionAsync(pObj, "CreateDate", "CreateBy");
        }

        public bool UpdateStatus(T pObj)
        {
            return UpdateWithTransaction(pObj, "CreateDate", "CreateBy");
        }

        public async Task<bool> UpdateStatusAsync(T pObj)
        {
            return await UpdateWithTransactionAsync(pObj, "CreateDate", "CreateBy");
        }

        private bool UpdateWithTransaction(T pObj, params string[] pNotUpdatedProperties)
        {
            using IDbContextTransaction transaction = _projectContext.Database.BeginTransaction();
            try
            {
                bool isOk = UpdateWithObject(pObj, pNotUpdatedProperties);
                if (isOk)
                {
                    _projectContext.SaveChanges();
                    transaction.Commit();
                }

                return isOk;
            }
            catch (Exception ex)
            {
                _projectContext.Database.RollbackTransaction();
                throw new ProjectException(ex.Message, ex);
            }
        }

        private async Task<bool> UpdateWithTransactionAsync(T pObj, params string[] pNotUpdatedProperties)
        {
            using IDbContextTransaction transaction = await _projectContext.Database.BeginTransactionAsync();
            try
            {
                bool isOk = UpdateWithObject(pObj, pNotUpdatedProperties);
                if (isOk)
                {
                    await _projectContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                return isOk;
            }
            catch (Exception ex)
            {
                await _projectContext.Database.RollbackTransactionAsync();
                throw new ProjectException(ex.Message, ex);
            }
        }

        public bool UpdateMany(IEnumerable<T> entity)
        {
            using var transaction = _projectContext.Database.BeginTransaction();
            try
            {
                _projectContext.Set<T>().UpdateRange(entity);
                _projectContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _projectContext.Database.RollbackTransaction();
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteByKey(long pKey)
        {
            using var transaction = _projectContext.Database.BeginTransaction();
            try
            {
                var obj = GetById(pKey);
                _projectContext.Remove(obj);
                await _projectContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (SystemException ex)
            {
                await _projectContext.Database.RollbackTransactionAsync();
                throw new ProjectException(ex.Message);
            }
        }

        private string[] GetPrimaryKey()
        {
            return _projectContext.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties
                .Select(x => x.Name)?.ToArray() ?? Array.Empty<string>(); ;
        }
    }
}