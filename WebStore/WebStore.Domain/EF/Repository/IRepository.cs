
//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Code Riders Ltd 2016">
//     Copyright (c) Code Riders. All rights reserved.
// </copyright>
// <author>Andrey Drobyshev</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace WebStore.Domain.EF.Repository
{
    interface IRepository<T> where T : class
    {
        T GetByID(int id);
        bool Add(T entity);
        bool Add(IEnumerable<T> entities);
        bool Update(T entity);
        bool Update(IEnumerable<T> entities);
        bool Delete(T entity);
        bool Delete(IEnumerable<T> entities);
        IEnumerable<T> All();
        bool Exists(T entity);
    }
}
