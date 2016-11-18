namespace DataAccess.Model.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Studies", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Studies", "ModifiedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "ModifiedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "StudyId", "dbo.Studies");
            DropForeignKey("dbo.Subjects", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Phases", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Phases", "ModifiedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Phases", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Files", "PhaseId", "dbo.Phases");
            DropForeignKey("dbo.Files", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "ModifiedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subjects", "StudyId", "dbo.Studies");
            DropForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Studies", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.StudyRecordingMetadataInfo", "Id", "dbo.StudyRequests");
            DropForeignKey("dbo.StudyRecordingInfo", "StudyRequestId", "dbo.StudyRequests");
            DropForeignKey("dbo.StudyRequests", "Id", "dbo.Studies");
            DropForeignKey("dbo.AspNetUsers", "SiteId", "dbo.Sites");
            DropIndex("dbo.AspNetUsers", new[] { "SiteId" });
            DropIndex("dbo.Studies", new[] { "SiteId" });
            DropIndex("dbo.Studies", new[] { "CreatedBy" });
            DropIndex("dbo.Studies", new[] { "ModifiedBy" });
            DropIndex("dbo.Questions", new[] { "StudyId" });
            DropIndex("dbo.Questions", new[] { "SubjectId" });
            DropIndex("dbo.Questions", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.Questions", new[] { "ModifiedByUser_Id" });
            DropIndex("dbo.Subjects", new[] { "StudyId" });
            DropIndex("dbo.Subjects", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.Subjects", new[] { "ModifiedByUser_Id" });
            DropIndex("dbo.Files", new[] { "PhaseId" });
            DropIndex("dbo.Files", new[] { "Subject_Id" });
            DropIndex("dbo.Phases", new[] { "SubjectId" });
            DropIndex("dbo.Phases", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.Phases", new[] { "ModifiedByUser_Id" });
            DropIndex("dbo.StudyRequests", new[] { "Id" });
            DropIndex("dbo.StudyRecordingMetadataInfo", new[] { "Id" });
            DropIndex("dbo.StudyRecordingInfo", new[] { "StudyRequestId" });
            DropColumn("dbo.AspNetUsers", "SiteId");
            DropColumn("dbo.AspNetUsers", "Country");
            DropTable("dbo.Studies");
            DropTable("dbo.Questions");
            DropTable("dbo.Subjects");
            DropTable("dbo.Files");
            DropTable("dbo.Phases");
            DropTable("dbo.Sites");
            DropTable("dbo.StudyRequests");
            DropTable("dbo.StudyRecordingMetadataInfo");
            DropTable("dbo.StudyRecordingInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudyRecordingInfo",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StudyRequestId = c.Guid(nullable: false),
                        Description = c.String(),
                        EnvironmentType = c.Int(),
                        RecordingMethod = c.Int(),
                        SpeakerConfiguration = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudyRecordingMetadataInfo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Format = c.Int(),
                        ContainsFileName = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudyRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        FileOrigin = c.Int(),
                        AnalysisType = c.Int(),
                        SubjectCount = c.Int(),
                        FileFormat = c.Int(),
                        Language = c.String(),
                        IsDataAnonymized = c.Boolean(),
                        AgreeWithTOS = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phases",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        SubjectId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedByUser_Id = c.Guid(),
                        ModifiedByUser_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OriginalFileName = c.String(),
                        MD5 = c.String(),
                        Extension = c.String(),
                        Length = c.Int(nullable: false),
                        PhaseId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Subject_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        StudyId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedByUser_Id = c.Guid(),
                        ModifiedByUser_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        Order = c.Int(nullable: false),
                        StudyId = c.Guid(nullable: false),
                        SubjectId = c.Guid(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedByUser_Id = c.Guid(),
                        ModifiedByUser_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studies",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        PhaseCount = c.Int(nullable: false),
                        PhaseRecordingCount = c.Int(nullable: false),
                        PhaseInterval = c.Int(nullable: false),
                        SubjectCount = c.Int(),
                        SiteId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "SiteId", c => c.Guid());
            CreateIndex("dbo.StudyRecordingInfo", "StudyRequestId");
            CreateIndex("dbo.StudyRecordingMetadataInfo", "Id");
            CreateIndex("dbo.StudyRequests", "Id");
            CreateIndex("dbo.Phases", "ModifiedByUser_Id");
            CreateIndex("dbo.Phases", "CreatedByUser_Id");
            CreateIndex("dbo.Phases", "SubjectId");
            CreateIndex("dbo.Files", "Subject_Id");
            CreateIndex("dbo.Files", "PhaseId");
            CreateIndex("dbo.Subjects", "ModifiedByUser_Id");
            CreateIndex("dbo.Subjects", "CreatedByUser_Id");
            CreateIndex("dbo.Subjects", "StudyId");
            CreateIndex("dbo.Questions", "ModifiedByUser_Id");
            CreateIndex("dbo.Questions", "CreatedByUser_Id");
            CreateIndex("dbo.Questions", "SubjectId");
            CreateIndex("dbo.Questions", "StudyId");
            CreateIndex("dbo.Studies", "ModifiedBy");
            CreateIndex("dbo.Studies", "CreatedBy");
            CreateIndex("dbo.Studies", "SiteId");
            CreateIndex("dbo.AspNetUsers", "SiteId");
            AddForeignKey("dbo.AspNetUsers", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.StudyRequests", "Id", "dbo.Studies", "Id");
            AddForeignKey("dbo.StudyRecordingInfo", "StudyRequestId", "dbo.StudyRequests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudyRecordingMetadataInfo", "Id", "dbo.StudyRequests", "Id");
            AddForeignKey("dbo.Studies", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects", "Id");
            AddForeignKey("dbo.Subjects", "StudyId", "dbo.Studies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "ModifiedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Files", "Subject_Id", "dbo.Subjects", "Id");
            AddForeignKey("dbo.Files", "PhaseId", "dbo.Phases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Phases", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Phases", "ModifiedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Phases", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Subjects", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "StudyId", "dbo.Studies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "ModifiedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "CreatedByUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Studies", "ModifiedBy", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Studies", "CreatedBy", "dbo.AspNetUsers", "Id");
        }
    }
}
