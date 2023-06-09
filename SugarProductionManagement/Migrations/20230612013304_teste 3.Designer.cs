﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SugarProductionManagement.Data;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20230612013304_teste 3")]
    partial class teste3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SugarProductionManagement.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ComplementoResidencial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InscricaoEstadual")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InscricaoMunicipal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroResidencial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ComplementoResidencial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Departamento")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroResidencial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rg")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataDeInventario")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescricaoMotivo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<int?>("ProducaoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("QtBaixa")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Producao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataProducao")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataValidade")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Lote")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ProdutoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("QtEstoque")
                        .HasColumnType("int");

                    b.Property<int?>("QtProduzida")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SafraId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("SafraId");

                    b.ToTable("Producao");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Preco")
                        .IsRequired()
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ProdutoStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Safra", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CodSafra")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DataAberturaSafra")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataFechamentoSafra")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StatusSafra")
                        .HasColumnType("int");

                    b.Property<string>("YearSafra")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Safra");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ClienteId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataVenda")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("QtVendida")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("VendasStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Inventario", b =>
                {
                    b.HasOne("SugarProductionManagement.Models.Funcionario", "Funcionario")
                        .WithMany("ListInventario")
                        .HasForeignKey("FuncionarioId");

                    b.HasOne("SugarProductionManagement.Models.Produto", "Produto")
                        .WithMany("ListInventario")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Producao", b =>
                {
                    b.HasOne("SugarProductionManagement.Models.Produto", "Produto")
                        .WithMany("ListProducao")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SugarProductionManagement.Models.Safra", "Safra")
                        .WithMany()
                        .HasForeignKey("SafraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Safra");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Venda", b =>
                {
                    b.HasOne("SugarProductionManagement.Models.Cliente", "Cliente")
                        .WithMany("ListVendas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SugarProductionManagement.Models.Funcionario", "Funcionario")
                        .WithMany("ListVendas")
                        .HasForeignKey("FuncionarioId");

                    b.HasOne("SugarProductionManagement.Models.Produto", "Produto")
                        .WithMany("ListVendas")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Funcionario");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Cliente", b =>
                {
                    b.Navigation("ListVendas");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Funcionario", b =>
                {
                    b.Navigation("ListInventario");

                    b.Navigation("ListVendas");
                });

            modelBuilder.Entity("SugarProductionManagement.Models.Produto", b =>
                {
                    b.Navigation("ListInventario");

                    b.Navigation("ListProducao");

                    b.Navigation("ListVendas");
                });
#pragma warning restore 612, 618
        }
    }
}
