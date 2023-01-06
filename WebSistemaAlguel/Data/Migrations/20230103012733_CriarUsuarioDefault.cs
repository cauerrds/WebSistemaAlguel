using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSistemaAlguel.Data.Migrations
{
    public partial class CriarUsuarioDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "123e4567-e89b-12d3-a456-426655440000", "df01ad2f-0d4d-4f6e-a82e-fdc89f25df04", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "setor_funcionario" },
                values: new object[] { "1ee7cf0a0-1922-401b-a1ae-6ec9261484c0", 0, "6a0d2801-54a6-4529-8cb6-7058dd6d3179", "Funcionario", "funcionarioADM@mail.com", false, false, null, "FUNCIONARIOADM@MAIL.COM", "FUNCIONARIOADM@MAIL.COM", "AQAAAAEAACcQAAAAEIOTdKvntPKbtJTNe2b+CyChPahYgEDMyiCzQxU9WQ+TyW4+yxQNVg9b4TdNsvB11w==", null, false, "20827be6-c8fb-4981-afb1-f2e033deead7", false, "funcionarioADM@mail.com", "Usuario ADM" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "123e4567-e89b-12d3-a456-426655440000", "1ee7cf0a0-1922-401b-a1ae-6ec9261484c0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "123e4567-e89b-12d3-a456-426655440000", "1ee7cf0a0-1922-401b-a1ae-6ec9261484c0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "123e4567-e89b-12d3-a456-426655440000");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ee7cf0a0-1922-401b-a1ae-6ec9261484c0");
        }
    }
}
