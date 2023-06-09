﻿using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data.Map;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data {
    public class BancoContext : DbContext {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {

        }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Cliente> Fornecedor { get; set; } 
        public DbSet<Safra> Safra { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Producao> Producao { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Saida> Saida { get; set; }
        public DbSet<VendaSaidas> VendaSaidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            //Estou obrigando a geração de ID no momento do create. 
            modelBuilder.Entity<Saida>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);

            //Aplicando a melhora do mapeamento para as movimentações.
            modelBuilder.ApplyConfiguration(new MapProducao());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MapVendas());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MapInventario());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MapSaida());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MapVendaSaidas());
            base.OnModelCreating(modelBuilder);


            //Não tenho interesse de salvar elas no banco de dados, por ser descnessário.
            modelBuilder.Entity<Producao>()
               .Ignore(c => c.ListProdutos);
            modelBuilder.Entity<Producao>()
               .Ignore(c => c.ListSafras);

            modelBuilder.Entity<Venda>()
                .Ignore(c => c.ProdutosList);
            modelBuilder.Entity<Venda>()
                .Ignore(c => c.ClientesList);
        }
    }
}
