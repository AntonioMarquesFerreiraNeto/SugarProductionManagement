﻿using SugarProductionManagement.Data;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;
using System;

namespace SugarProductionManagement.Repository {
    public class ClienteRepository : IClienteRepository {

        private BancoContext _bancoContext;

        public ClienteRepository(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }

        public Cliente Ativar(Cliente fornecedor) {
            try {
                Cliente fornecedorDB = GetFornecedorById(fornecedor.Id);
                if (fornecedorDB == null) throw new Exception("Desculpe, houve algum conflito interno!");
                fornecedorDB.Status = FornecedorStatus.Ativo;
                _bancoContext.Fornecedor.Update(fornecedorDB);
                _bancoContext.SaveChanges();
                return fornecedor;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Cliente Create(Cliente fornecedor) {
            try {
                if (ValidationDuplicata(fornecedor)) throw new Exception("Cliente já se encontra registrado!");
                _bancoContext.Fornecedor.Add(fornecedor);
                _bancoContext.SaveChanges();
                return fornecedor;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public List<Cliente> GetFornecedorAtivosAll() {
            return _bancoContext.Fornecedor.Where(x => x.Status == FornecedorStatus.Ativo).ToList();
        }

        public Cliente GetFornecedorById(int id) {
            return _bancoContext.Fornecedor.FirstOrDefault(x => x.Id == id);
        }

        public List<Cliente> GetFornecedorInativosAll() {
            return _bancoContext.Fornecedor.Where(x => x.Status == FornecedorStatus.Inativo).ToList();
        }

        public Cliente Inativar(Cliente fornecedor) {
            try {
                Cliente fornecedorDB = GetFornecedorById(fornecedor.Id);
                if (fornecedorDB == null) throw new Exception("Desculpe, houve algum conflito interno!");
                fornecedorDB.Status = FornecedorStatus.Inativo;
                _bancoContext.Fornecedor.Update(fornecedorDB);
                _bancoContext.SaveChanges();
                return fornecedor;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Cliente Update(Cliente fornecedor) {
            try {
                Cliente fornecedorDB = GetFornecedorById(fornecedor.Id);
                if (fornecedorDB == null) throw new Exception("Desculpe, houve algum conflito interno!");
                if (ValidationDuplicataEdit(fornecedor, fornecedorDB)) throw new Exception("Cliente já se encontra registrado!");
                fornecedorDB.NomeFantasia = fornecedor.NomeFantasia;
                fornecedorDB.RazaoSocial = fornecedor.RazaoSocial;
                fornecedorDB.InscricaoMunicipal = fornecedor.InscricaoMunicipal;
                fornecedorDB.InscricaoEstadual = fornecedor.InscricaoEstadual;
                fornecedorDB.Cnpj = fornecedor.Cnpj;
                fornecedorDB.Tel = fornecedor.Tel;
                fornecedorDB.Email = fornecedor.Email;
                fornecedorDB.Logradouro = fornecedor.Logradouro;
                fornecedorDB.NumeroResidencial = fornecedor.NumeroResidencial;
                fornecedorDB.ComplementoResidencial = fornecedor.ComplementoResidencial;
                fornecedorDB.Bairro = fornecedor.Bairro;
                fornecedorDB.Cidade = fornecedor.Cidade;
                fornecedorDB.Estado = fornecedor.Estado;

                _bancoContext.Fornecedor.Update(fornecedorDB);
                _bancoContext.SaveChanges();
                return fornecedorDB;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public bool ValidationDuplicata(Cliente cliente) {
            List<Cliente> clientes = _bancoContext.Fornecedor.ToList();
            if (clientes.Any(x => x.Cnpj == cliente.Cnpj ||
                     x.RazaoSocial == cliente.RazaoSocial ||
                     x.NomeFantasia == cliente.NomeFantasia ||
                     x.Tel == cliente.Tel ||
                     x.Email == cliente.Email ||
                     x.InscricaoEstadual == cliente.InscricaoEstadual)) {
                return true;
            }
            return false;
        }

        public bool ValidationDuplicataEdit(Cliente cliente, Cliente clienteDB) {
            List<Cliente> clientes = _bancoContext.Fornecedor.ToList();

            if (clientes.Any(x => (x.Cnpj == cliente.Cnpj && x.Cnpj != clienteDB.Cnpj) ||
                (x.RazaoSocial == cliente.RazaoSocial && x.RazaoSocial != clienteDB.RazaoSocial) ||
                (x.NomeFantasia == cliente.NomeFantasia && x.NomeFantasia != clienteDB.NomeFantasia) ||
                (x.Tel == cliente.Tel && x.Tel != clienteDB.Tel) ||
                (x.Email == cliente.Email && x.Email != clienteDB.Email) ||
                (x.InscricaoEstadual == cliente.InscricaoEstadual && x.InscricaoEstadual != clienteDB.InscricaoEstadual))) {
                return true;
            }

            return false;
        }
    }
}
