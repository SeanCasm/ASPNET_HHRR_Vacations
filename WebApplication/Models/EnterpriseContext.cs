using Microsoft.EntityFrameworkCore;

namespace ASPNET_HHRR_Vacations.Models;

public partial class EnterpriseContext : DbContext
{
    public EnterpriseContext()
    {
    }

    public EnterpriseContext(DbContextOptions<EnterpriseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<UserCredential> UserCredentials { get; set; }

    public virtual DbSet<Vacation> Vacations { get; set; }

    public virtual DbSet<VacationTicket> VacationTickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-E8C9POC7\\SQLEXPRESS;Database=Enterprise;TrustServerCertificate=True;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF139E2AC6E");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

        });
        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__RequestS__33A8519A6FF19F3A");

            entity.ToTable("RequestStatus");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.RequestType)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserCred__1788CCAC208918B0");

            entity.HasIndex(e => e.EmployeeId, "UQ__UserCred__7AD04FF07711573F").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.PasswordHash).IsUnicode(false);

            entity.HasOne(d => d.Employee).WithOne(p => p.UserCredential)
                .HasForeignKey<UserCredential>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserCrede__Emplo__2AD55B43");
        });

        modelBuilder.Entity<Vacation>(entity =>
        {
            entity.HasKey(e => e.VacationId).HasName("PK__Vacation__E420DF845A9863EF");

            entity.ToTable("Vacation");

            entity.Property(e => e.VacationId).HasColumnName("VacationID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Vacations)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Vacation__Employ__1F63A897");
        });

        modelBuilder.Entity<VacationTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Vacation__712CC627C77802E6");

            entity.ToTable("VacationTicket");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Issued)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.VacationId).HasColumnName("VacationID");

            entity.HasOne(d => d.Employee).WithMany(p => p.VacationTickets)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__VacationT__Emplo__24285DB4");

            entity.HasOne(d => d.Request).WithMany(p => p.VacationTickets)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__VacationT__Reque__2610A626");

            entity.HasOne(d => d.Vacation).WithMany(p => p.VacationTickets)
                .HasForeignKey(d => d.VacationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__VacationT__Vacat__251C81ED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
