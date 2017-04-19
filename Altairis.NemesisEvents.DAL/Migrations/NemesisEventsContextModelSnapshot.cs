using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Altairis.NemesisEvents.DAL.Migrations {
    [DbContext(typeof(NemesisEventsContext))]
    partial class NemesisEventsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("StorageUrl")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCancelled");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<int>("EventId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdmissionFee");

                    b.Property<bool>("AllowRegistration");

                    b.Property<DateTime>("DateBegin");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateEnd");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("InfoUrl")
                        .HasMaxLength(100);

                    b.Property<bool>("InvitationSent");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OrganizerName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("OwnerId");

                    b.Property<string>("RegistrationUrl")
                        .HasMaxLength(100);

                    b.Property<bool>("UseRegistration");

                    b.Property<int>("VenueId");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("VenueId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.EventTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("TagId");

                    b.ToTable("EventTags");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(100);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastLogin");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("EmailFrequency");

                    b.Property<bool>("Enabled");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.UserArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAreas");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.UserTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TagId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaId");

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(18,10)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(18,10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Attachment", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Event", "Event")
                        .WithMany("Attachments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Attendee", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Altairis.NemesisEvents.DAL.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Event", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.User", "Owner")
                        .WithMany("OwnedEvents")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Altairis.NemesisEvents.DAL.Venue", "Venue")
                        .WithMany("Events")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.EventTag", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Event", "Event")
                        .WithMany("EventTags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Altairis.NemesisEvents.DAL.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.UserArea", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Altairis.NemesisEvents.DAL.User", "User")
                        .WithMany("WatchedAreas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.UserTag", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Altairis.NemesisEvents.DAL.User", "User")
                        .WithMany("WatchedTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Altairis.NemesisEvents.DAL.Venue", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Area", "Area")
                        .WithMany("Venues")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Altairis.NemesisEvents.DAL.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Altairis.NemesisEvents.DAL.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
