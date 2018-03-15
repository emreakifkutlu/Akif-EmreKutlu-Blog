﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Abstract
{
   public interface IRepository<T>
        where T:class, new()
    {
        void Add(T model);
        T GetById(int id);
        void Delete(int id);
        void Update(T model);
        List<T> GetList();
        IEnumerable<T> Where(Expression<Func<T, bool>> lambda);
        IQueryable<T> WhereByQuery(Expression<Func<T, bool>> lambda);
        bool Any(Expression<Func<T, bool>> lambda);

        T GetObject(Expression<Func<T, bool>> lambda);
    }
}
