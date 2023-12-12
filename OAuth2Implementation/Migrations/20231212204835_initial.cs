using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth2Implementation.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OAuth2.Clients",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "TEXT", nullable: false),
                    Secret = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2.ResourceScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Scope = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.ResourceScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2.Users",
                columns: table => new
                {
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    secret = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.Users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2.ClientScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientId = table.Column<string>(type: "TEXT", nullable: false),
                    ResourceScopeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.ClientScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAuth2.ClientScopes_OAuth2.Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "OAuth2.Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OAuth2.ClientScopes_OAuth2.ResourceScopes_ResourceScopeId",
                        column: x => x.ResourceScopeId,
                        principalTable: "OAuth2.ResourceScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2.AuthorizationCodes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    Activated = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.AuthorizationCodes", x => x.Code);
                    table.ForeignKey(
                        name: "FK_OAuth2.AuthorizationCodes_OAuth2.Users_user_id",
                        column: x => x.userid,
                        principalTable: "OAuth2.Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2.UserAllowedScopes",
                columns: table => new
                {
                    key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    ResourceScopeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.UserAllowedScopes", x => x.key);
                    table.ForeignKey(
                        name: "FK_OAuth2.UserAllowedScopes_OAuth2.ResourceScopes_ResourceScopeId",
                        column: x => x.ResourceScopeId,
                        principalTable: "OAuth2.ResourceScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OAuth2.UserAllowedScopes_OAuth2.Users_user_id",
                        column: x => x.userid,
                        principalTable: "OAuth2.Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2.AuthorizationCodeScope",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorizationCodeCode = table.Column<string>(type: "TEXT", nullable: false),
                    ResourceScopeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2.AuthorizationCodeScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAuth2.AuthorizationCodeScope_OAuth2.AuthorizationCodes_AuthorizationCodeCode",
                        column: x => x.AuthorizationCodeCode,
                        principalTable: "OAuth2.AuthorizationCodes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OAuth2.AuthorizationCodeScope_OAuth2.ResourceScopes_ResourceScopeId",
                        column: x => x.ResourceScopeId,
                        principalTable: "OAuth2.ResourceScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.AuthorizationCodes_user_id",
                table: "OAuth2.AuthorizationCodes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.AuthorizationCodeScope_AuthorizationCodeCode",
                table: "OAuth2.AuthorizationCodeScope",
                column: "AuthorizationCodeCode");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.AuthorizationCodeScope_ResourceScopeId",
                table: "OAuth2.AuthorizationCodeScope",
                column: "ResourceScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.ClientScopes_ClientId",
                table: "OAuth2.ClientScopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.ClientScopes_ResourceScopeId",
                table: "OAuth2.ClientScopes",
                column: "ResourceScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.UserAllowedScopes_ResourceScopeId",
                table: "OAuth2.UserAllowedScopes",
                column: "ResourceScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2.UserAllowedScopes_user_id",
                table: "OAuth2.UserAllowedScopes",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAuth2.AuthorizationCodeScope");

            migrationBuilder.DropTable(
                name: "OAuth2.ClientScopes");

            migrationBuilder.DropTable(
                name: "OAuth2.UserAllowedScopes");

            migrationBuilder.DropTable(
                name: "OAuth2.AuthorizationCodes");

            migrationBuilder.DropTable(
                name: "OAuth2.Clients");

            migrationBuilder.DropTable(
                name: "OAuth2.ResourceScopes");

            migrationBuilder.DropTable(
                name: "OAuth2.Users");
        }
    }
}
