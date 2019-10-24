﻿using System;
using System.Data;
using System.Data.Common;
using Dekopon.Repository;
using Dekopon.Transaction;
using Microsoft.EntityFrameworkCore;
using StackExchange.Profiling.Data;

namespace Dekopon
{
    public class ProfiledDatabaseManager : DatabaseManager
    {
        private readonly IDbProfiler _dbProfiler;

        public ProfiledDatabaseManager(DbContextOptions dbContextOptions, IDbProfiler dbProfiler, ITransactionManager transactionManager = null)
            : base(dbContextOptions, transactionManager)
        {
            _dbProfiler = dbProfiler;
        }

        public ProfiledDatabaseManager(Func<DbContext> databaseFactory, IDbProfiler dbProfiler, ITransactionManager transactionManager = null)
            : base(databaseFactory, transactionManager)
        {
            _dbProfiler = dbProfiler;
        }

        public override IDbConnection GetConnection()
        {
            var rawConnection = (DbConnection) base.GetConnection();
            return new ProfiledDbConnection(rawConnection, _dbProfiler);
        }
    }
}