﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Models
{
    public partial class ChatContext : DbContext
    {
        public ChatContext()
        {
        }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HiddenMessage> HiddenMessages { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SessionUserMap> SessionsUsersMap { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HiddenMessage>(HiddenMessageConfigure);
            modelBuilder.Entity<Message>(MessageConfigure);
            modelBuilder.Entity<Session>(SessionConfigure);
            modelBuilder.Entity<SessionUserMap>(SessionUserMapConfigure);
            modelBuilder.Entity<User>(UserConfigure);

            OnModelCreatingPartial(modelBuilder);
        }

        public void HiddenMessageConfigure(EntityTypeBuilder<HiddenMessage> builder)
        {
            builder.ToTable("HIDDEN_MESSAGES");
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.MessageId).HasColumnName("MESSAGE_ID");
            builder.Property(e => e.UserId).HasColumnName("USER_ID");

            builder.HasOne(d => d.Message)
                .WithMany(p => p.HiddenMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__HIDDEN_ME__MESSA__46E78A0C");

            builder.HasOne(d => d.User)
                .WithMany(p => p.HiddenMessages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HIDDEN_ME__USER___45F365D3");
            
        }
        public void MessageConfigure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("MESSAGES");
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Date)
                .HasColumnName("DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Ishidden).HasColumnName("ISHIDDEN");
            builder.Property(e => e.SessionId).HasColumnName("SESSION_ID");
            builder.Property(e => e.Text).HasColumnName("TEXT");
            builder.Property(e => e.UserId).HasColumnName("USER_ID");

            builder.HasOne(d => d.Session)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MESSAGES__SESSIO__32E0915F");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MESSAGES__USER_I__31EC6D26");
        }
        public void SessionConfigure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("SESSIONS");
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Date)
                .HasColumnName("DATE")
                .HasColumnType("datetime");
        }

        public void SessionUserMapConfigure(EntityTypeBuilder<SessionUserMap> builder)
        {
            builder.ToTable("SESSIONS_USERS_MAP");
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Isadmin).HasColumnName("ISADMIN");
            builder.Property(e => e.SessionId).HasColumnName("SESSION_ID");
            builder.Property(e => e.UserId).HasColumnName("USER_ID");

            builder.HasOne(d => d.Session)
                .WithMany(p => p.SessionsUsersMap)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SESSIONS___SESSI__2D27B809");

            builder.HasOne(d => d.User)
                .WithMany(p => p.SessionsUsersMap)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SESSIONS___USER___2C3393D0");
        }

        public void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USERS");
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Firstname)
                .IsRequired()
                .HasColumnName("FIRSTNAME")
                .HasMaxLength(50);

            builder.Property(e => e.Isactive)
                .IsRequired()
                .HasColumnName("ISACTIVE")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Lastname)
                .HasColumnName("LASTNAME")
                .HasMaxLength(50);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
