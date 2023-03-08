using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulcast.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class AddTags : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Tags",
				table: "Posts",
				type: "TEXT",
				nullable: false,
				defaultValue: "");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Tags",
				table: "Posts");
		}
	}
}