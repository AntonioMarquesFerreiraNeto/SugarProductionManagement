using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface ISafraRepository {
        public Safra CreateSafra(Safra safra);
        public Safra FecharSafra(int? id);
        public Safra GetSafraById(int? id);
        public Safra DeleteSafra(int? id);
        public List<Safra> GetAllSafras();
        public List<Safra> GetSafrasAbertas();
    }
}
