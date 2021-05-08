using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace QuestionnaireServices.Models
{
    public partial class ERXQNContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public ERXQNContext()
        {
            
        }

        public ERXQNContext(DbContextOptions<ERXQNContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerRestriction> AnswerRestrictions { get; set; }
        public virtual DbSet<AnswerType> AnswerTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<QuestionChoice> QuestionChoices { get; set; }
        public virtual DbSet<Restriction> Restrictions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ERXQNDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => new { e.Token, e.QuestionId });

                entity.ToTable("Answer");

                entity.Property(e => e.Token)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value");

                entity.Ignore(e => e.Text);
            });

            modelBuilder.Entity<AnswerRestriction>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.RestrictionId });

                entity.ToTable("AnswerRestriction");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.RestrictionId).HasColumnName("restrictionId");
            });

            modelBuilder.Entity<AnswerType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("AnswerType");

                entity.Property(e => e.Name)
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnswerTypeId).HasColumnName("answerTypeId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.Property(e => e.Sequence).HasColumnName("sequence");
            });

            modelBuilder.Entity<QuestionCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("QuestionCategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(400)
                    .HasColumnName("name");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<QuestionChoice>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.Sequence });

                entity.ToTable("QuestionChoice");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.Sequence).HasColumnName("sequence");

                entity.Property(e => e.SourceUri).HasColumnName("sourceURI");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Restriction>(entity =>
            {
                entity.ToTable("Restriction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NotAllowedValue).HasColumnName("notAllowedValue");

                entity.Property(e => e.Operation).HasColumnName("operation");

                entity.Property(e => e.WarningMessage).HasColumnName("warningMessage");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
