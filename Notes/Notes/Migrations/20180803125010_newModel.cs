using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Migrations
{
    public partial class newModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Todo_TodoId",
                table: "Label");

            migrationBuilder.DropTable(
                name: "CheckListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todo",
                table: "Todo");

            migrationBuilder.RenameTable(
                name: "Todo",
                newName: "ToDo");

            migrationBuilder.RenameColumn(
                name: "TodoId",
                table: "Label",
                newName: "ToDoId");

            migrationBuilder.RenameIndex(
                name: "IX_Label_TodoId",
                table: "Label",
                newName: "IX_Label_ToDoId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPinned",
                table: "ToDo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDo",
                table: "ToDo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChecklistData = table.Column<string>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: false),
                    ToDoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklist_ToDo_ToDoId",
                        column: x => x.ToDoId,
                        principalTable: "ToDo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_ToDoId",
                table: "Checklist",
                column: "ToDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_ToDo_ToDoId",
                table: "Label",
                column: "ToDoId",
                principalTable: "ToDo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_ToDo_ToDoId",
                table: "Label");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDo",
                table: "ToDo");

            migrationBuilder.RenameTable(
                name: "ToDo",
                newName: "Todo");

            migrationBuilder.RenameColumn(
                name: "ToDoId",
                table: "Label",
                newName: "TodoId");

            migrationBuilder.RenameIndex(
                name: "IX_Label_ToDoId",
                table: "Label",
                newName: "IX_Label_TodoId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPinned",
                table: "Todo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todo",
                table: "Todo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CheckListItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckListData = table.Column<string>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: true),
                    TodoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckListItem_Todo_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItem_TodoId",
                table: "CheckListItem",
                column: "TodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Todo_TodoId",
                table: "Label",
                column: "TodoId",
                principalTable: "Todo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
