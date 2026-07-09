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
    public class UsuarioJogoConfiguration : IEntityTypeConfiguration<UsuarioJogo>
    {
        public void Configure(EntityTypeBuilder<UsuarioJogo> builder)
        {
            builder.ToTable("UsuarioJogo");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).HasColumnType("INT").UseIdentityColumn();
           
            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.JogoId).IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Jogo)
                .WithMany()
                .HasForeignKey(x => x.JogoId)
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
