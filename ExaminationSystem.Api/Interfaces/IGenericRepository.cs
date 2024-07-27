﻿using ExaminationSystem.Api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace ExaminationSystem.Api.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    IQueryable<T> GetAll();
    IQueryable<T> Get(Expression<Func<T, bool>> predicate);
    T GetByID(int id);
    T GetWithTrackinByID(int id);
    T Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(int id);
    T First(Expression<Func<T, bool>> predicate);

    void SaveChanges();
}
