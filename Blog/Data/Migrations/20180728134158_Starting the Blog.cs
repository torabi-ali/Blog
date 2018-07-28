using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class StartingtheBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsertUserId = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<int>(nullable: true),
                    DeleteUserId = table.Column<int>(nullable: true),
                    InsertDateTime = table.Column<DateTime>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: true),
                    DeleteDateTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    InsertUserId1 = table.Column<string>(nullable: true),
                    UpdateUserId1 = table.Column<string>(nullable: true),
                    DeleteUserId1 = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Url = table.Column<string>(maxLength: 256, nullable: false),
                    ImagePath = table.Column<string>(maxLength: 256, nullable: true),
                    Summary = table.Column<string>(maxLength: 1024, nullable: true),
                    Text = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 512, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 256, nullable: true),
                    FocusKeyword = table.Column<string>(maxLength: 32, nullable: true),
                    VisitCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    AlternativeName = table.Column<string>(maxLength: 64, nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_DeleteUserId1",
                        column: x => x.DeleteUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_InsertUserId1",
                        column: x => x.InsertUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_UpdateUserId1",
                        column: x => x.UpdateUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsertUserId = table.Column<int>(nullable: true),
                    UpdateUserId = table.Column<int>(nullable: true),
                    DeleteUserId = table.Column<int>(nullable: true),
                    InsertDateTime = table.Column<DateTime>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: true),
                    DeleteDateTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    InsertUserId1 = table.Column<string>(nullable: true),
                    UpdateUserId1 = table.Column<string>(nullable: true),
                    DeleteUserId1 = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Url = table.Column<string>(maxLength: 256, nullable: false),
                    ImagePath = table.Column<string>(maxLength: 256, nullable: true),
                    Summary = table.Column<string>(maxLength: 1024, nullable: true),
                    Text = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 512, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 256, nullable: true),
                    FocusKeyword = table.Column<string>(maxLength: 32, nullable: true),
                    VisitCount = table.Column<int>(nullable: false),
                    SourceName = table.Column<string>(maxLength: 32, nullable: true),
                    SourceUrl = table.Column<string>(maxLength: 256, nullable: true),
                    IsPin = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_DeleteUserId1",
                        column: x => x.DeleteUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_InsertUserId1",
                        column: x => x.InsertUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_UpdateUserId1",
                        column: x => x.UpdateUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(maxLength: 128, nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(maxLength: 512, nullable: true),
                    InsertDateTime = table.Column<DateTime>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    ParentCommentId = table.Column<int>(nullable: true),
                    PostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostCategory",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategory", x => new { x.PostId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PostCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategory_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeleteUserId1",
                table: "Category",
                column: "DeleteUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_InsertUserId1",
                table: "Category",
                column: "InsertUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdateUserId1",
                table: "Category",
                column: "UpdateUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_DeleteUserId1",
                table: "Post",
                column: "DeleteUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Post_InsertUserId1",
                table: "Post",
                column: "InsertUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UpdateUserId1",
                table: "Post",
                column: "UpdateUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategory_CategoryId",
                table: "PostCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PostCategory");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
