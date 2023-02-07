using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class tarjnullpaq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaCredito_Producto_PaqueteProductoId",
                table: "TarjetaCredito");

            migrationBuilder.AlterColumn<int>(
                name: "PaqueteProductoId",
                table: "TarjetaCredito",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaCredito_Producto_PaqueteProductoId",
                table: "TarjetaCredito",
                column: "PaqueteProductoId",
                principalTable: "Producto",
                principalColumn: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaCredito_Producto_PaqueteProductoId",
                table: "TarjetaCredito");

            migrationBuilder.AlterColumn<int>(
                name: "PaqueteProductoId",
                table: "TarjetaCredito",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaCredito_Producto_PaqueteProductoId",
                table: "TarjetaCredito",
                column: "PaqueteProductoId",
                principalTable: "Producto",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
