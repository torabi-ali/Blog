using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalBlog.Migrations
{
    public partial class fixing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Post",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Category",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateTime",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeleteUserId",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FocusKeyword",
                table: "Category",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Category",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDateTime",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Category",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "Category",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Category",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Category",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Category",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Category",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Category",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeleteUserId",
                table: "Category",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_InsertUserId",
                table: "Category",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdateUserId",
                table: "Category",
                column: "UpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_DeleteUserId",
                table: "Category",
                column: "DeleteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_InsertUserId",
                table: "Category",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_UpdateUserId",
                table: "Category",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_DeleteUserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_InsertUserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_UpdateUserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_DeleteUserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_InsertUserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_UpdateUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "FocusKeyword",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Category",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
