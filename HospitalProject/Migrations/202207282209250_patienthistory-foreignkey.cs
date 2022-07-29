namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patienthistoryforeignkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientHistories", "PatientID", c => c.Int(nullable: false));
            CreateIndex("dbo.PatientHistories", "PatientID");
            AddForeignKey("dbo.PatientHistories", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientHistories", "PatientID", "dbo.Patients");
            DropIndex("dbo.PatientHistories", new[] { "PatientID" });
            DropColumn("dbo.PatientHistories", "PatientID");
        }
    }
}
