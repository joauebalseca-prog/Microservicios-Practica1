using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Api.Migrations
{
    /// <inheritdoc />
    public partial class InicialInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    IdBodega = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.IdBodega);
                });

            migrationBuilder.CreateTable(
                name: "Existencias",
                columns: table => new
                {
                    IdExistencias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBodega = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Existencias", x => x.IdExistencias);
                    table.ForeignKey(
                        name: "FK_Existencias_Bodegas_IdBodega",
                        column: x => x.IdBodega,
                        principalTable: "Bodegas",
                        principalColumn: "IdBodega",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosInventario",
                columns: table => new
                {
                    IdMovimientoInv = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBodega = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosInventario", x => x.IdMovimientoInv);
                    table.ForeignKey(
                        name: "FK_MovimientosInventario_Bodegas_IdBodega",
                        column: x => x.IdBodega,
                        principalTable: "Bodegas",
                        principalColumn: "IdBodega",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    IdStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBodega = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.IdStock);
                    table.ForeignKey(
                        name: "FK_Stocks_Bodegas_IdBodega",
                        column: x => x.IdBodega,
                        principalTable: "Bodegas",
                        principalColumn: "IdBodega",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_IdBodega",
                table: "Existencias",
                column: "IdBodega");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosInventario_IdBodega",
                table: "MovimientosInventario",
                column: "IdBodega");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_IdBodega",
                table: "Stocks",
                column: "IdBodega");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Existencias");

            migrationBuilder.DropTable(
                name: "MovimientosInventario");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Bodegas");
        }
    }
}
