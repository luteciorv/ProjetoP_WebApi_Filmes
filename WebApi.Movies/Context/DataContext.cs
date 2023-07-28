﻿using Microsoft.EntityFrameworkCore;
using WebApi.Movies.Entity;
using WebApi.Movies.Repositories;

namespace WebApi.Movies.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
