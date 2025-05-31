using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD_zajecia12.Migrations
{
    /// <inheritdoc />
    public partial class ExampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "s30635",
                table: "Client",
                columns: new[] { "IdClient", "Email", "FirstName", "LastName", "Pesel", "Telephone" },
                values: new object[,]
                {
                    { 1, "jan.kowalski@example.com", "Jan", "Kowalski", "12345678901", "123-456-789" },
                    { 2, "anna.nowak@example.com", "Anna", "Nowak", "10987654321", "987-654-321" }
                });

            migrationBuilder.InsertData(
                schema: "s30635",
                table: "Country",
                columns: new[] { "IdCountry", "Name" },
                values: new object[,]
                {
                    { 1, "Grecja" },
                    { 2, "Francja" }
                });

            migrationBuilder.InsertData(
                schema: "s30635",
                table: "Trip",
                columns: new[] { "IdTrip", "DateFrom", "DateTo", "Description", "MaxPeople", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wypoczynek na plaży", 20, "Wakacje w Grecji" },
                    { 2, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zwiedzanie miasta światła", 15, "Wycieczka do Paryża" }
                });

            migrationBuilder.InsertData(
                schema: "s30635",
                table: "Client_Trip",
                columns: new[] { "IdClient", "IdTrip", "PaymentDate", "RegisteredAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "s30635",
                table: "Country_Trip",
                columns: new[] { "IdCountry", "IdTrip" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Client_Trip",
                keyColumns: new[] { "IdClient", "IdTrip" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Client_Trip",
                keyColumns: new[] { "IdClient", "IdTrip" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Country_Trip",
                keyColumns: new[] { "IdCountry", "IdTrip" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Country_Trip",
                keyColumns: new[] { "IdCountry", "IdTrip" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Country",
                keyColumn: "IdCountry",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Country",
                keyColumn: "IdCountry",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Trip",
                keyColumn: "IdTrip",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "s30635",
                table: "Trip",
                keyColumn: "IdTrip",
                keyValue: 2);
        }
    }
}
