namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patienthistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientHistories",
                c => new
                    {
                        PatientHistoryID = c.Int(nullable: false, identity: true),
                        PatientDetails = c.String(),
                    })
                .PrimaryKey(t => t.PatientHistoryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientHistories");
        }
    }
}
