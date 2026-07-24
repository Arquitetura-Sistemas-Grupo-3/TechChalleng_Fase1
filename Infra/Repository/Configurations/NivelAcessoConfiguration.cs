using Core.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Configurations
{
    public class NivelAcessoConfiguration : IEntityTypeConfiguration<NivelAcesso>
    {
        public void Configure(EntityTypeBuilder<NivelAcesso> builder)
        {
            builder.ToTable("NivelAcesso"); 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);

            builder.HasData(
                new NivelAcesso {Id= 1 ,Nome = "Admin" },
                new NivelAcesso {Id = 2,Nome = "Usuário" }
                );
        }
    }
}
