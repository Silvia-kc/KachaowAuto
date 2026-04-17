using KachaowAuto.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Tests.Helpers
{
    public static class DbContextFactory
    {
        public static KachaowAutoDbContext Create()
        {
            var options = new DbContextOptionsBuilder<KachaowAutoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new KachaowAutoDbContext(options);
        }
    }
}
