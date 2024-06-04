using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projRESTfulApiFitConnect.Models;

public partial class GymContext : DbContext
{
    public GymContext()
    {
    }

    public GymContext(DbContextOptions<GymContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TGym> TGyms { get; set; }

    public virtual DbSet<TIdentity> TIdentities { get; set; }

    public virtual DbSet<Tcity> Tcities { get; set; }

    public virtual DbSet<Tclass> Tclasses { get; set; }

    public virtual DbSet<TclassLimitDetail> TclassLimitDetails { get; set; }

    public virtual DbSet<TclassReserve> TclassReserves { get; set; }

    public virtual DbSet<TclassSchedule> TclassSchedules { get; set; }

    public virtual DbSet<TclassSort有氧> TclassSort有氧s { get; set; }

    public virtual DbSet<TclassSort訓練> TclassSort訓練s { get; set; }

    public virtual DbSet<TclassStatusDetail> TclassStatusDetails { get; set; }

    public virtual DbSet<TcoachExpert> TcoachExperts { get; set; }

    public virtual DbSet<TcoachInfoId> TcoachInfoIds { get; set; }

    public virtual DbSet<TcoachPhoto> TcoachPhotos { get; set; }

    public virtual DbSet<Tcompany> Tcompanies { get; set; }

    public virtual DbSet<Tfield> Tfields { get; set; }

    public virtual DbSet<TfieldPhoto> TfieldPhotos { get; set; }

    public virtual DbSet<TfieldReserve> TfieldReserves { get; set; }

    public virtual DbSet<TgenderTable> TgenderTables { get; set; }

    public virtual DbSet<TidentityRoleDetail> TidentityRoleDetails { get; set; }

    public virtual DbSet<TmemberFollow> TmemberFollows { get; set; }

    public virtual DbSet<TmemberRateClass> TmemberRateClasses { get; set; }

    public virtual DbSet<TmemberStatusDetail> TmemberStatusDetails { get; set; }

    public virtual DbSet<Towner> Towners { get; set; }

    public virtual DbSet<TregionTable> TregionTables { get; set; }

    public virtual DbSet<TtimesDetail> TtimesDetails { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=gym;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TGym>(entity =>
        {
            entity.HasKey(e => e.GymId);

            entity.ToTable("tGym");

            entity.Property(e => e.GymId).HasColumnName("Gym_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Describe).HasColumnName("describe");
            entity.Property(e => e.EMail)
                .HasMaxLength(20)
                .HasColumnName("e-mail");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Time)
                .HasMaxLength(20)
                .HasColumnName("time");
            entity.Property(e => e.Website).HasColumnName("website");

            entity.HasOne(d => d.Company).WithMany(p => p.TGyms)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGym_tcompany");

            entity.HasOne(d => d.Region).WithMany(p => p.TGyms)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGym_tregion_table");
        });

        modelBuilder.Entity<TIdentity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Identity");

            entity.ToTable("tIdentity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activated)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activated");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.EMail)
                .HasMaxLength(20)
                .HasColumnName("e-mail");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Payment)
                .HasColumnType("money")
                .HasColumnName("payment");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Gender).WithMany(p => p.TIdentities)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Identity_gender_Table1");

            entity.HasOne(d => d.Role).WithMany(p => p.TIdentities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Identity_identity_role_detail1");
        });

        modelBuilder.Entity<Tcity>(entity =>
        {
            entity.HasKey(e => e.CityId);

            entity.ToTable("tcity");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
        });

        modelBuilder.Entity<Tclass>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK_class");

            entity.ToTable("tclasses");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassIntroduction).HasColumnName("class_introduction");
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .HasColumnName("class_name");
            entity.Property(e => e.ClassPhoto).HasColumnName("class_photo");
            entity.Property(e => e.ClassSort1Id).HasColumnName("class_sort1_id");
            entity.Property(e => e.ClassSort2Id).HasColumnName("class_sort2_id");
            entity.Property(e => e.ClassStatus).HasColumnName("class_status");
            entity.Property(e => e.LimitedGender).HasColumnName("limited_gender");

            entity.HasOne(d => d.ClassSort1).WithMany(p => p.Tclasses)
                .HasForeignKey(d => d.ClassSort1Id)
                .HasConstraintName("FK_classes_class_sort_有氧");

            entity.HasOne(d => d.ClassSort2).WithMany(p => p.Tclasses)
                .HasForeignKey(d => d.ClassSort2Id)
                .HasConstraintName("FK_classes_class_sort_訓練");

            entity.HasOne(d => d.LimitedGenderNavigation).WithMany(p => p.Tclasses)
                .HasForeignKey(d => d.LimitedGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_classes_class_limit_details");
        });

        modelBuilder.Entity<TclassLimitDetail>(entity =>
        {
            entity.HasKey(e => e.ClassLimitedId).HasName("PK_class_limit_details");

            entity.ToTable("tclass_limit_details");

            entity.Property(e => e.ClassLimitedId)
                .ValueGeneratedNever()
                .HasColumnName("class_limited_id");
            entity.Property(e => e.Describe)
                .HasMaxLength(50)
                .HasColumnName("describe");
        });

        modelBuilder.Entity<TclassReserve>(entity =>
        {
            entity.HasKey(e => e.ReserveId).HasName("PK_reserve");

            entity.ToTable("tclass_reserve");

            entity.Property(e => e.ReserveId).HasColumnName("reserve_id");
            entity.Property(e => e.ClassScheduleId).HasColumnName("class_schedule_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.ReserveStatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("reserve_status");

            entity.HasOne(d => d.ClassSchedule).WithMany(p => p.TclassReserves)
                .HasForeignKey(d => d.ClassScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_reserve_class_schedule");

            entity.HasOne(d => d.Member).WithMany(p => p.TclassReserves)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_reserve_Identity1");
        });

        modelBuilder.Entity<TclassSchedule>(entity =>
        {
            entity.HasKey(e => e.ClassScheduleId).HasName("PK_class_schedule");

            entity.ToTable("tclass_schedule");

            entity.Property(e => e.ClassScheduleId).HasColumnName("class_schedule_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassPayment)
                .HasColumnType("money")
                .HasColumnName("class_payment");
            entity.Property(e => e.ClassStatusId)
                .HasDefaultValueSql("((2))")
                .HasColumnName("class_status_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.CoachPayment).HasColumnName("coach_payment");
            entity.Property(e => e.CourseDate)
                .HasColumnType("date")
                .HasColumnName("course_date");
            entity.Property(e => e.CourseTimeId).HasColumnName("course_time_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.MaxStudent).HasColumnName("Max_student");

            entity.HasOne(d => d.Class).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_schedule_classes");

            entity.HasOne(d => d.ClassStatus).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.ClassStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_schedule_class_status_detail");

            entity.HasOne(d => d.Coach).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_schedule_Identity");

            entity.HasOne(d => d.CourseTime).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.CourseTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tclass_schedule_ttimes_detail");

            entity.HasOne(d => d.Field).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_schedule_field");
        });

        modelBuilder.Entity<TclassSort有氧>(entity =>
        {
            entity.HasKey(e => e.ClassSort1Id).HasName("PK_class_sort_有氧");

            entity.ToTable("tclass_sort_有氧");

            entity.Property(e => e.ClassSort1Id).HasColumnName("class_sort1_id");
            entity.Property(e => e.ClassSort1Detail)
                .HasMaxLength(50)
                .HasColumnName("class_sort1_detail");
        });

        modelBuilder.Entity<TclassSort訓練>(entity =>
        {
            entity.HasKey(e => e.ClassSort2Id).HasName("PK_class_sort_訓練");

            entity.ToTable("tclass_sort_訓練");

            entity.Property(e => e.ClassSort2Id).HasColumnName("class_sort2_id");
            entity.Property(e => e.ClassSort2Detail)
                .HasMaxLength(50)
                .HasColumnName("class_sort2_detail");
        });

        modelBuilder.Entity<TclassStatusDetail>(entity =>
        {
            entity.HasKey(e => e.ClassStatusId).HasName("PK_class_status_detail");

            entity.ToTable("tclass_status_detail");

            entity.Property(e => e.ClassStatusId).HasColumnName("class_status_id");
            entity.Property(e => e.ClassStatusDiscribe)
                .HasMaxLength(50)
                .HasColumnName("class_status_discribe");
        });

        modelBuilder.Entity<TcoachExpert>(entity =>
        {
            entity.HasKey(e => e.ExpertId).HasName("PK_coach_expert");

            entity.ToTable("tcoach_expert");

            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");

            entity.HasOne(d => d.Class).WithMany(p => p.TcoachExperts)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_coach_expert_classes");

            entity.HasOne(d => d.Coach).WithMany(p => p.TcoachExperts)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_coach_expert_Identity");
        });

        modelBuilder.Entity<TcoachInfoId>(entity =>
        {
            entity.HasKey(e => e.CoachInfoId).HasName("PK_coach_info_id");

            entity.ToTable("tcoach_info_id");

            entity.Property(e => e.CoachInfoId).HasColumnName("coach_info_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.CoachIntro).HasColumnName("coach_intro");

            entity.HasOne(d => d.Coach).WithMany(p => p.TcoachInfoIds)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_coach_info_id_Identity");
        });

        modelBuilder.Entity<TcoachPhoto>(entity =>
        {
            entity.HasKey(e => e.CoachPhotoId);

            entity.ToTable("tcoach_photo");

            entity.Property(e => e.CoachPhotoId).HasColumnName("coach_photo_id");
            entity.Property(e => e.CoachPhoto).HasColumnName("coach_photo");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");

            entity.HasOne(d => d.Expert).WithMany(p => p.TcoachPhotos)
                .HasForeignKey(d => d.ExpertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tcoach_photo_tcoach_expert");
        });

        modelBuilder.Entity<Tcompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId);

            entity.ToTable("tcompany");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Timelimit)
                .HasColumnType("date")
                .HasColumnName("timelimit");

            entity.HasOne(d => d.Owner).WithMany(p => p.Tcompanies)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tcompany_towner");
        });

        modelBuilder.Entity<Tfield>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK_field");

            entity.ToTable("tfield");

            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .HasColumnName("field_name");
            entity.Property(e => e.FieldPayment)
                .HasColumnType("money")
                .HasColumnName("field_payment");
            entity.Property(e => e.Floor)
                .HasMaxLength(50)
                .HasColumnName("floor");
            entity.Property(e => e.GymId).HasColumnName("Gym_id");

            entity.HasOne(d => d.Gym).WithMany(p => p.Tfields)
                .HasForeignKey(d => d.GymId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tfield_tGym");
        });

        modelBuilder.Entity<TfieldPhoto>(entity =>
        {
            entity.HasKey(e => e.FieldPhotoId);

            entity.ToTable("tfield_photo");

            entity.Property(e => e.FieldPhotoId).HasColumnName("field_photo_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.FieldPhoto).HasColumnName("field_photo");

            entity.HasOne(d => d.Field).WithMany(p => p.TfieldPhotos)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tfield_photo_tfield");
        });

        modelBuilder.Entity<TfieldReserve>(entity =>
        {
            entity.HasKey(e => e.FieldReserveId).HasName("PK_field_reserve");

            entity.ToTable("tfield_reserve");

            entity.Property(e => e.FieldReserveId).HasColumnName("field_reserve_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.ReserveStatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("reserve_status");

            entity.HasOne(d => d.Coach).WithMany(p => p.TfieldReserves)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_field_reserve_Identity1");

            entity.HasOne(d => d.Field).WithMany(p => p.TfieldReserves)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_field_reserve_field");
        });

        modelBuilder.Entity<TgenderTable>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK_gender_Table");

            entity.ToTable("tgender_Table");

            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.GenderText)
                .HasMaxLength(4)
                .HasColumnName("gender_text");
        });

        modelBuilder.Entity<TidentityRoleDetail>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_identity_role_detail");

            entity.ToTable("tidentity_role_detail");

            entity.Property(e => e.RoleId).HasColumnName("role_Id");
            entity.Property(e => e.RoleDescribe)
                .HasMaxLength(50)
                .HasColumnName("role_describe");
        });

        modelBuilder.Entity<TmemberFollow>(entity =>
        {
            entity.HasKey(e => e.FollowId).HasName("PK_member_follow");

            entity.ToTable("tmember_follow");

            entity.Property(e => e.FollowId).HasColumnName("follow_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.TmemberFollowCoaches)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmember_follow_tIdentity");

            entity.HasOne(d => d.Member).WithMany(p => p.TmemberFollowMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_member_follow_Identity1");

            entity.HasOne(d => d.Status).WithMany(p => p.TmemberFollows)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_member_follow_member_status_details");
        });

        modelBuilder.Entity<TmemberRateClass>(entity =>
        {
            entity.HasKey(e => e.RateId);

            entity.ToTable("tmember_rate_class");

            entity.Property(e => e.RateId)
                .ValueGeneratedNever()
                .HasColumnName("rate_id");
            entity.Property(e => e.ClassDescribe).HasColumnName("class_describe");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CoachDescribe).HasColumnName("coach_describe");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.RateClass)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("rate_class");
            entity.Property(e => e.RateCoach)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("rate_coach");
            entity.Property(e => e.ReserveId).HasColumnName("reserve_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.TmemberRateClasses)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmember_rate_class_tIdentity");

            entity.HasOne(d => d.Reserve).WithMany(p => p.TmemberRateClasses)
                .HasForeignKey(d => d.ReserveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmember_rate_class_tclass_reserve");
        });

        modelBuilder.Entity<TmemberStatusDetail>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK_status_id");

            entity.ToTable("tmember_status_details");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusDescribe)
                .HasMaxLength(20)
                .HasColumnName("status_describe");
        });

        modelBuilder.Entity<Towner>(entity =>
        {
            entity.HasKey(e => e.OwnerId);

            entity.ToTable("towner");

            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Owner)
                .HasMaxLength(20)
                .HasColumnName("owner");
        });

        modelBuilder.Entity<TregionTable>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK_region_table");

            entity.ToTable("tregion_table");

            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");

            entity.HasOne(d => d.City).WithMany(p => p.TregionTables)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tregion_table_tcity");
        });

        modelBuilder.Entity<TtimesDetail>(entity =>
        {
            entity.HasKey(e => e.TimeId);

            entity.ToTable("ttimes_detail");

            entity.Property(e => e.TimeId).HasColumnName("time_id");
            entity.Property(e => e.TimeName)
                .HasPrecision(0)
                .HasColumnName("time_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
