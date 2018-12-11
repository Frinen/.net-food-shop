using Microsoft.EntityFrameworkCore.Migrations;

namespace Food_Store.Migrations
{
    public partial class updateUserItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ammount",
                table: "UsersItems",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "UsersItems",
                newName: "Ammount");
        }
    }
}
