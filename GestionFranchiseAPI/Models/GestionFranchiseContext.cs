using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class GestionFranchiseContext : DbContext
    {
        public GestionFranchiseContext()
        {
        }

        public GestionFranchiseContext(DbContextOptions<GestionFranchiseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Franchise> Franchises { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=GestionFranchise.mssql.somee.com;packet size=4096;user id=PlutoR47_SQLLogin_1;pwd=7joobsaxxh;data source=GestionFranchise.mssql.somee.com;persist security info=False;initial catalog=GestionFranchise");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.IdAgent)
                    .HasName("PK__Agent__D87D391FBA3502BB");

                entity.ToTable("Agent");

                entity.Property(e => e.IdAgent).HasColumnName("idAgent");

                entity.Property(e => e.IdFranchise).HasColumnName("idFranchise");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.HasOne(d => d.IdFranchiseNavigation)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.IdFranchise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Agent__idFranchi__49C3F6B7");
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.HasKey(e => e.IdCommand)
                    .HasName("PK__Command__0289890AA354EEDD");

                entity.ToTable("Command");

                entity.Property(e => e.IdCommand).HasColumnName("idCommand");

                entity.Property(e => e.DateCommand)
                    .HasColumnType("date")
                    .HasColumnName("dateCommand");

                entity.Property(e => e.IdProduit).HasColumnName("idProduit");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.QteModified).HasColumnName("qteModified");

                entity.HasOne(d => d.IdProduitNavigation)
                    .WithMany(p => p.Commands)
                    .HasForeignKey(d => d.IdProduit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Command__idProdu__4F7CD00D");

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.Commands)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Command__login__5070F446");
            });

            modelBuilder.Entity<Franchise>(entity =>
            {
                entity.HasKey(e => e.IdFranchise)
                    .HasName("PK__Franchis__FEECC7ED9BAA0814");

                entity.ToTable("Franchise");

                entity.Property(e => e.IdFranchise).HasColumnName("idFranchise");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("login");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.IdProduit)
                    .HasName("PK__Produit__5EEC0A1947CF957F");

                entity.ToTable("Produit");

                entity.Property(e => e.IdProduit).HasColumnName("idProduit");

                entity.Property(e => e.IdFranchise).HasColumnName("idFranchise");

                entity.Property(e => e.NameProduit)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nameProduit");

                entity.Property(e => e.QteProduit).HasColumnName("qteProduit");

                entity.HasOne(d => d.IdFranchiseNavigation)
                    .WithMany(p => p.Produits)
                    .HasForeignKey(d => d.IdFranchise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Produit__idFranc__4CA06362");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("PK__Utilisat__7838F273848FC97D");

                entity.ToTable("Utilisateur");

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.EmailUtilisateur)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("emailUtilisateur");

                entity.Property(e => e.IdType).HasColumnName("idType");

                entity.Property(e => e.NameUtilisateur)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nameUtilisateur");

                entity.Property(e => e.NumUtilisateur)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("numUtilisateur");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.TypeUtilisateur)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("typeUtilisateur");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
