
//-----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Code Riders Ltd 2016">
//     Copyright (c) Code Riders. All rights reserved.
// </copyright>
// <author>Andrey Drobyshev</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace WebStore.Domain.EF.Repository
{
    class Repository<T> : IRepository<T> where T : class
    {
        #region IRepository implementation

        virtual public T GetByID(int id)
        {
            using (Context<T> context = new Context<T>())
            {
                return context.Table.Find(id);
            }
        }

        virtual public bool Add(T entity)
        {
            using (Context<T> context = new Context<T>())
            {
                if (!AddImpl(context, entity))
                {
                    return false;
                }

                context.SaveChanges();
                return true;
            }
        }

        virtual public bool Add(IEnumerable<T> entities)
        {
            using (Context<T> context = new Context<T>())
            {
                foreach (var entity in entities)
                {
                    AddImpl(context, entity);
                }

                context.SaveChanges();
                return true;
            }
        }

        virtual public bool Update(T entity)
        {
            using (Context<T> context = new Context<T>())
            {
                if (!UpdateImpl(context, entity, true))
                {
                    return false;
                }

                context.SaveChanges();
                return true;
            }
        }

        virtual public bool Update(IEnumerable<T> entities)
        {
            using (Context<T> context = new Context<T>())
            {
                foreach (var entity in entities)
                {
                    UpdateImpl(context, entity, true);
                }

                context.SaveChanges();
                return true;
            }
        }

        virtual public bool Delete(T entity)
        {
            using (Context<T> context = new Context<T>())
            {
                if (!DeleteImpl(context, entity))
                {
                    return false;
                }

                context.SaveChanges();
                return true;
            }
        }

        virtual public bool Delete(IEnumerable<T> entities)
        {
            using (Context<T> context = new Context<T>())
            {
                foreach (var entity in entities)
                {
                    DeleteImpl(context, entity);
                }

                context.SaveChanges();
                return true;
            }
        }

        virtual public IEnumerable<T> All()
        {
            using (Context<T> context = new Context<T>())
            {
                return context.Table;
            }
        }

        virtual public bool Exists(T entity)
        {
            using (Context<T> context = new Context<T>())
            {
                return ExistsImpl(context, entity);
            }
        }

        private bool AddImpl(Context<T> context, T entity)
        {
            if (ExistsImpl(context, entity))
            {
                return UpdateImpl(context, entity, false);
            }
            
            context.Table.Add(entity);
            return true;
        }

        private bool UpdateImpl(Context<T> context, T entity, bool check_exist)
        {
            if (check_exist && !ExistsImpl(context, entity))
            {
                return false;
            }
            
            context.Table.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        private bool DeleteImpl(Context<T> context, T entity)
        {
            if (!ExistsImpl(context, entity))
            {
                return false;
            }
            
            context.Table.Remove(entity);
            return true;
        }

        private bool ExistsImpl(Context<T> context, T entity)
        {
            return context.Table.Any(e => e == entity);
        }

        #endregion // IRepository implementation
    }
}
