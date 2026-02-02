using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChordSense.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddResultJsonToAnalysisResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultJson",
                table: "AnalysisResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultJson",
                table: "AnalysisResults");
        }
    }
}
