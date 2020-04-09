using Microsoft.EntityFrameworkCore.Migrations;

namespace BackOfficeMiniProjectCross.Migrations
{
    public partial class Seed_manual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            migrationBuilder.Sql($"LOAD DATA LOCAL INFILE '{path}\\Resourses\\Brands.tsv' INTO TABLE Brands; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          //  migrationBuilder.Sql(sql);
        }
    }
}
