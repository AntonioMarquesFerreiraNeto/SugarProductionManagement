﻿using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data {
    public class BancoContext : DbContext {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {

        }
        public DbSet<Funcionario> Funcionario { get; set; }
    }
}