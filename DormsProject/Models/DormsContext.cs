using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DormsProject.Models;

public partial class DormsContext : DbContext
{
    public DormsContext()
    {
    }

    public DormsContext(DbContextOptions<DormsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Complex> Complexes { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Dorm> Dorms { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=dorms;Username=postgres;Password=raluca; Port=4747");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("address_pkey");

            entity.ToTable("address");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("address_id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdministratorId).HasName("administrator_pkey");

            entity.ToTable("administrator");

            entity.Property(e => e.AdministratorId)
                .ValueGeneratedNever()
                .HasColumnName("administrator_id");
            entity.Property(e => e.Cnp)
                .HasMaxLength(13)
                .HasColumnName("cnp");
            entity.Property(e => e.DormId).HasColumnName("dorm_id");

            entity.HasOne(d => d.CnpNavigation).WithMany(p => p.Administrators)
                .HasForeignKey(d => d.Cnp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("administrator_person_cnp_fkey");

            entity.HasOne(d => d.Dorm).WithMany(p => p.Administrators)
                .HasForeignKey(d => d.DormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("administrator_dorm_id_fkey");
        });

        modelBuilder.Entity<Complex>(entity =>
        {
            entity.HasKey(e => e.ComplexId).HasName("complex_pkey");

            entity.ToTable("complex");

            entity.Property(e => e.ComplexId)
                .ValueGeneratedNever()
                .HasColumnName("complex_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("contract_pkey");

            entity.ToTable("contract");

            entity.Property(e => e.ContractId)
                .ValueGeneratedNever()
                .HasColumnName("contract_id");
            entity.Property(e => e.RoomNumber).HasColumnName("room_number");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_room_number_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contract_student_id_fkey");
        });

        modelBuilder.Entity<Dorm>(entity =>
        {
            entity.HasKey(e => e.DormId).HasName("dorm_pkey");

            entity.ToTable("dorm");

            entity.Property(e => e.DormId)
                .ValueGeneratedNever()
                .HasColumnName("dorm_id");
            entity.Property(e => e.AdressId).HasColumnName("adress_id");
            entity.Property(e => e.ComplexId).HasColumnName("complex_id");

            entity.HasOne(d => d.Adress).WithMany(p => p.Dorms)
                .HasForeignKey(d => d.AdressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dorm_adress_id_fkey");

            entity.HasOne(d => d.Complex).WithMany(p => p.Dorms)
                .HasForeignKey(d => d.ComplexId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("complex_id");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Building).HasName("faculty_pkey");

            entity.ToTable("faculty");

            entity.Property(e => e.Building)
                .ValueGeneratedNever()
                .HasColumnType("char")
                .HasColumnName("building");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.DomainName)
                .HasMaxLength(70)
                .HasColumnName("domain_name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Address).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("faculty_address_id_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Cnp).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.Cnp)
                .HasMaxLength(13)
                .HasColumnName("cnp");
            entity.Property(e => e.AdressId).HasColumnName("adress_id");
            entity.Property(e => e.EMail)
                .HasMaxLength(50)
                .HasColumnName("e-mail");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");

            entity.HasOne(d => d.Adress).WithMany(p => p.People)
                .HasForeignKey(d => d.AdressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_adress_id_fkey");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfessorId).HasName("professor_pkey");

            entity.ToTable("professor");

            entity.Property(e => e.ProfessorId)
                .ValueGeneratedNever()
                .HasColumnName("professor_id");
            entity.Property(e => e.Building)
                .HasColumnType("char")
                .HasColumnName("building");
            entity.Property(e => e.Cnp)
                .HasMaxLength(13)
                .HasColumnName("cnp");
            entity.Property(e => e.RoomNumber).HasColumnName("room_number");

            entity.HasOne(d => d.BuildingNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.Building)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("professor_faculty_building_fkey");

            entity.HasOne(d => d.CnpNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.Cnp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("professor_person_cnp_fkey");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("professor_room_number_fkey");

            entity.HasMany(d => d.Buildings).WithMany(p => p.ProfessorsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfessorFaculty",
                    r => r.HasOne<Faculty>().WithMany()
                        .HasForeignKey("Building")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("faculty_building_fkey"),
                    l => l.HasOne<Professor>().WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("professor_id_fkey"),
                    j =>
                    {
                        j.HasKey("ProfessorId", "Building").HasName("professor_faculty_pkey");
                        j.ToTable("professor_faculty");
                        j.IndexerProperty<int>("ProfessorId").HasColumnName("professor_id");
                        j.IndexerProperty<char>("Building")
                            .HasColumnType("char")
                            .HasColumnName("building");
                    });
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomNumber).HasName("room_pkey");

            entity.ToTable("room");

            entity.Property(e => e.RoomNumber)
                .ValueGeneratedNever()
                .HasColumnName("room_number");
            entity.Property(e => e.DormId).HasColumnName("dorm_id");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.Isoccupied).HasColumnName("isoccupied");

            entity.HasOne(d => d.Dorm).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.DormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dorm_id");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("student_id");
            entity.Property(e => e.Cnp)
                .HasMaxLength(13)
                .HasColumnName("cnp");
            entity.Property(e => e.FormOfEducation)
                .HasMaxLength(20)
                .HasColumnName("form_of_education");
            entity.Property(e => e.RoomNumber).HasColumnName("room_number");
            entity.Property(e => e.StudyYear).HasColumnName("study_year");

            entity.HasOne(d => d.CnpNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Cnp)
                .HasConstraintName("student_person_cnp");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_room_number");

            entity.HasMany(d => d.Buildings).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentFaculty",
                    r => r.HasOne<Faculty>().WithMany()
                        .HasForeignKey("Building")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("faculty_building_fkey"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("student_id_fkey"),
                    j =>
                    {
                        j.HasKey("StudentId", "Building").HasName("student_faculty_pkey");
                        j.ToTable("student_faculty");
                        j.IndexerProperty<int>("StudentId").HasColumnName("student_id");
                        j.IndexerProperty<char>("Building")
                            .HasColumnType("char")
                            .HasColumnName("building");
                    });

            entity.HasMany(d => d.Professors).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentProfessor",
                    r => r.HasOne<Professor>().WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("professor_id_fkey"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("student_id_fkey"),
                    j =>
                    {
                        j.HasKey("StudentId", "ProfessorId").HasName("student_professor_pkey");
                        j.ToTable("student_professor");
                        j.IndexerProperty<int>("StudentId").HasColumnName("student_id");
                        j.IndexerProperty<int>("ProfessorId").HasColumnName("professor_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
