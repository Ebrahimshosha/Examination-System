using ExaminationSystem.Api.Data;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExaminationSystem.Api.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity); // This line because change tracker maybe disable !
    }

    public void Delete(int id)
    {
        T entity = _context.Find<T>(id);
        Delete(entity);
    }

    public void HardDelete(int id)
    {
        //T entity = _context.Find<T>(id);
        //_context.Set<T>().Remove(entity);

        _context.Set<T>().Where(x => x.Id == id).ExecuteDelete();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return GetAll().Where(predicate);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().Where(x => !x.IsDeleted).AsNoTracking();
        //return _context.Set<T>().Where(x => !x.Deleted).AsNoTrackingWithIdentityResolution(); // better than AsNoTracking() 
    }

    public T GetByID(int id)
    {
        //return _context.Find<T>(id);
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public T GetWithTrackinByID(int id)
    {
        return _context.Set<T>()
            .Where(x => !x.IsDeleted && x.Id == id)
            .AsTracking()
            .FirstOrDefault();
    }

    public T First(Expression<Func<T, bool>> predicate)
    {
        return Get(predicate).FirstOrDefault();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

}
