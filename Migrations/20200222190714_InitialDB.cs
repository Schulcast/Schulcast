using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Schulcast.Server.Migrations
{
	public partial class InitialDB : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Files",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
					   .Annotation("Sqlite:Autoincrement", true),
					Path = table.Column<string>(nullable: true)
				},
				constraints: table => table.PrimaryKey("PK_Files", x => x.Id));

			migrationBuilder.CreateTable(
				name: "Members",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
					   .Annotation("Sqlite:Autoincrement", true),
					Nickname = table.Column<string>(maxLength: 50, nullable: false),
					Role = table.Column<string>(maxLength: 50, nullable: true),
					Password = table.Column<string>(maxLength: 64, nullable: true),
					ImageId = table.Column<int>(nullable: false)
				},
				constraints: table => table.PrimaryKey("PK_Members", x => x.Id));

			migrationBuilder.CreateTable(
				name: "Slides",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
					   .Annotation("Sqlite:Autoincrement", true),
					Description = table.Column<string>(maxLength: 200, nullable: false),
					ImageId = table.Column<int>(nullable: false)
				},
				constraints: table => table.PrimaryKey("PK_Slides", x => x.Id));

			migrationBuilder.CreateTable(
				name: "Tasks",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
					   .Annotation("Sqlite:Autoincrement", true),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table => table.PrimaryKey("PK_Tasks", x => x.Id));

			migrationBuilder.CreateTable(
				name: "MemberData",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
					   .Annotation("Sqlite:Autoincrement", true),
					Title = table.Column<string>(maxLength: 100, nullable: false),
					Response = table.Column<string>(nullable: false),
					MemberId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MemberData", x => x.Id);
					table.ForeignKey(
						name: "FK_MemberData_Members_MemberId",
						column: x => x.MemberId,
						principalTable: "Members",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Posts",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
					   .Annotation("Sqlite:Autoincrement", true),
					Title = table.Column<string>(maxLength: 100, nullable: false),
					Content = table.Column<string>(nullable: false),
					Published = table.Column<DateTime>(nullable: false),
					LastUpdated = table.Column<DateTime>(nullable: false),
					MemberId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Posts", x => x.Id);
					table.ForeignKey(
						name: "FK_Posts_Members_MemberId",
						column: x => x.MemberId,
						principalTable: "Members",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "MemberTask",
				columns: table => new
				{
					TaskId = table.Column<int>(nullable: false),
					MemberId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MemberTask", x => new { x.MemberId, x.TaskId });
					table.ForeignKey(
						name: "FK_MemberTask_Members_MemberId",
						column: x => x.MemberId,
						principalTable: "Members",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_MemberTask_Tasks_TaskId",
						column: x => x.TaskId,
						principalTable: "Tasks",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_MemberData_MemberId",
				table: "MemberData",
				column: "MemberId");

			migrationBuilder.CreateIndex(
				name: "IX_MemberTask_TaskId",
				table: "MemberTask",
				column: "TaskId");

			migrationBuilder.CreateIndex(
				name: "IX_Posts_MemberId",
				table: "Posts",
				column: "MemberId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Files");

			migrationBuilder.DropTable(
				name: "MemberData");

			migrationBuilder.DropTable(
				name: "MemberTask");

			migrationBuilder.DropTable(
				name: "Posts");

			migrationBuilder.DropTable(
				name: "Slides");

			migrationBuilder.DropTable(
				name: "Tasks");

			migrationBuilder.DropTable(
				name: "Members");
		}
	}
}