using Demo.Domain.AggregatesModel.ProductAggregate;

#nullable disable

namespace Demo.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class CreateProductsTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:product_status", "draft,active,discontinued");

        migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                price_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                price_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                status = table.Column<ProductStatus>(type: "product_status", nullable: false),
                created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                last_modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "products");

        migrationBuilder.AlterDatabase()
            .OldAnnotation("Npgsql:Enum:product_status", "draft,active,discontinued");
    }
}
