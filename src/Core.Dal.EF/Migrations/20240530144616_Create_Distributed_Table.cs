using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Dal.EF.Migrations
{
    /// <inheritdoc />
    public partial class Create_Distributed_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // shard `tenants` table by `id`
            migrationBuilder.Sql(@"SELECT create_distributed_table('tenants', 'id');");
            
            // shard `users` table by `tenant_id`
            migrationBuilder.Sql(@"SELECT create_distributed_table('users', 'tenant_id', colocate_with =>'tenants');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SELECT citus_schema_undistribute('tenants');");
            migrationBuilder.Sql(@"SELECT citus_schema_undistribute('users');");
        }
    }
}
