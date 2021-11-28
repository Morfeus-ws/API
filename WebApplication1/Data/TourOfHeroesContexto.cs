using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public partial class TourOfHeroesContexto : DbContext
    {
        public virtual DbSet<Heroi> Heroi { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<HeroiGrupo> HeroiGrupo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(@"User Id=Archerd;Password=fin_contab;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.171.130.114)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=orcl)))",
                                    options => options.UseOracleSQLCompatibility("11"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Heroi>(builder =>
            {
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("IDHEROI")
                    .HasColumnType("NUMBER(15)")
                //.HasDefaultValueSql("SELECT SEQ_HEROI.NEXTVAL FROM DUAL");
                .ValueGeneratedOnAdd()
                .HasValueGenerator((x, y) => new SequenceValueGenerator("SEQ_HEROI"));


                builder.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasColumnType("VARCHAR2(50)");

                builder.Property(e => e.Identidade)
                    .HasColumnName("IDENTIDADE")
                    .HasColumnType("VARCHAR2(50)");

                builder.Property(e => e.Poder)
                    .HasColumnName("PODER")
                    .HasColumnType("VARCHAR2(50)");

                builder.Property(e => e.Idtipo)
                    .HasColumnName("IDTIPO")
                    .HasColumnType("NUMBER(15)");

                builder.HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.Idtipo);

                builder.ToTable("HEROI");
            });

            modelBuilder.Entity<Grupo>(builder =>
            {               
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("IDGRUPO")
                    .HasColumnType("NUMBER(15)")
                    .ValueGeneratedOnAdd()
                  //  .HasDefaultValueSql("SELECT SEQ_GRUPO.NEXTVAL FROM DUAL");
                   .HasValueGenerator((x, y) => new SequenceValueGenerator("SEQ_GRUPO"));

                builder.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("VARCHAR2(50)");

                builder.Property(e => e.Idtipo)
                    .IsRequired()
                    .HasColumnName("IDTIPO")
                    .HasColumnType("NUMBER(15)");

                builder.HasOne(e => e.Tipo)
               .WithMany()
               .HasForeignKey(e => e.Idtipo);

                builder.HasMany(e => e.GrupoHerois)
                .WithOne(f => f.Grupo)
                .HasForeignKey(e => e.IdGrupo);


                builder.ToTable("GRUPO");
            });

            modelBuilder.Entity<Tipo>(builder =>
            {
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("IDTIPO")
                    .HasColumnType("NUMBER(15)");

                builder.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("VARCHAR2(50)");

                builder.ToTable("TIPO");
            });


            modelBuilder.Entity<HeroiGrupo>(builder => 
            {
                builder.HasKey(e => new { e.IdHero, e.IdGrupo });

                builder.Property(e => e.IdHero)
                    .IsRequired()
                    .HasColumnName("IDHEROI")
                    .HasColumnType("NUMBER(15)");

                builder.Property(e => e.IdGrupo)
                    .IsRequired()
                    .HasColumnName("IDGRUPO")
                    .HasColumnType("NUMBER(15)");

                builder.HasOne(e => e.Grupo)
                .WithMany(f => f.GrupoHerois)
                .HasForeignKey(e => e.IdGrupo);


                builder.HasOne(e => e.Heroi)
                .WithMany()
                .HasForeignKey(e => e.IdHero);


                builder.ToTable("HEROIGRUPO");
            });

            base.OnModelCreating(modelBuilder);
        }

        public class SequenceValueGenerator : ValueGenerator<long>
        {
            private readonly string _sequenceName;

            public SequenceValueGenerator(string sequenceName)
            {
                _sequenceName = sequenceName;
            }

            public override bool GeneratesTemporaryValues => false;

            public override long Next(EntityEntry entry)
            {
                using var command = entry.Context.Database.GetDbConnection().CreateCommand();
                command.CommandText = $"SELECT {_sequenceName}.NEXTVAL FROM DUAL";
                entry.Context.Database.OpenConnection();
                var reader = command.ExecuteScalar();
                return Convert.ToInt64(reader);
            }
        }
    }

}
