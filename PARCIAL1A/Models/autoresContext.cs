﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1A.Models
{

    public class autoresContext : DbContext
    {
        public autoresContext(DbContextOptions<autoresContext> options) : base(options)
        {
        }
    }
}