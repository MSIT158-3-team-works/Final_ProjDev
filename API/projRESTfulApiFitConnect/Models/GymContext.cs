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

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<ClassReservedDetail> ClassReservedDetails { get; set; }

    public virtual DbSet<FieldReservedDetail> FieldReservedDetails { get; set; }

    public virtual DbSet<GymInfoDetail> GymInfoDetails { get; set; }

    public virtual DbSet<TGym> TGyms { get; set; }

    public virtual DbSet<TGymTime> TGymTimes { get; set; }

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

    public virtual DbSet<TcoursePhoto> TcoursePhotos { get; set; }

    public virtual DbSet<Tdiscount> Tdiscounts { get; set; }

    public virtual DbSet<Tfield> Tfields { get; set; }

    public virtual DbSet<TfieldPhoto> TfieldPhotos { get; set; }

    public virtual DbSet<TfieldReserve> TfieldReserves { get; set; }

    public virtual DbSet<TgenderTable> TgenderTables { get; set; }

    public virtual DbSet<TidentityRoleDetail> TidentityRoleDetails { get; set; }

    public virtual DbSet<TlogisticsStatus> TlogisticsStatuses { get; set; }

    public virtual DbSet<TmemberFollow> TmemberFollows { get; set; }

    public virtual DbSet<TmemberRateClass> TmemberRateClasses { get; set; }

    public virtual DbSet<TmemberStatusDetail> TmemberStatusDetails { get; set; }

    public virtual DbSet<Tnews> Tnews { get; set; }

    public virtual DbSet<TnewsCategory> TnewsCategories { get; set; }

    public virtual DbSet<TnewsComment> TnewsComments { get; set; }

    public virtual DbSet<TnewsMedium> TnewsMedia { get; set; }

    public virtual DbSet<Torder> Torders { get; set; }

    public virtual DbSet<TorderDetail> TorderDetails { get; set; }

    public virtual DbSet<TorderStatus> TorderStatuses { get; set; }

    public virtual DbSet<Towner> Towners { get; set; }

    public virtual DbSet<Tpayment> Tpayments { get; set; }

    public virtual DbSet<TpaymentMethod> TpaymentMethods { get; set; }

    public virtual DbSet<Tpaymentrole> Tpaymentroles { get; set; }

    public virtual DbSet<Tproduct> Tproducts { get; set; }

    public virtual DbSet<TproductCategory> TproductCategories { get; set; }

    public virtual DbSet<TproductImage> TproductImages { get; set; }

    public virtual DbSet<TproductShoppingcart> TproductShoppingcarts { get; set; }

    public virtual DbSet<TproductTrack> TproductTracks { get; set; }

    public virtual DbSet<TregionTable> TregionTables { get; set; }

    public virtual DbSet<TshippingMethod> TshippingMethods { get; set; }

    public virtual DbSet<TtimesDetail> TtimesDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=gym;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(10)
                .HasColumnName("city");
            entity.Property(e => e.Road)
                .HasMaxLength(200)
                .HasColumnName("road");
            entity.Property(e => e.SiteId)
                .HasMaxLength(50)
                .HasColumnName("site_id");
        });

        modelBuilder.Entity<ClassReservedDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("class_reserved_detail");

            entity.Property(e => e.ClassIntroduction).HasColumnName("class_introduction");
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .HasColumnName("class_name");
            entity.Property(e => e.ClassPayment)
                .HasColumnType("money")
                .HasColumnName("class_payment");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.CourseDate).HasColumnName("course_date");
            entity.Property(e => e.CourseEndTimeId).HasColumnName("course_end_time_id");
            entity.Property(e => e.CourseStartTimeId).HasColumnName("course_start_time_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.MaxStudent).HasColumnName("Max_student");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.ReserveId).HasColumnName("reserve_id");
            entity.Property(e => e.ReserveStatus).HasColumnName("reserve_status");
        });

        modelBuilder.Entity<FieldReservedDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("field_reserved_detail");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.FieldDate).HasColumnName("field_date");
            entity.Property(e => e.FieldDescribe).HasColumnName("field_describe");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .HasColumnName("field_name");
            entity.Property(e => e.FieldPayment)
                .HasColumnType("money")
                .HasColumnName("field_payment");
            entity.Property(e => e.FieldReserveEndTime).HasColumnName("field_reserve__end_time");
            entity.Property(e => e.FieldReserveId).HasColumnName("field_reserve_id");
            entity.Property(e => e.FieldReserveStartTime).HasColumnName("field_reserve__start_time");
            entity.Property(e => e.Floor)
                .HasMaxLength(50)
                .HasColumnName("floor");
            entity.Property(e => e.GymAddress)
                .HasMaxLength(50)
                .HasColumnName("Gym_address");
            entity.Property(e => e.GymDescribe).HasColumnName("Gym_describe");
            entity.Property(e => e.GymName)
                .HasMaxLength(20)
                .HasColumnName("Gym_name");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
        });

        modelBuilder.Entity<GymInfoDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("gym_info_detail");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.GymAddress)
                .HasMaxLength(50)
                .HasColumnName("Gym_address");
            entity.Property(e => e.GymDescribe).HasColumnName("Gym_describe");
            entity.Property(e => e.GymId).HasColumnName("Gym_id");
            entity.Property(e => e.GymName)
                .HasMaxLength(20)
                .HasColumnName("Gym_name");
            entity.Property(e => e.GymPark)
                .HasMaxLength(50)
                .HasColumnName("Gym_park");
            entity.Property(e => e.GymPhoto).HasColumnName("Gym_photo");
            entity.Property(e => e.GymStatus).HasColumnName("Gym_status");
            entity.Property(e => e.GymTime)
                .HasMaxLength(20)
                .HasColumnName("Gym_time");
            entity.Property(e => e.GymTraffic)
                .HasMaxLength(50)
                .HasColumnName("Gym_traffic");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
        });

        modelBuilder.Entity<TGym>(entity =>
        {
            entity.HasKey(e => e.GymId);

            entity.ToTable("tGym");

            entity.Property(e => e.GymId).HasColumnName("Gym_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry date");
            entity.Property(e => e.GymAddress)
                .HasMaxLength(50)
                .HasColumnName("Gym_address");
            entity.Property(e => e.GymDescribe).HasColumnName("Gym_describe");
            entity.Property(e => e.GymName)
                .HasMaxLength(20)
                .HasColumnName("Gym_name");
            entity.Property(e => e.GymPark)
                .HasMaxLength(50)
                .HasColumnName("Gym_park");
            entity.Property(e => e.GymPhone)
                .HasMaxLength(20)
                .HasColumnName("Gym_phone");
            entity.Property(e => e.GymPhoto).HasColumnName("Gym_photo");
            entity.Property(e => e.GymStatus).HasColumnName("Gym_status");
            entity.Property(e => e.GymTime)
                .HasMaxLength(20)
                .HasColumnName("Gym_time");
            entity.Property(e => e.GymTraffic)
                .HasMaxLength(50)
                .HasColumnName("Gym_traffic");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.Company).WithMany(p => p.TGyms)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGym_tcompany");

            entity.HasOne(d => d.Region).WithMany(p => p.TGyms)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGym_tregion_table");
        });

        modelBuilder.Entity<TGymTime>(entity =>
        {
            entity.ToTable("tGym_time");

            entity.Property(e => e.TGymTimeId).HasColumnName("tGym_time_id");
            entity.Property(e => e.GymId).HasColumnName("Gym_id");
            entity.Property(e => e.GymTime).HasColumnName("Gym_time");

            entity.HasOne(d => d.Gym).WithMany(p => p.TGymTimes)
                .HasForeignKey(d => d.GymId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGym_time_tGym");

            entity.HasOne(d => d.GymTimeNavigation).WithMany(p => p.TGymTimes)
                .HasForeignKey(d => d.GymTime)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tGym_time_ttimes_detail");
        });

        modelBuilder.Entity<TIdentity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Identity");

            entity.ToTable("tIdentity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activated)
                .HasDefaultValue(true)
                .HasColumnName("activated");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
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

            entity.Property(e => e.ClassLimitedId).HasColumnName("class_limited_id");
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
                .HasDefaultValue(true)
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
                .HasDefaultValue(2)
                .HasColumnName("class_status_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.CoachPayment).HasColumnName("coach_payment");
            entity.Property(e => e.CourseDate).HasColumnName("course_date");
            entity.Property(e => e.CourseEndTimeId).HasColumnName("course_end_time_id");
            entity.Property(e => e.CourseStartTimeId).HasColumnName("course_start_time_id");
            entity.Property(e => e.CourseTimeId).HasColumnName("course_time_id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.FieldReservedId).HasColumnName("field_reserved_id");
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

            entity.HasOne(d => d.CourseEndTime).WithMany(p => p.TclassScheduleCourseEndTimes)
                .HasForeignKey(d => d.CourseEndTimeId)
                .HasConstraintName("FK_tclass_schedule_ttimes_detail1");

            entity.HasOne(d => d.CourseStartTime).WithMany(p => p.TclassScheduleCourseStartTimes)
                .HasForeignKey(d => d.CourseStartTimeId)
                .HasConstraintName("FK_tclass_schedule_ttimes_detail");

            entity.HasOne(d => d.Field).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_class_schedule_field");

            entity.HasOne(d => d.FieldReserved).WithMany(p => p.TclassSchedules)
                .HasForeignKey(d => d.FieldReservedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tclass_schedule_tfield_reserve");
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
            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.TcoachPhotos)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tcoach_photo_tIdentity");
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
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Timelimit).HasColumnName("timelimit");

            entity.HasOne(d => d.Owner).WithMany(p => p.Tcompanies)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tcompany_towner");
        });

        modelBuilder.Entity<TcoursePhoto>(entity =>
        {
            entity.HasKey(e => e.CoursePhotoId);

            entity.ToTable("tcourse_photo");

            entity.Property(e => e.CoursePhotoId).HasColumnName("course_photo_id");
            entity.Property(e => e.ClassScheduleId).HasColumnName("class_schedule_id");
            entity.Property(e => e.CoursePhoto).HasColumnName("course_photo");

            entity.HasOne(d => d.ClassSchedule).WithMany(p => p.TcoursePhotos)
                .HasForeignKey(d => d.ClassScheduleId)
                .HasConstraintName("FK_tcourse_photo_tclass_schedule");
        });

        modelBuilder.Entity<Tdiscount>(entity =>
        {
            entity.HasKey(e => e.DiscountId);

            entity.ToTable("tdiscount");

            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(16)
                .HasColumnName("discount_code");
            entity.Property(e => e.DiscountCondition)
                .HasMaxLength(20)
                .HasColumnName("discount_condition");
            entity.Property(e => e.DiscountExpire)
                .HasMaxLength(20)
                .HasColumnName("discount_expire");
            entity.Property(e => e.DiscountOpened)
                .HasDefaultValue(true)
                .HasColumnName("discount_opened");
            entity.Property(e => e.DiscountValue)
                .HasMaxLength(10)
                .HasDefaultValueSql("((30))")
                .HasColumnName("discount_value");
        });

        modelBuilder.Entity<Tfield>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK_field");

            entity.ToTable("tfield");

            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.FieldDescribe).HasColumnName("field_describe");
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
            entity.Property(e => e.Status).HasColumnName("status");

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
            entity.Property(e => e.FieldDate).HasColumnName("field_date");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
            entity.Property(e => e.FieldReserveEndTime).HasColumnName("field_reserve__end_time");
            entity.Property(e => e.FieldReserveStartTime).HasColumnName("field_reserve__start_time");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.ReserveStatus)
                .HasDefaultValue(true)
                .HasColumnName("reserve_status");

            entity.HasOne(d => d.Coach).WithMany(p => p.TfieldReserves)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_field_reserve_Identity1");

            entity.HasOne(d => d.Field).WithMany(p => p.TfieldReserves)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_field_reserve_field");

            entity.HasOne(d => d.FieldReserveEndTimeNavigation).WithMany(p => p.TfieldReserveFieldReserveEndTimeNavigations)
                .HasForeignKey(d => d.FieldReserveEndTime)
                .HasConstraintName("FK_tfield_reserve_tGym_time1");

            entity.HasOne(d => d.FieldReserveStartTimeNavigation).WithMany(p => p.TfieldReserveFieldReserveStartTimeNavigations)
                .HasForeignKey(d => d.FieldReserveStartTime)
                .HasConstraintName("FK_tfield_reserve_tGym_time");
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

        modelBuilder.Entity<TlogisticsStatus>(entity =>
        {
            entity.HasKey(e => e.LogisticsStatusId);

            entity.ToTable("tlogistics_status");

            entity.Property(e => e.LogisticsStatusId).HasColumnName("logistics_status_id");
            entity.Property(e => e.LogisticsStatus)
                .HasMaxLength(20)
                .HasColumnName("logistics_status");
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

            entity.Property(e => e.RateId).HasColumnName("rate_id");
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

        modelBuilder.Entity<Tnews>(entity =>
        {
            entity.HasKey(e => e.NewsId);

            entity.ToTable("tnews");

            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.NewsCategoryId).HasColumnName("news_category_id");
            entity.Property(e => e.NewsContent).HasColumnName("news_content");
            entity.Property(e => e.NewsDate).HasColumnName("news_date");
            entity.Property(e => e.NewsTitle)
                .HasMaxLength(50)
                .HasColumnName("news_title");

            entity.HasOne(d => d.NewsCategory).WithMany(p => p.Tnews)
                .HasForeignKey(d => d.NewsCategoryId)
                .HasConstraintName("FK_tnews_tnews_categories");
        });

        modelBuilder.Entity<TnewsCategory>(entity =>
        {
            entity.HasKey(e => e.NewsCategoryId);

            entity.ToTable("tnews_categories");

            entity.Property(e => e.NewsCategoryId).HasColumnName("news_category_id");
            entity.Property(e => e.NewsCategory)
                .HasMaxLength(20)
                .HasColumnName("news_category");
        });

        modelBuilder.Entity<TnewsComment>(entity =>
        {
            entity.HasKey(e => e.NewsCommentId);

            entity.ToTable("tnews_comment");

            entity.Property(e => e.NewsCommentId)
                .ValueGeneratedNever()
                .HasColumnName("news_comment_id");
            entity.Property(e => e.CommenterId).HasColumnName("commenter_id");
            entity.Property(e => e.NewsComment).HasColumnName("news_comment");
            entity.Property(e => e.NewsId).HasColumnName("news_id");

            entity.HasOne(d => d.Commenter).WithMany(p => p.TnewsComments)
                .HasForeignKey(d => d.CommenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tnews_comment_tIdentity");

            entity.HasOne(d => d.News).WithMany(p => p.TnewsComments)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tnews_comment_tnews");
        });

        modelBuilder.Entity<TnewsMedium>(entity =>
        {
            entity.HasKey(e => e.NewsPhotoId).HasName("PK_tnews_photo");

            entity.ToTable("tnews_media");

            entity.Property(e => e.NewsPhotoId)
                .ValueGeneratedNever()
                .HasColumnName("news_photo_id");
            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.NewsPhoto).HasColumnName("news_photo");
            entity.Property(e => e.NewsVideolink).HasColumnName("news_videolink");

            entity.HasOne(d => d.News).WithMany(p => p.TnewsMedia)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tnews_photo_tnews");
        });

        modelBuilder.Entity<Torder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("torder");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Consignee)
                .HasMaxLength(20)
                .HasColumnName("consignee");
            entity.Property(e => e.ConsigneePhone)
                .HasMaxLength(16)
                .HasColumnName("consignee_phone");
            entity.Property(e => e.LogisticsStatus)
                .HasDefaultValue(1)
                .HasColumnName("logistics_status");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.OrderStatus)
                .HasDefaultValue(1)
                .HasColumnName("order_status");
            entity.Property(e => e.OrderedTime)
                .HasMaxLength(50)
                .HasColumnName("ordered_time");
            entity.Property(e => e.PaymentMethod)
                .HasDefaultValue(1)
                .HasColumnName("payment_method");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(50)
                .HasColumnName("shipping_address");
            entity.Property(e => e.ShippingMethod)
                .HasDefaultValue(1)
                .HasColumnName("shipping_method");

            entity.HasOne(d => d.LogisticsStatusNavigation).WithMany(p => p.Torders)
                .HasForeignKey(d => d.LogisticsStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_tlogistics_status");

            entity.HasOne(d => d.Member).WithMany(p => p.Torders)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_tIdentity");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.Torders)
                .HasForeignKey(d => d.OrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_torder_status");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Torders)
                .HasForeignKey(d => d.PaymentMethod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_tpayment_method");

            entity.HasOne(d => d.ShippingMethodNavigation).WithMany(p => p.Torders)
                .HasForeignKey(d => d.ShippingMethod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_tshipping_method");
        });

        modelBuilder.Entity<TorderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId);

            entity.ToTable("torder_detail");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderQuantity)
                .HasDefaultValue(1)
                .HasColumnName("order_quantity");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductUnitprice)
                .HasColumnType("money")
                .HasColumnName("product_unitprice");

            entity.HasOne(d => d.Discount).WithMany(p => p.TorderDetails)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_torder_detail_tdiscount");

            entity.HasOne(d => d.Order).WithMany(p => p.TorderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_detail_torder");

            entity.HasOne(d => d.Product).WithMany(p => p.TorderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_torder_detail_tproduct");
        });

        modelBuilder.Entity<TorderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId);

            entity.ToTable("torder_status");

            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .HasColumnName("order_status");
        });

        modelBuilder.Entity<Towner>(entity =>
        {
            entity.HasKey(e => e.OwnerId);

            entity.ToTable("towner");

            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Owner)
                .HasMaxLength(20)
                .HasColumnName("owner");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Tpayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("tpayment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Payment)
                .HasColumnType("money")
                .HasColumnName("payment");
            entity.Property(e => e.PaymentroleId).HasColumnName("paymentrole_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Paymentrole).WithMany(p => p.Tpayments)
                .HasForeignKey(d => d.PaymentroleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tpayment_tpaymentrole");
        });

        modelBuilder.Entity<TpaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId);

            entity.ToTable("tpayment_method");

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .HasColumnName("payment_method");
        });

        modelBuilder.Entity<Tpaymentrole>(entity =>
        {
            entity.HasKey(e => e.PaymentroleId);

            entity.ToTable("tpaymentrole");

            entity.Property(e => e.PaymentroleId).HasColumnName("paymentrole_id");
            entity.Property(e => e.Paymentdetail)
                .HasMaxLength(50)
                .HasColumnName("paymentdetail");
        });

        modelBuilder.Entity<Tproduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("tproduct");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ProductDetail).HasColumnName("product_detail");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(50)
                .HasColumnName("product_image");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductSupplied).HasColumnName("product_supplied");
            entity.Property(e => e.ProductUnitprice)
                .HasColumnType("money")
                .HasColumnName("product_unitprice");

            entity.HasOne(d => d.Category).WithMany(p => p.Tproducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_tproduct_tproduct_category");
        });

        modelBuilder.Entity<TproductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId);

            entity.ToTable("tproduct_category");

            entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_id");
            entity.Property(e => e.CategoryDescription).HasColumnName("category_description");
            entity.Property(e => e.CategoryImage)
                .HasMaxLength(50)
                .HasColumnName("category_image");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<TproductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImagesId);

            entity.ToTable("tproduct_images");

            entity.Property(e => e.ProductImagesId).HasColumnName("product_images_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductImages).HasColumnName("product_images");

            entity.HasOne(d => d.Product).WithMany(p => p.TproductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tproduct_images_tproduct");
        });

        modelBuilder.Entity<TproductShoppingcart>(entity =>
        {
            entity.HasKey(e => e.ProductShoppingcartId);

            entity.ToTable("tproduct_shoppingcart");

            entity.Property(e => e.ProductShoppingcartId).HasColumnName("product_shoppingcart_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductQuantity)
                .HasDefaultValue(1)
                .HasColumnName("product_quantity");
            entity.Property(e => e.ProductUnitprice)
                .HasColumnType("money")
                .HasColumnName("product_unitprice");

            entity.HasOne(d => d.Member).WithMany(p => p.TproductShoppingcarts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tproduct_shoppingcart_tIdentity");

            entity.HasOne(d => d.Product).WithMany(p => p.TproductShoppingcarts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tproduct_shoppingcart_tproduct");
        });

        modelBuilder.Entity<TproductTrack>(entity =>
        {
            entity.HasKey(e => e.ProductTrackId);

            entity.ToTable("tproduct_track");

            entity.Property(e => e.ProductTrackId).HasColumnName("product_track_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Member).WithMany(p => p.TproductTracks)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tproduct_track_tIdentity");

            entity.HasOne(d => d.Product).WithMany(p => p.TproductTracks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tproduct_track_tproduct");
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

        modelBuilder.Entity<TshippingMethod>(entity =>
        {
            entity.HasKey(e => e.ShippingMethodId);

            entity.ToTable("tshipping_method");

            entity.Property(e => e.ShippingMethodId).HasColumnName("shipping_method_id");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(20)
                .HasColumnName("shipping_method");
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
