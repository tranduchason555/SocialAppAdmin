using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MangXaHoi.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Safe> Saves { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9KFA5D6\\SQLEXPRESS;Database=mangxahoi;user id=sa;password=123456;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat__3213E83F4FDEDDC7");

            entity.ToTable("chat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(250)
                .HasColumnName("content");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Messagesid).HasColumnName("messagesid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Messages).WithMany(p => p.Chats)
                .HasForeignKey(d => d.Messagesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat__messagesid__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat__userid__5165187F");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F77E65818");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment1)
                .HasMaxLength(250)
                .HasColumnName("comment");
            entity.Property(e => e.Contentid).HasColumnName("contentid");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Content).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Contentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__conten__45F365D3");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__userid__44FF419A");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__content__3213E83FBE2642CC");

            entity.ToTable("content");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content1)
                .HasMaxLength(250)
                .HasColumnName("content");
            entity.Property(e => e.ContentPhoto)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("contentPhoto");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Contents)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__content__userid__4222D4EF");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__friendsh__3213E83F81096201");

            entity.ToTable("friendships");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Userid1).HasColumnName("userid1");
            entity.Property(e => e.Userid2).HasColumnName("userid2");

            entity.HasOne(d => d.Userid1Navigation).WithMany(p => p.FriendshipUserid1Navigations)
                .HasForeignKey(d => d.Userid1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__friendshi__useri__3B75D760");

            entity.HasOne(d => d.Userid2Navigation).WithMany(p => p.FriendshipUserid2Navigations)
                .HasForeignKey(d => d.Userid2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__friendshi__useri__3C69FB99");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__likes__3213E83F90BFD442");

            entity.ToTable("likes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentid).HasColumnName("commentid");
            entity.Property(e => e.Contentid).HasColumnName("contentid");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Comment).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Commentid)
                .HasConstraintName("FK__likes__commentid__4AB81AF0");

            entity.HasOne(d => d.Content).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Contentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__likes__contentid__49C3F6B7");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__likes__userid__48CFD27E");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83FA04E5E62");

            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContentPhoto)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("contentPhoto");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Useridgui).HasColumnName("useridgui");
            entity.Property(e => e.Useridnhan).HasColumnName("useridnhan");

            entity.HasOne(d => d.UseridguiNavigation).WithMany(p => p.MessageUseridguiNavigations)
                .HasForeignKey(d => d.Useridgui)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__userid__4D94879B");

            entity.HasOne(d => d.UseridnhanNavigation).WithMany(p => p.MessageUseridnhanNavigations)
                .HasForeignKey(d => d.Useridnhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__userid__4E88ABD4");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83F25E2A383");

            entity.ToTable("notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentid).HasColumnName("commentid");
            entity.Property(e => e.Contentid).HasColumnName("contentid");
            entity.Property(e => e.Friendshipid).HasColumnName("friendshipid");
            entity.Property(e => e.Likeid).HasColumnName("likeid");
            entity.Property(e => e.Messageid).HasColumnName("messageid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Storyid).HasColumnName("storyid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Comment).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Commentid)
                .HasConstraintName("FK__notificat__comme__5535A963");

            entity.HasOne(d => d.Content).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Contentid)
                .HasConstraintName("FK__notificat__conte__571DF1D5");

            entity.HasOne(d => d.Friendship).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Friendshipid)
                .HasConstraintName("FK__notificat__frien__5812160E");

            entity.HasOne(d => d.Like).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Likeid)
                .HasConstraintName("FK__notificat__likei__5629CD9C");

            entity.HasOne(d => d.Message).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Messageid)
                .HasConstraintName("FK__notificat__messa__59063A47");

            entity.HasOne(d => d.Story).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Storyid)
                .HasConstraintName("FK__notificat__story__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__useri__5AEE82B9");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report__3213E83FF0F9874B");

            entity.ToTable("report");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contentid).HasColumnName("contentid");
            entity.Property(e => e.Contentreport)
                .HasMaxLength(250)
                .HasColumnName("contentreport");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Content).WithMany(p => p.Reports)
                .HasForeignKey(d => d.Contentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__report__contenti__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__report__userid__619B8048");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83FBFA85AF8");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Safe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__saves__3213E83FBCAB3ECE");

            entity.ToTable("saves");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contentid).HasColumnName("contentid");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Content).WithMany(p => p.Saves)
                .HasForeignKey(d => d.Contentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__saves__contentid__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Saves)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__saves__userid__5DCAEF64");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__story__3213E83F2E88D590");

            entity.ToTable("story");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(250)
                .HasColumnName("content");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Photo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Stories)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story__userid__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FDEF2D7E3");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(250)
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK__users__roleid__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
