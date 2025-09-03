using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addschemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartMentSubjects_DepartMents_DepartMentId",
                table: "DepartMentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartMentSubjects_Subjects_SubjectId",
                table: "DepartMentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_DepartMents_DepartMentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartMents",
                table: "DepartMents");

            migrationBuilder.EnsureSchema(
                name: "Dep");

            migrationBuilder.EnsureSchema(
                name: "Std");

            migrationBuilder.EnsureSchema(
                name: "Sub");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject",
                newSchema: "Sub");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student",
                newSchema: "Std");

            migrationBuilder.RenameTable(
                name: "DepartMents",
                newName: "DepartMent",
                newSchema: "Dep");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DepartMentId",
                schema: "Std",
                table: "Student",
                newName: "IX_Student_DepartMentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                schema: "Sub",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                schema: "Std",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartMent",
                schema: "Dep",
                table: "DepartMent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartMentSubjects_DepartMent_DepartMentId",
                table: "DepartMentSubjects",
                column: "DepartMentId",
                principalSchema: "Dep",
                principalTable: "DepartMent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartMentSubjects_Subject_SubjectId",
                table: "DepartMentSubjects",
                column: "SubjectId",
                principalSchema: "Sub",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_DepartMent_DepartMentId",
                schema: "Std",
                table: "Student",
                column: "DepartMentId",
                principalSchema: "Dep",
                principalTable: "DepartMent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Student_StudentId",
                table: "StudentSubjects",
                column: "StudentId",
                principalSchema: "Std",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalSchema: "Sub",
                principalTable: "Subject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartMentSubjects_DepartMent_DepartMentId",
                table: "DepartMentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartMentSubjects_Subject_SubjectId",
                table: "DepartMentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_DepartMent_DepartMentId",
                schema: "Std",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Student_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                schema: "Sub",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                schema: "Std",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartMent",
                schema: "Dep",
                table: "DepartMent");

            migrationBuilder.RenameTable(
                name: "Subject",
                schema: "Sub",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "Student",
                schema: "Std",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "DepartMent",
                schema: "Dep",
                newName: "DepartMents");

            migrationBuilder.RenameIndex(
                name: "IX_Student_DepartMentId",
                table: "Students",
                newName: "IX_Students_DepartMentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartMents",
                table: "DepartMents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartMentSubjects_DepartMents_DepartMentId",
                table: "DepartMentSubjects",
                column: "DepartMentId",
                principalTable: "DepartMents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartMentSubjects_Subjects_SubjectId",
                table: "DepartMentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_DepartMents_DepartMentId",
                table: "Students",
                column: "DepartMentId",
                principalTable: "DepartMents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentId",
                table: "StudentSubjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
