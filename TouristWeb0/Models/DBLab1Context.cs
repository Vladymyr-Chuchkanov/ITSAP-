using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TouristWeb0
{
    public partial class DBLab1Context : DbContext
    {
        public DBLab1Context()
        {
        }

        public DBLab1Context(DbContextOptions<DBLab1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admition> Admitions { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<CompetitionTeam> CompetitionTeams { get; set; }
        public virtual DbSet<Complexity> Complexities { get; set; }
        public virtual DbSet<Obstacle> Obstacles { get; set; }
        public virtual DbSet<ObstacleCompetition> ObstacleCompetitions { get; set; }
        public virtual DbSet<Partisipant> Partisipants { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<RankPartisipant> RankPartisipants { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamPartisipant> TeamPartisipants { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-8AGUDFK; Database=DBLab1; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Admition>(entity =>
            {
                entity.HasKey(e => e.AdmittedId);

                entity.Property(e => e.AdmittedId).HasColumnName("Admitted_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.Property(e => e.CompetitionId).HasColumnName("Competition_id");

                entity.Property(e => e.IdComplexity).HasColumnName("id_Complexity");

                entity.Property(e => e.IdType).HasColumnName("id_Type");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.StartTax)
                    .HasColumnType("money")
                    .HasColumnName("Start_tax");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_time");

                entity.HasOne(d => d.IdComplexityNavigation)
                    .WithMany(p => p.Competitions)
                    .HasForeignKey(d => d.IdComplexity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competitions_Complexities");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Competitions)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competition_Types");
            });

            modelBuilder.Entity<CompetitionTeam>(entity =>
            {
                entity.ToTable("CompetitionTeam");

                entity.Property(e => e.CompetitionTeamId).HasColumnName("CompetitionTeam_id");

                entity.Property(e => e.AdmittedId).HasColumnName("Admitted_id");

                entity.Property(e => e.CompetitionId).HasColumnName("Competition_id");

                entity.Property(e => e.Penalty).HasColumnName("penalty");

                entity.Property(e => e.ResultTime).HasColumnName("result_time");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.HasOne(d => d.Admitted)
                    .WithMany(p => p.CompetitionTeams)
                    .HasForeignKey(d => d.AdmittedId)
                    .HasConstraintName("FK_CompetitionTeam_Admitions");

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.CompetitionTeams)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competition/Team_Competitions");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.CompetitionTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competition/Team_Teams");
            });

            modelBuilder.Entity<Complexity>(entity =>
            {
                entity.Property(e => e.ComplexityId).HasColumnName("Complexity_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Obstacle>(entity =>
            {
                entity.Property(e => e.ObstacleId).HasColumnName("Obstacle_id");

                entity.Property(e => e.AdditionalDescription).HasColumnType("ntext");

                entity.Property(e => e.ConditionalComplexity).HasColumnType("ntext");

                entity.Property(e => e.ConditionsOvercoming).HasColumnType("ntext");

                entity.Property(e => e.EquipmentObstacle).HasColumnType("ntext");

                entity.Property(e => e.EquipmentStart).HasColumnType("ntext");

                entity.Property(e => e.EquipmentTarget).HasColumnType("ntext");

                entity.Property(e => e.MovementFirst).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<ObstacleCompetition>(entity =>
            {
                entity.ToTable("ObstacleCompetition");

                entity.Property(e => e.ObstacleCompetitionId).HasColumnName("ObstacleCompetition_id");

                entity.Property(e => e.CompetitionId).HasColumnName("Competition_id");

                entity.Property(e => e.ObstacleId).HasColumnName("Obstacle_id");

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.ObstacleCompetitions)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Obstacle/competition_Competition");

                entity.HasOne(d => d.Obstacle)
                    .WithMany(p => p.ObstacleCompetitions)
                    .HasForeignKey(d => d.ObstacleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Obstacle/competition_Obstacles");
            });

            modelBuilder.Entity<Partisipant>(entity =>
            {
                entity.HasKey(e => e.ParticipantId);

                entity.Property(e => e.ParticipantId)
                    .ValueGeneratedNever()
                    .HasColumnName("Participant_id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_birth");

                entity.Property(e => e.FileInsurance).HasColumnType("image");

                entity.Property(e => e.IdRole).HasColumnName("id_Role");

                entity.Property(e => e.IdSex).HasColumnName("id_Sex");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.PhoneNumber).HasColumnName("Phone_number");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Partisipants)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partisipants_Statuses");

                entity.HasOne(d => d.IdSexNavigation)
                    .WithMany(p => p.Partisipants)
                    .HasForeignKey(d => d.IdSex)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partisipants_Sexes");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.Property(e => e.RankId).HasColumnName("Rank_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<RankPartisipant>(entity =>
            {
                entity.ToTable("RankPartisipant");

                entity.Property(e => e.RankPartisipantId).HasColumnName("RankPartisipant_id");

                entity.Property(e => e.DateOfAchievement)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_achievement");

                entity.Property(e => e.PartisipantId).HasColumnName("Partisipant_id");

                entity.Property(e => e.RankId).HasColumnName("Rank_id");

                entity.HasOne(d => d.Partisipant)
                    .WithMany(p => p.RankPartisipants)
                    .HasForeignKey(d => d.PartisipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rank/partisipant_Partisipants");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.RankPartisipants)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rank/partisipant_Ranks");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.Property(e => e.ResultId).HasColumnName("Result_id");

                entity.Property(e => e.ObstacleCompetitionId).HasColumnName("ObstacleCompetition_id");

                entity.Property(e => e.PartisipantId).HasColumnName("Partisipant_id");

                entity.Property(e => e.Penalty).HasColumnName("penalty");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.ObstacleCompetition)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.ObstacleCompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Results_Obstacle/competition");

                entity.HasOne(d => d.Partisipant)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.PartisipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Results_Partisipants");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolesId)
                    .HasName("PK_Statuses");

                entity.Property(e => e.RolesId).HasColumnName("Roles_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.Property(e => e.SexId).HasColumnName("Sex_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.FileDocument).HasColumnType("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<TeamPartisipant>(entity =>
            {
                entity.ToTable("TeamPartisipant");

                entity.Property(e => e.TeamPartisipantId).HasColumnName("TeamPartisipant_id");

                entity.Property(e => e.Participated)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.PartisipantId).HasColumnName("Partisipant_id");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.HasOne(d => d.Partisipant)
                    .WithMany(p => p.TeamPartisipants)
                    .HasForeignKey(d => d.PartisipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team/partisipant_Partisipants");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamPartisipants)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team/partisipant_Teams");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.TypeId).HasColumnName("Type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
