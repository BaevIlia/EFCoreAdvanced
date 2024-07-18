using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreAdvanced.Migrations
{
    /// <inheritdoc />
    public partial class StudentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Students",
                newName: "FavouriteCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                newName: "IX_Students_FavouriteCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_FavouriteCourseId",
                table: "Students",
                column: "FavouriteCourseId",
                principalTable: "Courses",
                principalColumn: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_FavouriteCourseId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "FavouriteCourseId",
                table: "Students",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_FavouriteCourseId",
                table: "Students",
                newName: "IX_Students_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseID");
        }
    }
}
