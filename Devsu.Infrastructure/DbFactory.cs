using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Infrastructure
{
    public class DbFactory : IDisposable
    {
        private bool _isDisposed;
        private Func<DevsuContext> _instanceFunc;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<DevsuContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_isDisposed && _dbContext != null)
            {
                _isDisposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
