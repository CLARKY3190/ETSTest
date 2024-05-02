using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ETS_API.Models;

public partial class EnterpriseTrackingSystemContext : DbContext
{
    private readonly IConfiguration Configuration;
    public EnterpriseTrackingSystemContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public EnterpriseTrackingSystemContext(DbContextOptions<EnterpriseTrackingSystemContext> options,  IConfiguration configuration)
        : base(options)
    {
         Configuration = configuration;
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Etsuser> Etsusers { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4CE669A010");

            entity.ToTable("Company");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedNever()
                .HasColumnName("CompanyID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1D287E09F");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Employee__Compan__267ABA7A");
        });

        modelBuilder.Entity<Etsuser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__ETSUser__1788CCAC39FDCF11");

            entity.ToTable("ETSUser");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Etsusers)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ETSUser__Employe__29572725");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__Expense__1445CFF3AA993F3E");

            entity.ToTable("Expense", tb => tb.HasTrigger("TR_Expense_PaymentMethod_Check"));

            entity.Property(e => e.ExpenseId)
                .ValueGeneratedNever()
                .HasColumnName("ExpenseID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ExpenseCategoryId).HasColumnName("ExpenseCategoryID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ExpenseCategory).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ExpenseCategoryId)
                .HasConstraintName("FK__Expense__Expense__3B75D760");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_Expense_PaymentMethod");

            entity.HasOne(d => d.User).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Expense__UserID__3A81B327");
        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.ExpenseCategoryId).HasName("PK__ExpenseC__9C2C63D8D4583BFE");

            entity.ToTable("ExpenseCategory");

            entity.Property(e => e.ExpenseCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("ExpenseCategoryID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F3B41B7D75");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId)
                .ValueGeneratedNever()
                .HasColumnName("PaymentMethodID");
            entity.Property(e => e.CardNumber).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PaymentMe__UserI__3F466844");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId).HasName("PK__Privileg__B3E77E3C04B91509");

            entity.ToTable("Privilege");

            entity.Property(e => e.PrivilegeId)
                .ValueGeneratedNever()
                .HasColumnName("PrivilegeID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A635568C6");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Privileges).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePrivilege",
                    r => r.HasOne<Privilege>().WithMany()
                        .HasForeignKey("PrivilegeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolePrivi__Privi__35BCFE0A"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolePrivi__RoleI__34C8D9D1"),
                    j =>
                    {
                        j.HasKey("RoleId", "PrivilegeId").HasName("PK__RolePriv__51C4B9D93A5B3A5C");
                        j.ToTable("RolePrivilege");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                        j.IndexerProperty<int>("PrivilegeId").HasColumnName("PrivilegeID");
                    });
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK__UserRole__AF27604FC2DD5E13");

            entity.ToTable("UserRole");

            entity.HasIndex(e => e.UserId, "UQ_UserRole_UserID").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__RoleID__31EC6D26");

            entity.HasOne(d => d.User).WithOne(p => p.UserRole)
                .HasForeignKey<UserRole>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__UserID__30F848ED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
