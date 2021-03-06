﻿namespace NameTransliterator.Data.Repositories
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Data.Context;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;

        private readonly IApplicationDbContext context;

        public GenericRepository(IApplicationDbContext context)
        {
            this.context = context ?? 
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");

            this.DbSet = context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.DbSet;
        }

        public virtual T GetById(Guid id)
        {
            return this.DbSet.Find(id);
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            this.DbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
