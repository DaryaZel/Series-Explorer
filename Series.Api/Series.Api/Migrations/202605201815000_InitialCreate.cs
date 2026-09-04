namespace Series.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "tblSeries",
                columns => new
                {
                    KY_ID = columns.Int(nullable: false),
                    TX_DESCRIPTION = columns.String(nullable: false, maxLength: 200),
                    TX_SKU = columns.String(maxLength: 50),
                    TX_COUNTRY = columns.String(maxLength: 50),
                    TX_CUSTOMER = columns.String(maxLength: 50),
                    TX_CATEGORY = columns.String(maxLength: 50)
                })
                .PrimaryKey(table => table.KY_ID);

            CreateTable(
                "tblHierarchy",
                columns => new
                {
                    FK_SERIES = columns.Int(nullable: false),
                    FK_PARENT_SERIES = columns.Int(nullable: false)
                })
                .PrimaryKey(table => table.FK_SERIES)
                .ForeignKey("tblSeries", table => table.FK_SERIES)
                .Index(table => table.FK_PARENT_SERIES);

            CreateTable(
                "tblObservations",
                columns => new
                {
                    FK_SERIES = columns.Int(nullable: false),
                    TX_PERIOD = columns.String(nullable: false, maxLength: 50),
                    QY_SALES = columns.Double(),
                    QY_DEMAND = columns.Double(),
                    QY_SUPPLY = columns.Double()
                })
                .PrimaryKey(table => new { table.FK_SERIES, table.TX_PERIOD })
                .ForeignKey("tblSeries", table => table.FK_SERIES)
                .Index(table => new { table.FK_SERIES, table.TX_PERIOD });

            CreateTable(
                "tblLockedPeriods",
                columns => new
                {
                    FK_SERIES = columns.Int(nullable: false),
                    TX_PERIOD = columns.String(nullable: false, maxLength: 50),
                    TX_OPINION = columns.String(nullable: false, maxLength: 50)
                })
                .PrimaryKey(table => new { table.FK_SERIES, table.TX_PERIOD, table.TX_OPINION })
                .Index(table => new { table.FK_SERIES, table.TX_PERIOD, table.TX_OPINION });

            Sql(@"ALTER TABLE ""tblHierarchy""
ADD CONSTRAINT ""CK_tblHierarchy_NoSelfParent""
CHECK (""FK_PARENT_SERIES"" = 0 OR ""FK_PARENT_SERIES"" <> ""FK_SERIES"");");

            Sql(@"ALTER TABLE ""tblObservations""
ADD CONSTRAINT ""CK_tblObservations_TX_PERIOD_Format""
CHECK (""TX_PERIOD"" ~ '^[1-2][0-9]{3}-(0[1-9]|1[0-2])$');");

            Sql(@"ALTER TABLE ""tblLockedPeriods""
ADD CONSTRAINT ""FK_tblLockedPeriods_tblObservations""
FOREIGN KEY (""FK_SERIES"", ""TX_PERIOD"")
REFERENCES ""tblObservations"" (""FK_SERIES"", ""TX_PERIOD"");");

            Sql(@"ALTER TABLE ""tblLockedPeriods""
ADD CONSTRAINT ""CK_tblLockedPeriods_TX_OPINION""
CHECK (""TX_OPINION"" IN ('QY_SALES', 'QY_DEMAND', 'QY_SUPPLY'));");
        }

        public override void Down()
        {
            DropTable("tblLockedPeriods");
            DropTable("tblObservations");
            DropTable("tblHierarchy");
            DropTable("tblSeries");
        }
    }
}
