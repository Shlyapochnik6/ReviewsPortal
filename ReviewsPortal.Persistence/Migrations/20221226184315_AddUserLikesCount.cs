using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewsPortal.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLikesCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "AspNetUsers");
        }
    }
}
