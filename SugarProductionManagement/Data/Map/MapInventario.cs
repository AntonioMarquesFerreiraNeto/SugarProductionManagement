using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data.Map {
    public class MapInventario : IEntityTypeConfiguration<Inventario> {
        public void Configure(EntityTypeBuilder<Inventario> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Producao);
            builder.HasOne(x => x.Funcionario);
        }
    }
}
