using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data.Map {
    public class MapSaida : IEntityTypeConfiguration<Saida> {
        public void Configure(EntityTypeBuilder<Saida> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Venda);
            builder.HasOne(x => x.Produto);
            builder.HasOne(x => x.Cliente);
            builder.HasOne(x => x.Funcionario);
        }
    }
}
