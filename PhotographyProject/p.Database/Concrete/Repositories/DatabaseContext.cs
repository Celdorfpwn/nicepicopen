using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Photographer> Photographers { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<FriendRequest> FriendRequests { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<PictureCategory> Categories { get; set; }

        public DbSet<PictureComment> Comments { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<ConversationMessage> ConversationsMessages { get; set; }

        public DbSet<TextWatermark> TextWatermarks { get; set; }

        public DbSet<ImageWatermark> ImageWatermarks { get; set; }

        public DbSet<PhotographerCamera> PhotographerCameras { get; set; }

        public DbSet<UserUpdate> UserUpdates { get; set; }

        public DbSet<Portofolio> Portofolio { get; set; }

        public DbSet<CommunityUpdate> CommunityUpdates { get; set; }

        public DbSet<PortofolioPictureNice> PortofolioNices { get; set; }

        public DbSet<PortofolioPictureView> PictureViews { get; set; }

        public DbSet<PortofolioPictureComment> PortofolioPictureViews { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Blind> Blinds { get; set; }

        public DbSet<DailyContest> DailyContests { get; set; }

        public DbSet<ContestPicture> ContestsPictures { get; set; }

        public DbSet<OnlineUser> OnlineUsers { get; set; }

        public DbSet<Discussion> Discussions { get; set; }

        public DbSet<DiscussionPost> DiscussionPosts { get; set; }

        public DbSet<DiscussionPostReply> DiscussionPostsReply { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<UserProfilePicture> UserProfilePictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigurePhotographer(modelBuilder);
            ConfigureAlbum(modelBuilder);
            ConfigureFriendRequest(modelBuilder);
            ConfigurePicture(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureComment(modelBuilder);
            ConfigureConversations(modelBuilder);
            ConfigureConversationMessage(modelBuilder);
            ConfigureWatermarks(modelBuilder);
            ConfigureCamera(modelBuilder);
            ConfigureUserUpdates(modelBuilder);
            ConfigurePortofolio(modelBuilder);
            ConfigureCommunityUpdates(modelBuilder);
            ConfigurePortofolioPictureNice(modelBuilder);
            ConfigurePortofolioPictureView(modelBuilder);
            ConfigurePortofolioPictureComment(modelBuilder);
            ConfigureQuote(modelBuilder);
            ConfigureDailyContest(modelBuilder);
            ConfigureContestPicture(modelBuilder);
            ConfigureDiscussions(modelBuilder);
            ConfigureDiscussionPosts(modelBuilder);
            ConfigureDiscussionsPostReplies(modelBuilder);
            ConfigureVisitors(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureUserPorfilePicture(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfilePicture>().HasKey(m => m.UserId);

            modelBuilder.Entity<UserProfilePicture>().HasRequired(m => m.User);
        }

        private void ConfigureVisitors(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>().HasKey(visitor => visitor.Id);

            modelBuilder.Entity<Visitor>().HasRequired(visitor => visitor.Visited)
                .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Visitor>().HasRequired(visitor => visitor.UserVisitor)
                .WithMany().WillCascadeOnDelete(false);
        }

        private void ConfigureDiscussionsPostReplies(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscussionPostReply>().HasKey(reply => reply.Id);
            modelBuilder.Entity<DiscussionPostReply>().HasRequired(reply => reply.Photographer)
                .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<DiscussionPostReply>().HasRequired(reply => reply.DiscussionPost)
                .WithMany().WillCascadeOnDelete(false);
        }

        private void ConfigureDiscussionPosts(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscussionPost>().HasKey(post => post.Id);
            modelBuilder.Entity<DiscussionPost>().HasRequired(post => post.Photographer)
                .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<DiscussionPost>().HasRequired(post => post.Discussion)
                .WithMany().WillCascadeOnDelete(false);
        }

        private void ConfigureDiscussions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discussion>().HasKey(discussion => discussion.Id);

            modelBuilder.Entity<Discussion>().HasRequired(discussion => discussion.Creator)
                .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Discussion>().HasMany(discussion => discussion.UserViews)
                .WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("DiscussionId");
                        table.MapRightKey("UserId");
                        table.ToTable("DiscussionViews");
                    });

            modelBuilder.Entity<Discussion>().HasMany(discussion => discussion.Participants)
                .WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("DiscussionId");
                        table.MapRightKey("UserId");
                        table.ToTable("DiscussionParticipants");
                    });
        }

        private void ConfigureContestPicture(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContestPicture>().HasKey(picture => picture.Id);

            modelBuilder.Entity<ContestPicture>().HasRequired(picture => picture.Picture)
                .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ContestPicture>().HasRequired(picture => picture.Photographer)
                .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ContestPicture>().HasMany(picture => picture.Nices)
                .WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("CpId");
                        table.MapRightKey("UserId");
                        table.ToTable("CpNices");
                    });

            modelBuilder.Entity<ContestPicture>().HasMany(picture => picture.NotSo)
                .WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("CpId");
                        table.MapRightKey("UserId");
                        table.ToTable("CpNotNices");
                    });
        }

        private void ConfigureDailyContest(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyContest>().HasKey(contest => contest.Id);
            modelBuilder.Entity<DailyContest>().HasMany(contest => contest.Pictures)
                .WithRequired().HasForeignKey(picture => picture.DailyContestId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<DailyContest>().HasOptional(contest => contest.Winner)
                .WithMany();
        }

        private void ConfigureQuote(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>().HasKey(quote => quote.Id);
        }

        private void ConfigurePortofolioPictureComment(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortofolioPictureComment>().HasKey(comment => comment.Id);

            modelBuilder.Entity<PortofolioPictureComment>()
                .HasRequired(comment => comment.Photographer)
                .WithMany().HasForeignKey(comment => comment.PhotographerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PortofolioPictureComment>()
                .HasRequired(comment => comment.Picture)
                .WithMany()
                .HasForeignKey(comment => comment.PictureId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PortofolioPictureComment>()
                .HasRequired(comment => comment.Portofolio)
                .WithMany(portofolio => portofolio.PictureComments)
                .HasForeignKey(comment => comment.PortofolioId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigurePortofolioPictureView(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortofolioPictureView>().HasKey(pictureView => pictureView.Id);

            modelBuilder.Entity<PortofolioPictureView>().HasRequired(pictureView => pictureView.Photographer)
                .WithMany().HasForeignKey(pictureView => pictureView.PhotographerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PortofolioPictureView>().HasRequired(pictureView => pictureView.Picture)
                .WithMany().HasForeignKey(pictureView => pictureView.PictureId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigurePortofolioPictureNice(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortofolioPictureNice>().HasKey(pictureNice => pictureNice.Id);

            modelBuilder.Entity<PortofolioPictureNice>().HasRequired(pictureNice => pictureNice.Picture)
                .WithMany().HasForeignKey(pictureNice => pictureNice.PictureId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PortofolioPictureNice>().HasRequired(pictureNice => pictureNice.Photographer)
                .WithMany().HasForeignKey(pictureNice => pictureNice.PhotographerId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureCommunityUpdates(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommunityUpdate>().HasKey(update => update.Id);

            modelBuilder.Entity<CommunityUpdate>().HasOptional(update => update.Album)
                .WithMany();

            modelBuilder.Entity<CommunityUpdate>().HasOptional(update => update.Portofolio)
                .WithMany();
        }

        private void ConfigurePortofolio(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portofolio>().HasKey(portofolio => portofolio.PhotographerId);

            modelBuilder.Entity<Portofolio>().HasRequired(portofolio => portofolio.Photographer)
                .WithOptional(photographer => photographer.Portofolio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Portofolio>().HasMany(portofolio => portofolio.Pictures)
                .WithOptional().HasForeignKey(picture => picture.PortofolioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Portofolio>().HasOptional(portofolio => portofolio.Cover)
                .WithMany();

            modelBuilder.Entity<Portofolio>().HasMany(portofolio => portofolio.PicturesNices)
                .WithRequired().HasForeignKey(pictureNice => pictureNice.PortofolioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Portofolio>().HasMany(portofolio => portofolio.PicturesViews)
                .WithRequired().HasForeignKey(pictureView => pictureView.PortofolioId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureUserUpdates(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserUpdate>().HasKey(update => update.Id);
            modelBuilder.Entity<UserUpdate>().HasRequired(update => update.Photographer)
                .WithMany().HasForeignKey(update => update.PhotographerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserUpdate>().HasOptional(update => update.Album).WithMany();
            modelBuilder.Entity<UserUpdate>().HasMany(update => update.Pictures).WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("UpdateId");
                        table.MapRightKey("PictureId");
                        table.ToTable("Update_Pictures");
                    });
            modelBuilder.Entity<UserUpdate>().HasOptional(update => update.Quote).WithMany();
            modelBuilder.Entity<UserUpdate>().HasOptional(update => update.ContestPicture).WithMany();
            modelBuilder.Entity<UserUpdate>().HasOptional(update => update.DailyContestWinner).WithMany();
        }

        private void ConfigureCamera(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotographerCamera>().HasKey(camera => camera.Id);
        }

        private void ConfigureWatermarks(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextWatermark>().HasKey(watermark => watermark.Id);
            modelBuilder.Entity<ImageWatermark>().HasKey(watermark => watermark.Id);
        }

        private void ConfigureConversationMessage(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConversationMessage>().HasKey(message => message.Id);

            modelBuilder.Entity<ConversationMessage>().HasRequired(message => message.Sender)
                .WithMany().HasForeignKey(message => message.SenderId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureConversations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>().HasKey(conversation => conversation.Id);

            modelBuilder.Entity<Conversation>().HasMany(conversation => conversation.Messages)
                .WithRequired().HasForeignKey(message => message.ConversationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>().HasRequired(conversation => conversation.Sender)
                .WithMany(user => user.MyConversations)
                .HasForeignKey(conversation => conversation.SenderId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureComment(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PictureComment>().HasKey(comment => comment.Id);
            modelBuilder.Entity<PictureComment>().HasRequired(comment => comment.Photographer)
                .WithMany().HasForeignKey(comment => comment.UserId).WillCascadeOnDelete(false);
        }

        private void ConfigureCategory(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PictureCategory>().HasKey(category => category.Id);
        }

        private void ConfigurePicture(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>().HasKey(picture => picture.Id);
            modelBuilder.Entity<Picture>().HasRequired(picture => picture.Category)
                .WithMany().HasForeignKey(picture => picture.CategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Picture>().HasMany(picture => picture.Comments)
                .WithRequired().HasForeignKey(pictureComment => pictureComment.PictureId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Picture>().HasMany(picture => picture.PhotographerNice)
                .WithMany(user => user.Favorites)
                .Map(table =>
                    {
                        table.MapLeftKey("PictureId");
                        table.MapRightKey("UserId");
                        table.ToTable("Nices");
                    });

            modelBuilder.Entity<Picture>().HasOptional(picture => picture.ImageWatermark)
                .WithMany(mark => mark.Pictures);

            modelBuilder.Entity<Picture>().HasOptional(picture => picture.TextWatermark)
                .WithMany(mark => mark.Pictures);

            modelBuilder.Entity<Picture>().HasMany(picture => picture.PhotographerViews)
                .WithMany()
                 .Map(table =>
                    {
                        table.MapLeftKey("PictureId");
                        table.MapRightKey("PhotographerId");
                        table.ToTable("Views");
                    });

            modelBuilder.Entity<Picture>().HasOptional(picture => picture.Camera)
                .WithMany(camera => camera.Pictures);

            modelBuilder.Entity<Picture>().HasMany(picture => picture.PhotographerNotNice)
                .WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("PictureId");
                        table.MapRightKey("PhotographerId");
                        table.ToTable("NotNice");
                    });
        }

        private void ConfigureFriendRequest(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendRequest>().HasRequired(friendRequest => friendRequest.PhotographerRequesting)
                .WithMany(photographer => photographer.FriendRequestsISent)
                .HasForeignKey(friendRequest => friendRequest.FriendRequestId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureAlbum(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasKey(album => album.Id);

            modelBuilder.Entity<Album>().HasMany(album => album.Pictures)
                .WithRequired(picture => picture.Album).HasForeignKey(picture => picture.AlbumId);
        }

        private void ConfigurePhotographer(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photographer>().HasKey(photographer => photographer.Id);

            modelBuilder.Entity<Photographer>().HasMany(phographer => phographer.FriendsIAdd)
                .WithMany(photographer => photographer.FriendsIAccept)
                .Map(table =>
                    {
                        table.MapLeftKey("p1_key");
                        table.MapRightKey("p2_key");
                        table.ToTable("Friends");
                    });
            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.FriendRequests)
                .WithRequired().HasForeignKey(friendRequest => friendRequest.PhotographerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.Albums)
                .WithRequired(album => album.Photographer).HasForeignKey(album => album.PhotographerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.TheirConversations)
                .WithRequired(conversation => conversation.Receiver)
                .HasForeignKey(conversation => conversation.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.TextWatermarks)
                .WithRequired().HasForeignKey(watermark => watermark.PhotographerId);
            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.ImageWatermarks)
                .WithRequired().HasForeignKey(watermark => watermark.PhotographerId);

            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.CheckedUpdates)
                .WithMany()
                .Map(table =>
                    {
                        table.MapLeftKey("PhotographerId");
                        table.MapRightKey("UpdateId");
                        table.ToTable("CheckedUpdates");
                    });

            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.Quotes)
                .WithRequired().HasForeignKey(quote => quote.PhotographerId);


            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.MyBlinds)
                .WithRequired(blind => blind.Blinder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Blind>().HasRequired(blind => blind.Blinded)
                .WithMany(photographer => photographer.TheirBlinds)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>().HasMany(photographer => photographer.ActiveConversations)
                .WithMany()
                .Map(table =>
                {
                    table.MapLeftKey("photographerId");
                    table.MapRightKey("conversationsId");
                    table.ToTable("activeConversations");
                });
        }

        public void NotChange()
        {
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        public void NotLazy()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public void NotProxy()
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
