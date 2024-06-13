using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SilinmeTarih",
                table: "Users",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "SilenKullaniciId",
                table: "Users",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "OlusturulmaTarih",
                table: "Users",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "OlusturanKullaniciId",
                table: "Users",
                newName: "DeletedUserId");

            migrationBuilder.RenameColumn(
                name: "GuncelleyenKullaniciId",
                table: "Users",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "GuncellenmeTarih",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "Durum",
                table: "Users",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Anahtar",
                table: "Users",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "SilinmeTarih",
                table: "Brands",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "SilenKullaniciId",
                table: "Brands",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "OlusturulmaTarih",
                table: "Brands",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "OlusturanKullaniciId",
                table: "Brands",
                newName: "DeletedUserId");

            migrationBuilder.RenameColumn(
                name: "GuncelleyenKullaniciId",
                table: "Brands",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "GuncellenmeTarih",
                table: "Brands",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "Durum",
                table: "Brands",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Anahtar",
                table: "Brands",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "Users",
                newName: "SilenKullaniciId");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Users",
                newName: "SilinmeTarih");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "Durum");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Users",
                newName: "Anahtar");

            migrationBuilder.RenameColumn(
                name: "DeletedUserId",
                table: "Users",
                newName: "OlusturanKullaniciId");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Users",
                newName: "OlusturulmaTarih");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Users",
                newName: "GuncelleyenKullaniciId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "GuncellenmeTarih");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "Brands",
                newName: "SilenKullaniciId");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Brands",
                newName: "SilinmeTarih");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Brands",
                newName: "Durum");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Brands",
                newName: "Anahtar");

            migrationBuilder.RenameColumn(
                name: "DeletedUserId",
                table: "Brands",
                newName: "OlusturanKullaniciId");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Brands",
                newName: "OlusturulmaTarih");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Brands",
                newName: "GuncelleyenKullaniciId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Brands",
                newName: "GuncellenmeTarih");
        }
    }
}
