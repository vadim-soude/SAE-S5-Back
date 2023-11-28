using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WorkshopAPI.Models
{
    public partial class bdeContext : DbContext
    {
        public bdeContext()
        {
        }

        public bdeContext(DbContextOptions<bdeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actualité> Actualités { get; set; } = null!;
        public virtual DbSet<Contenu> Contenus { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventInscription> EventInscriptions { get; set; } = null!;
        public virtual DbSet<Membre> Membres { get; set; } = null!;
        public virtual DbSet<PaiementReussi> PaiementReussis { get; set; } = null!;
        public virtual DbSet<Produit> Produits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=bde;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Actualité>(entity =>
            {
                entity.HasKey(e => e.IdActu)
                    .HasName("PRIMARY");

                entity.ToTable("actualités");

                entity.Property(e => e.IdActu)
                    .ValueGeneratedNever()
                    .HasColumnName("id_actu");

                entity.Property(e => e.Auteur)
                    .HasMaxLength(50)
                    .HasColumnName("auteur");

                entity.Property(e => e.DateCreation).HasColumnName("date_creation");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(140)
                    .HasColumnName("image_url");

                entity.Property(e => e.LongDescription)
                    .HasMaxLength(200)
                    .HasColumnName("long_description");

                entity.Property(e => e.Nom)
                    .HasMaxLength(60)
                    .HasColumnName("nom");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(100)
                    .HasColumnName("short_description");
            });

            modelBuilder.Entity<Contenu>(entity =>
            {
                entity.HasKey(e => e.IdContent)
                    .HasName("PRIMARY");

                entity.ToTable("contenu");

                entity.HasIndex(e => e.NameSpace, "name_space")
                    .IsUnique();

                entity.Property(e => e.IdContent)
                    .ValueGeneratedNever()
                    .HasColumnName("id_content");

                entity.Property(e => e.Content)
                    .HasColumnType("mediumtext")
                    .HasColumnName("content");

                entity.Property(e => e.NameSpace).HasColumnName("name_space");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvent)
                    .HasName("PRIMARY");

                entity.ToTable("events");

                entity.Property(e => e.IdEvent)
                    .ValueGeneratedNever()
                    .HasColumnName("id_event");

                entity.Property(e => e.Auteur)
                    .HasMaxLength(50)
                    .HasColumnName("auteur");

                entity.Property(e => e.DateCreation).HasColumnName("date_creation");

                entity.Property(e => e.DateDebut)
                    .HasColumnType("datetime")
                    .HasColumnName("date_debut");

                entity.Property(e => e.DateFin)
                    .HasColumnType("datetime")
                    .HasColumnName("date_fin");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(140)
                    .HasColumnName("image_url");

                entity.Property(e => e.Lieu)
                    .HasMaxLength(40)
                    .HasColumnName("lieu");

                entity.Property(e => e.LongDescription)
                    .HasMaxLength(200)
                    .HasColumnName("long_description");

                entity.Property(e => e.NbPlaceRestantes).HasColumnName("nb_place_restantes");

                entity.Property(e => e.NbPlacesDispo).HasColumnName("nb_places_dispo");

                entity.Property(e => e.Nom)
                    .HasMaxLength(60)
                    .HasColumnName("nom");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(140)
                    .HasColumnName("short_description");
            });

            modelBuilder.Entity<EventInscription>(entity =>
            {
                entity.HasKey(e => e.IdEventInscription)
                    .HasName("PRIMARY");

                entity.ToTable("event_inscription");

                entity.HasIndex(e => e.IdEvent, "id_event");

                entity.HasIndex(e => e.IdMembre, "id_membre");

                entity.Property(e => e.IdEventInscription)
                    .ValueGeneratedNever()
                    .HasColumnName("id_event_inscription");

                entity.Property(e => e.IdEvent).HasColumnName("id_event");

                entity.Property(e => e.IdMembre).HasColumnName("id_membre");

                entity.Property(e => e.StatutPaiement).HasColumnName("statut_paiement");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventInscriptions)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event_inscription_ibfk_2");

                entity.HasOne(d => d.IdMembreNavigation)
                    .WithMany(p => p.EventInscriptions)
                    .HasForeignKey(d => d.IdMembre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event_inscription_ibfk_1");
            });

            modelBuilder.Entity<Membre>(entity =>
            {
                entity.HasKey(e => e.IdMembre)
                    .HasName("PRIMARY");

                entity.ToTable("membres");

                entity.Property(e => e.IdMembre)
                    .ValueGeneratedNever()
                    .HasColumnName("id_membre");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.DiscordUsername)
                    .HasMaxLength(40)
                    .HasColumnName("discord_username");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .HasColumnName("last_name");

                entity.Property(e => e.MailUpjv)
                    .HasMaxLength(60)
                    .HasColumnName("mail_upjv");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.PpImageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("pp_image_url");

                entity.Property(e => e.Statut)
                    .HasMaxLength(40)
                    .HasColumnName("statut");
            });

            modelBuilder.Entity<PaiementReussi>(entity =>
            {
                entity.HasKey(e => e.IdPaiement)
                    .HasName("PRIMARY");

                entity.ToTable("paiement_reussi");

                entity.Property(e => e.IdPaiement)
                    .ValueGeneratedNever()
                    .HasColumnName("id_paiement");

                entity.Property(e => e.JsonDuPaiement)
                    .HasColumnType("mediumtext")
                    .HasColumnName("json_du_paiement");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.IdProduit)
                    .HasName("PRIMARY");

                entity.ToTable("produits");

                entity.Property(e => e.IdProduit)
                    .ValueGeneratedNever()
                    .HasColumnName("id_produit");

                entity.Property(e => e.Categorie)
                    .HasMaxLength(40)
                    .HasColumnName("categorie");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("image_url");

                entity.Property(e => e.Nom)
                    .HasMaxLength(40)
                    .HasColumnName("nom");

                entity.Property(e => e.PrixAdherent).HasColumnName("prix_adherent");

                entity.Property(e => e.PrixFournisseur).HasColumnName("prix_fournisseur");

                entity.Property(e => e.PrixNonAdherent).HasColumnName("prix_non_adherent");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
