using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer.DataBase;

public partial class MyBlockForumDbContext : DbContext
{
    public MyBlockForumDbContext()
    {
    }

    public MyBlockForumDbContext(DbContextOptions<MyBlockForumDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Thread> Threads { get; set; }

    public virtual DbSet<ThreadTheme> ThreadThemes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserThread> UserThreads { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-4Q61DMJ\\SQLEXPRESS2019;Database=MyBlockForum;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message__3213E83F9EC7B908");

            entity.ToTable("message");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text)
                .HasMaxLength(1024)
                .HasColumnName("text");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Thread).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message__thread___42E1EEFE");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message__user_id__41EDCAC5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83FDE251DA3");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__status__3213E83F8283DDA2");

            entity.ToTable("status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Thread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__thread__3213E83FB619BBC1");

            entity.ToTable("thread");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.ThreadThemeId).HasColumnName("thread_theme_id");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .HasColumnName("title");

            entity.HasOne(d => d.ThreadTheme).WithMany(p => p.Threads)
                .HasForeignKey(d => d.ThreadThemeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__thread__thread_t__3864608B");
        });

        modelBuilder.Entity<ThreadTheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__thread_t__3213E83FEB65A131");

            entity.ToTable("thread_theme");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F5A0922D9");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(128)
                .HasColumnName("description");
            entity.Property(e => e.Karma)
                .HasDefaultValue(0)
                .HasColumnName("karma");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");
            entity.Property(e => e.RoleId)
                .HasDefaultValue(1)
                .HasColumnName("role_id");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("status_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__role_id__3E1D39E1");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__status_id__3C34F16F");
        });

        modelBuilder.Entity<UserThread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_thr__3213E83FB25F0AD7");

            entity.ToTable("user_thread");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Thread).WithMany(p => p.UserThreads)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_thre__threa__46B27FE2");

            entity.HasOne(d => d.User).WithMany(p => p.UserThreads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_thre__user___45BE5BA9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
