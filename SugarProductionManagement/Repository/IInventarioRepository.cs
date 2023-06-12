using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IInventarioRepository {
        public Inventario Create(Inventario inventario);
        public Inventario Update(Inventario inventario);
        public Inventario Inativar(int id);
        public Inventario GetById(int id);
        public List<Inventario> GetAtivosAll();
        public List<Inventario> GetInativosAll();
    }
}
