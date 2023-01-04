using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewsPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImagesRefInReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Reviews",
                type: "text",
                nullable: true);
        }
    }
}
