using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data.Map {
    public class MapVendaSaidas : IEntityTypeConfiguration<VendaSaidas> {
        public void Configure(EntityTypeBuilder<VendaSaidas> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Producao);
            builder.HasOne(x => x.Venda);
        }
    }
}
