using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Data.Map {
    public class MapVendas : IEntityTypeConfiguration<Venda> {
        public void Configure(EntityTypeBuilder<Venda> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Produto);
            builder.HasOne(x => x.Funcionario);
            builder.HasOne(x => x.Cliente);
        }
    }
}
