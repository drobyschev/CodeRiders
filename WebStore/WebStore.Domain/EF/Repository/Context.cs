
//-----------------------------------------------------------------------
// <copyright file="Context.cs" company="Code Riders Ltd 2016">
//     Copyright (c) Code Riders. All rights reserved.
// </copyright>
// <author>Andrey Drobyshev</author>
//-----------------------------------------------------------------------

using System.Data.Entity;

namespace WebStore.Domain.EF.Repository
{
    class Context<T> : DbContext where T : class
    {
        private const string DB_NAME = "WebStoreCR_v1";
        public Context() : base(DB_NAME) { }
        public DbSet<T> Table { get; set; }
    }
}
