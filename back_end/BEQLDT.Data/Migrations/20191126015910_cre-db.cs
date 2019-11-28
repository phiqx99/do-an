using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BEQLDT.Data.Migrations
{
    public partial class credb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameCategory = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Councils",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameCouncil = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Councils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decisions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameDecision = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameGroup = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    StartDay = table.Column<DateTime>(nullable: true),
                    EndDay = table.Column<DateTime>(nullable: true),
                    Caption = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameRole = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameSchool = table.Column<string>(maxLength: 350, nullable: false),
                    Phone = table.Column<int>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    Address = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    FullName = table.Column<string>(maxLength: 150, nullable: true),
                    Gender = table.Column<bool>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    Degree = table.Column<string>(maxLength: 50, nullable: true),
                    SSOSub = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    CouncilId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Councils_CouncilId",
                        column: x => x.CouncilId,
                        principalTable: "Councils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameTopic = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    PeriodId = table.Column<int>(nullable: true),
                    SchoolId = table.Column<int>(nullable: true),
                    DecisionId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topics_Decisions_DecisionId",
                        column: x => x.DecisionId,
                        principalTable: "Decisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topics_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topics_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    NameFile = table.Column<string>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicCouncils",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<int>(nullable: true),
                    UpdateUser = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    CouncilId = table.Column<int>(nullable: true),
                    TopicId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicCouncils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicCouncils_Councils_CouncilId",
                        column: x => x.CouncilId,
                        principalTable: "Councils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicCouncils_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_TopicId",
                table: "Files",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_CouncilId",
                table: "GroupUsers",
                column: "CouncilId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_GroupId",
                table: "Permissions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicCouncils_CouncilId",
                table: "TopicCouncils",
                column: "CouncilId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicCouncils_TopicId",
                table: "TopicCouncils",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CategoryId",
                table: "Topics",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_DecisionId",
                table: "Topics",
                column: "DecisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_PeriodId",
                table: "Topics",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_SchoolId",
                table: "Topics",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_UserId",
                table: "Topics",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "TopicCouncils");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Councils");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropTable(
                name: "Decisions");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
