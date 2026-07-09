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
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("Jogo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("INT").UseIdentityColumn(); 
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
        }
    }
}
