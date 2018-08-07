using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoNotes.Migrations.Prototype
{
    public partial class ispinnednullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPinned",
                table: "ToDo",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPinned",
                table: "ToDo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
