using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data.Map {
    public class MapProducao : IEntityTypeConfiguration<Producao> {
        public void Configure(EntityTypeBuilder<Producao> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Produto);
            builder.HasOne(x => x.Safra);
        }
    }
}
