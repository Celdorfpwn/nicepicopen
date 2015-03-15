namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        Description = c.String(),
                        Downloadable = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.Photographers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        ProfilePicture = c.Binary(),
                        Role = c.String(),
                        FacebookCredentials_AccessToken = c.String(),
                        FacebookCredentials_AccessName = c.String(),
                        TwitterCredentials_AccessToken = c.String(),
                        TwitterCredentials_AccessName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommunityUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(),
                        PortofolioId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId)
                .ForeignKey("dbo.Portofolios", t => t.PortofolioId)
                .Index(t => t.AlbumId)
                .Index(t => t.PortofolioId);
            
            CreateTable(
                "dbo.Portofolios",
                c => new
                    {
                        PhotographerId = c.Int(nullable: false),
                        CoverId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotographerId)
                .ForeignKey("dbo.Pictures", t => t.CoverId)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .Index(t => t.CoverId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        AlbumName = c.String(),
                        PhotographerId = c.Int(nullable: false),
                        PhotographerName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(),
                        Downloadable = c.Boolean(nullable: false),
                        TextWatermarkId = c.Int(),
                        ImageWatermarkId = c.Int(),
                        CameraId = c.Int(),
                        DownloadWithWatermark = c.Boolean(nullable: false),
                        PortofolioId = c.Int(),
                        Image = c.Binary(),
                        Views = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotographerCameras", t => t.CameraId)
                .ForeignKey("dbo.PictureCategories", t => t.CategoryId)
                .ForeignKey("dbo.ImageWatermarks", t => t.ImageWatermarkId)
                .ForeignKey("dbo.TextWatermarks", t => t.TextWatermarkId)
                .ForeignKey("dbo.Portofolios", t => t.PortofolioId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.CameraId)
                .Index(t => t.CategoryId)
                .Index(t => t.ImageWatermarkId)
                .Index(t => t.TextWatermarkId)
                .Index(t => t.PortofolioId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.PhotographerCameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Camera = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PictureCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PictureComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PictureId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.UserId)
                .ForeignKey("dbo.Pictures", t => t.PictureId)
                .Index(t => t.UserId)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.ImageWatermarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        WatermarkName = c.String(nullable: false),
                        Image = c.Binary(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        HorizontalAlign = c.String(nullable: false),
                        VerticalAlign = c.String(nullable: false),
                        Opacity = c.Int(nullable: false),
                        Padding = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.TextWatermarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        WatermarkName = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        FontColor = c.String(nullable: false),
                        FontSize = c.Int(nullable: false),
                        FontStyle = c.String(nullable: false),
                        FontFamily = c.String(nullable: false),
                        HorizonalAlign = c.String(nullable: false),
                        VerticalAlign = c.String(nullable: false),
                        Opacity = c.Int(nullable: false),
                        Padding = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.PortofolioPictureComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        PortofolioId = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .ForeignKey("dbo.Pictures", t => t.PictureId)
                .ForeignKey("dbo.Portofolios", t => t.PortofolioId)
                .Index(t => t.PhotographerId)
                .Index(t => t.PictureId)
                .Index(t => t.PortofolioId);
            
            CreateTable(
                "dbo.PortofolioPictureNices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        PortofolioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .ForeignKey("dbo.Pictures", t => t.PictureId)
                .ForeignKey("dbo.Portofolios", t => t.PortofolioId)
                .Index(t => t.PhotographerId)
                .Index(t => t.PictureId)
                .Index(t => t.PortofolioId);
            
            CreateTable(
                "dbo.PortofolioPictureViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        PortofolioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .ForeignKey("dbo.Pictures", t => t.PictureId)
                .ForeignKey("dbo.Portofolios", t => t.PortofolioId)
                .Index(t => t.PhotographerId)
                .Index(t => t.PictureId)
                .Index(t => t.PortofolioId);
            
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        FriendRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.FriendRequestId)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .Index(t => t.FriendRequestId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.Blinds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlinderId = c.Int(nullable: false),
                        BlindedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.BlindedId)
                .ForeignKey("dbo.Photographers", t => t.BlinderId)
                .Index(t => t.BlindedId)
                .Index(t => t.BlinderId);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.SenderId)
                .ForeignKey("dbo.Photographers", t => t.ReceiverId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.ConversationMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ConversationId = c.Int(nullable: false),
                        Message = c.String(),
                        Seen = c.Boolean(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.SenderId)
                .ForeignKey("dbo.Conversations", t => t.ConversationId)
                .Index(t => t.SenderId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.ContestPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PictureId = c.Int(nullable: false),
                        PhotographerId = c.Int(nullable: false),
                        DailyContestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .ForeignKey("dbo.Pictures", t => t.PictureId)
                .ForeignKey("dbo.DailyContests", t => t.DailyContestId)
                .Index(t => t.PhotographerId)
                .Index(t => t.PictureId)
                .Index(t => t.DailyContestId);
            
            CreateTable(
                "dbo.DailyContests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        WinnerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestPictures", t => t.WinnerId)
                .Index(t => t.WinnerId);
            
            CreateTable(
                "dbo.DiscussionPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscussionId = c.Int(nullable: false),
                        Text = c.String(),
                        PhotographerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discussions", t => t.DiscussionId)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .Index(t => t.DiscussionId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Text = c.String(),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.DiscussionPostReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscussionPostId = c.Int(nullable: false),
                        Text = c.String(),
                        PhotographerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiscussionPosts", t => t.DiscussionPostId)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .Index(t => t.DiscussionPostId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.OnlineUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotographerId = c.Int(nullable: false),
                        AlbumId = c.Int(),
                        QuoteId = c.Int(),
                        ContestPictureId = c.Int(),
                        DailyContestWinnerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId)
                .ForeignKey("dbo.ContestPictures", t => t.ContestPictureId)
                .ForeignKey("dbo.DailyContests", t => t.DailyContestWinnerId)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId)
                .ForeignKey("dbo.Quotes", t => t.QuoteId)
                .Index(t => t.AlbumId)
                .Index(t => t.ContestPictureId)
                .Index(t => t.DailyContestWinnerId)
                .Index(t => t.PhotographerId)
                .Index(t => t.QuoteId);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        UserVisitorId = c.Int(nullable: false),
                        VisitedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.UserVisitorId)
                .ForeignKey("dbo.Photographers", t => t.VisitedId)
                .Index(t => t.UserVisitorId)
                .Index(t => t.VisitedId);
            
            CreateTable(
                "dbo.Nices",
                c => new
                    {
                        PictureId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PictureId, t.UserId })
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PictureId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NotNice",
                c => new
                    {
                        PictureId = c.Int(nullable: false),
                        PhotographerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PictureId, t.PhotographerId })
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .Index(t => t.PictureId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        PictureId = c.Int(nullable: false),
                        PhotographerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PictureId, t.PhotographerId })
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .Index(t => t.PictureId)
                .Index(t => t.PhotographerId);
            
            CreateTable(
                "dbo.CheckedUpdates",
                c => new
                    {
                        PhotographerId = c.Int(nullable: false),
                        UpdateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhotographerId, t.UpdateId })
                .ForeignKey("dbo.Photographers", t => t.PhotographerId, cascadeDelete: true)
                .ForeignKey("dbo.CommunityUpdates", t => t.UpdateId, cascadeDelete: true)
                .Index(t => t.PhotographerId)
                .Index(t => t.UpdateId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        p1_key = c.Int(nullable: false),
                        p2_key = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.p1_key, t.p2_key })
                .ForeignKey("dbo.Photographers", t => t.p1_key)
                .ForeignKey("dbo.Photographers", t => t.p2_key)
                .Index(t => t.p1_key)
                .Index(t => t.p2_key);
            
            CreateTable(
                "dbo.CpNices",
                c => new
                    {
                        CpId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CpId, t.UserId })
                .ForeignKey("dbo.ContestPictures", t => t.CpId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CpId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CpNotNices",
                c => new
                    {
                        CpId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CpId, t.UserId })
                .ForeignKey("dbo.ContestPictures", t => t.CpId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CpId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DiscussionParticipants",
                c => new
                    {
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiscussionId, t.UserId })
                .ForeignKey("dbo.Discussions", t => t.DiscussionId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.DiscussionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DiscussionViews",
                c => new
                    {
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiscussionId, t.UserId })
                .ForeignKey("dbo.Discussions", t => t.DiscussionId, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.DiscussionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Update_Pictures",
                c => new
                    {
                        UpdateId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UpdateId, t.PictureId })
                .ForeignKey("dbo.UserUpdates", t => t.UpdateId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .Index(t => t.UpdateId)
                .Index(t => t.PictureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitors", "VisitedId", "dbo.Photographers");
            DropForeignKey("dbo.Visitors", "UserVisitorId", "dbo.Photographers");
            DropForeignKey("dbo.UserUpdates", "QuoteId", "dbo.Quotes");
            DropForeignKey("dbo.Update_Pictures", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Update_Pictures", "UpdateId", "dbo.UserUpdates");
            DropForeignKey("dbo.UserUpdates", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.UserUpdates", "DailyContestWinnerId", "dbo.DailyContests");
            DropForeignKey("dbo.UserUpdates", "ContestPictureId", "dbo.ContestPictures");
            DropForeignKey("dbo.UserUpdates", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.DiscussionPostReplies", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.DiscussionPostReplies", "DiscussionPostId", "dbo.DiscussionPosts");
            DropForeignKey("dbo.DiscussionPosts", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.DiscussionPosts", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.DiscussionViews", "UserId", "dbo.Photographers");
            DropForeignKey("dbo.DiscussionViews", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.DiscussionParticipants", "UserId", "dbo.Photographers");
            DropForeignKey("dbo.DiscussionParticipants", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.Discussions", "CreatorId", "dbo.Photographers");
            DropForeignKey("dbo.DailyContests", "WinnerId", "dbo.ContestPictures");
            DropForeignKey("dbo.ContestPictures", "DailyContestId", "dbo.DailyContests");
            DropForeignKey("dbo.ContestPictures", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.ContestPictures", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.CpNotNices", "UserId", "dbo.Photographers");
            DropForeignKey("dbo.CpNotNices", "CpId", "dbo.ContestPictures");
            DropForeignKey("dbo.CpNices", "UserId", "dbo.Photographers");
            DropForeignKey("dbo.CpNices", "CpId", "dbo.ContestPictures");
            DropForeignKey("dbo.Pictures", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Conversations", "ReceiverId", "dbo.Photographers");
            DropForeignKey("dbo.TextWatermarks", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Quotes", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Conversations", "SenderId", "dbo.Photographers");
            DropForeignKey("dbo.ConversationMessages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.ConversationMessages", "SenderId", "dbo.Photographers");
            DropForeignKey("dbo.Blinds", "BlinderId", "dbo.Photographers");
            DropForeignKey("dbo.Blinds", "BlindedId", "dbo.Photographers");
            DropForeignKey("dbo.ImageWatermarks", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Friends", "p2_key", "dbo.Photographers");
            DropForeignKey("dbo.Friends", "p1_key", "dbo.Photographers");
            DropForeignKey("dbo.FriendRequests", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.FriendRequests", "FriendRequestId", "dbo.Photographers");
            DropForeignKey("dbo.CheckedUpdates", "UpdateId", "dbo.CommunityUpdates");
            DropForeignKey("dbo.CheckedUpdates", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.CommunityUpdates", "PortofolioId", "dbo.Portofolios");
            DropForeignKey("dbo.PortofolioPictureViews", "PortofolioId", "dbo.Portofolios");
            DropForeignKey("dbo.PortofolioPictureViews", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.PortofolioPictureViews", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.PortofolioPictureNices", "PortofolioId", "dbo.Portofolios");
            DropForeignKey("dbo.PortofolioPictureNices", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.PortofolioPictureNices", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Pictures", "PortofolioId", "dbo.Portofolios");
            DropForeignKey("dbo.PortofolioPictureComments", "PortofolioId", "dbo.Portofolios");
            DropForeignKey("dbo.PortofolioPictureComments", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.PortofolioPictureComments", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Portofolios", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Portofolios", "CoverId", "dbo.Pictures");
            DropForeignKey("dbo.Pictures", "TextWatermarkId", "dbo.TextWatermarks");
            DropForeignKey("dbo.Views", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.Views", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.NotNice", "PhotographerId", "dbo.Photographers");
            DropForeignKey("dbo.NotNice", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Nices", "UserId", "dbo.Photographers");
            DropForeignKey("dbo.Nices", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Pictures", "ImageWatermarkId", "dbo.ImageWatermarks");
            DropForeignKey("dbo.PictureComments", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.PictureComments", "UserId", "dbo.Photographers");
            DropForeignKey("dbo.Pictures", "CategoryId", "dbo.PictureCategories");
            DropForeignKey("dbo.Pictures", "CameraId", "dbo.PhotographerCameras");
            DropForeignKey("dbo.CommunityUpdates", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Albums", "PhotographerId", "dbo.Photographers");
            DropIndex("dbo.Visitors", new[] { "VisitedId" });
            DropIndex("dbo.Visitors", new[] { "UserVisitorId" });
            DropIndex("dbo.UserUpdates", new[] { "QuoteId" });
            DropIndex("dbo.Update_Pictures", new[] { "PictureId" });
            DropIndex("dbo.Update_Pictures", new[] { "UpdateId" });
            DropIndex("dbo.UserUpdates", new[] { "PhotographerId" });
            DropIndex("dbo.UserUpdates", new[] { "DailyContestWinnerId" });
            DropIndex("dbo.UserUpdates", new[] { "ContestPictureId" });
            DropIndex("dbo.UserUpdates", new[] { "AlbumId" });
            DropIndex("dbo.DiscussionPostReplies", new[] { "PhotographerId" });
            DropIndex("dbo.DiscussionPostReplies", new[] { "DiscussionPostId" });
            DropIndex("dbo.DiscussionPosts", new[] { "PhotographerId" });
            DropIndex("dbo.DiscussionPosts", new[] { "DiscussionId" });
            DropIndex("dbo.DiscussionViews", new[] { "UserId" });
            DropIndex("dbo.DiscussionViews", new[] { "DiscussionId" });
            DropIndex("dbo.DiscussionParticipants", new[] { "UserId" });
            DropIndex("dbo.DiscussionParticipants", new[] { "DiscussionId" });
            DropIndex("dbo.Discussions", new[] { "CreatorId" });
            DropIndex("dbo.DailyContests", new[] { "WinnerId" });
            DropIndex("dbo.ContestPictures", new[] { "DailyContestId" });
            DropIndex("dbo.ContestPictures", new[] { "PictureId" });
            DropIndex("dbo.ContestPictures", new[] { "PhotographerId" });
            DropIndex("dbo.CpNotNices", new[] { "UserId" });
            DropIndex("dbo.CpNotNices", new[] { "CpId" });
            DropIndex("dbo.CpNices", new[] { "UserId" });
            DropIndex("dbo.CpNices", new[] { "CpId" });
            DropIndex("dbo.Pictures", new[] { "AlbumId" });
            DropIndex("dbo.Conversations", new[] { "ReceiverId" });
            DropIndex("dbo.TextWatermarks", new[] { "PhotographerId" });
            DropIndex("dbo.Quotes", new[] { "PhotographerId" });
            DropIndex("dbo.Conversations", new[] { "SenderId" });
            DropIndex("dbo.ConversationMessages", new[] { "ConversationId" });
            DropIndex("dbo.ConversationMessages", new[] { "SenderId" });
            DropIndex("dbo.Blinds", new[] { "BlinderId" });
            DropIndex("dbo.Blinds", new[] { "BlindedId" });
            DropIndex("dbo.ImageWatermarks", new[] { "PhotographerId" });
            DropIndex("dbo.Friends", new[] { "p2_key" });
            DropIndex("dbo.Friends", new[] { "p1_key" });
            DropIndex("dbo.FriendRequests", new[] { "PhotographerId" });
            DropIndex("dbo.FriendRequests", new[] { "FriendRequestId" });
            DropIndex("dbo.CheckedUpdates", new[] { "UpdateId" });
            DropIndex("dbo.CheckedUpdates", new[] { "PhotographerId" });
            DropIndex("dbo.CommunityUpdates", new[] { "PortofolioId" });
            DropIndex("dbo.PortofolioPictureViews", new[] { "PortofolioId" });
            DropIndex("dbo.PortofolioPictureViews", new[] { "PictureId" });
            DropIndex("dbo.PortofolioPictureViews", new[] { "PhotographerId" });
            DropIndex("dbo.PortofolioPictureNices", new[] { "PortofolioId" });
            DropIndex("dbo.PortofolioPictureNices", new[] { "PictureId" });
            DropIndex("dbo.PortofolioPictureNices", new[] { "PhotographerId" });
            DropIndex("dbo.Pictures", new[] { "PortofolioId" });
            DropIndex("dbo.PortofolioPictureComments", new[] { "PortofolioId" });
            DropIndex("dbo.PortofolioPictureComments", new[] { "PictureId" });
            DropIndex("dbo.PortofolioPictureComments", new[] { "PhotographerId" });
            DropIndex("dbo.Portofolios", new[] { "PhotographerId" });
            DropIndex("dbo.Portofolios", new[] { "CoverId" });
            DropIndex("dbo.Pictures", new[] { "TextWatermarkId" });
            DropIndex("dbo.Views", new[] { "PhotographerId" });
            DropIndex("dbo.Views", new[] { "PictureId" });
            DropIndex("dbo.NotNice", new[] { "PhotographerId" });
            DropIndex("dbo.NotNice", new[] { "PictureId" });
            DropIndex("dbo.Nices", new[] { "UserId" });
            DropIndex("dbo.Nices", new[] { "PictureId" });
            DropIndex("dbo.Pictures", new[] { "ImageWatermarkId" });
            DropIndex("dbo.PictureComments", new[] { "PictureId" });
            DropIndex("dbo.PictureComments", new[] { "UserId" });
            DropIndex("dbo.Pictures", new[] { "CategoryId" });
            DropIndex("dbo.Pictures", new[] { "CameraId" });
            DropIndex("dbo.CommunityUpdates", new[] { "AlbumId" });
            DropIndex("dbo.Albums", new[] { "PhotographerId" });
            DropTable("dbo.Update_Pictures");
            DropTable("dbo.DiscussionViews");
            DropTable("dbo.DiscussionParticipants");
            DropTable("dbo.CpNotNices");
            DropTable("dbo.CpNices");
            DropTable("dbo.Friends");
            DropTable("dbo.CheckedUpdates");
            DropTable("dbo.Views");
            DropTable("dbo.NotNice");
            DropTable("dbo.Nices");
            DropTable("dbo.Visitors");
            DropTable("dbo.UserUpdates");
            DropTable("dbo.OnlineUsers");
            DropTable("dbo.DiscussionPostReplies");
            DropTable("dbo.Discussions");
            DropTable("dbo.DiscussionPosts");
            DropTable("dbo.DailyContests");
            DropTable("dbo.ContestPictures");
            DropTable("dbo.Quotes");
            DropTable("dbo.ConversationMessages");
            DropTable("dbo.Conversations");
            DropTable("dbo.Blinds");
            DropTable("dbo.FriendRequests");
            DropTable("dbo.PortofolioPictureViews");
            DropTable("dbo.PortofolioPictureNices");
            DropTable("dbo.PortofolioPictureComments");
            DropTable("dbo.TextWatermarks");
            DropTable("dbo.ImageWatermarks");
            DropTable("dbo.PictureComments");
            DropTable("dbo.PictureCategories");
            DropTable("dbo.PhotographerCameras");
            DropTable("dbo.Pictures");
            DropTable("dbo.Portofolios");
            DropTable("dbo.CommunityUpdates");
            DropTable("dbo.Photographers");
            DropTable("dbo.Albums");
        }
    }
}
