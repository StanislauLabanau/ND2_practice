using Microsoft.EntityFrameworkCore;
using System;
using TicketsReselling.DAL;

namespace TicketsReselling.Tests
{
    public class CoreTest
    {
        protected DbContextOptions<TicketsResellingContext> ContextOptions { get; }

        protected CoreTest(DbContextOptions<TicketsResellingContext> contextOptions)
        {
            ContextOptions = contextOptions;

            var context = new TicketsResellingContext(contextOptions);
            var dataSeeder = new TestDataSeeder(context);
            dataSeeder.SeedData();
        }
    }
}
