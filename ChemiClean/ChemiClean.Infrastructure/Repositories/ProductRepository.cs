using ChemiClean.Core;
using ChemiClean.Core.Interfaces;
using ChemiClean.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChemiClean.Infrastructure
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        DbSet<Product> entity;
        public ProductRepository(ChemicleanContext context) : base(context)
            => entity = context.Set<Product>() ?? throw new ArgumentException(nameof(entity));
    }
}
