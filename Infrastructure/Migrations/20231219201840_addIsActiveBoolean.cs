using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addIsActiveBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "coordinators");

            migrationBuilder.EnsureSchema(
                name: "medicalWorkers");

            migrationBuilder.EnsureSchema(
                name: "medicalTeams");

            migrationBuilder.EnsureSchema(
                name: "medicalWorker");

            migrationBuilder.EnsureSchema(
                name: "schedules");

            migrationBuilder.EnsureSchema(
                name: "shifts");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "MedicalWorkerProfessionToPermission",
                schema: "medicalWorker",
                columns: table => new
                {
                    MedicalWorkerProfession = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalTeamType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalWorkerProfessionToPermission", x => new { x.MedicalWorkerProfession, x.MedicRole, x.MedicalTeamType });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coordinators",
                schema: "coordinators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordinators_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalWorkers",
                schema: "medicalWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    ApartamentNumber = table.Column<int>(type: "int", nullable: true),
                    Dateofbirth = table.Column<DateTime>(name: "Date of birth", type: "Date", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalWorkers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "users",
                columns: table => new
                {
                    RoleCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleCode });
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalTeams",
                schema: "medicalTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SizeOfTeam = table.Column<int>(type: "int", nullable: true),
                    MedicalTeamType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalTeams_Coordinators_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalSchema: "coordinators",
                        principalTable: "Coordinators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaysOff",
                schema: "medicalWorkers",
                columns: table => new
                {
                    Start = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    End = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MedicalWorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOff", x => new { x.MedicalWorkerId, x.Start, x.End });
                    table.ForeignKey(
                        name: "FK_DaysOff_MedicalWorkers_MedicalWorkerId",
                        column: x => x.MedicalWorkerId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalWorkerProfessions",
                schema: "medicalWorkers",
                columns: table => new
                {
                    MedicalWorkerProfession = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalWorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalWorkerProfessions", x => new { x.MedicalWorkerProfession, x.MedicalWorkerId });
                    table.ForeignKey(
                        name: "FK_MedicalWorkerProfessions_MedicalWorkers_MedicalWorkerId",
                        column: x => x.MedicalWorkerId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentContracts",
                schema: "medicalWorkers",
                columns: table => new
                {
                    ContractType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalWorkerProfession = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalWorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentContracts", x => new { x.MedicalWorkerId, x.ContractType, x.MedicRole, x.MedicalWorkerProfession });
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_MedicalTeams_MedicalTeamId",
                        column: x => x.MedicalTeamId,
                        principalSchema: "medicalTeams",
                        principalTable: "MedicalTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_MedicalWorkers_MedicalWorkerId",
                        column: x => x.MedicalWorkerId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_MedicalTeams_MedicalTeamId",
                        column: x => x.MedicalTeamId,
                        principalSchema: "medicalTeams",
                        principalTable: "MedicalTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                schema: "shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateRange_Start = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateRange_End = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    MedicalTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CrewMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_MedicalTeams_MedicalTeamId",
                        column: x => x.MedicalTeamId,
                        principalSchema: "medicalTeams",
                        principalTable: "MedicalTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_MedicalWorkers_CrewMemberId",
                        column: x => x.CrewMemberId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_MedicalWorkers_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_MedicalWorkers_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "schedules",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalWorkerShift",
                columns: table => new
                {
                    CrewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalWorkerShift", x => new { x.CrewId, x.ShiftsId });
                    table.ForeignKey(
                        name: "FK_MedicalWorkerShift_MedicalWorkers_CrewId",
                        column: x => x.CrewId,
                        principalSchema: "medicalWorkers",
                        principalTable: "MedicalWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalWorkerShift_Shifts_ShiftsId",
                        column: x => x.ShiftsId,
                        principalSchema: "shifts",
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "medicalWorker",
                table: "MedicalWorkerProfessionToPermission",
                columns: new[] { "MedicRole", "MedicalTeamType", "MedicalWorkerProfession" },
                values: new object[,]
                {
                    { "RegularMedic", "T", "BasicMedic" },
                    { "RegularMedic", "S", "Nurse" },
                    { "RegularMedic", "P", "Nurse" },
                    { "RegularMedic", "T", "Nurse" },
                    { "RegularMedic", "N", "Nurse" },
                    { "Driver", "T", "Doctor" },
                    { "Driver", "P", "Doctor" },
                    { "Manager", "T", "Nurse" },
                    { "Driver", "S", "Doctor" },
                    { "Manager", "P", "Doctor" },
                    { "Manager", "T", "Doctor" },
                    { "Manager", "S", "Doctor" },
                    { "Manager", "N", "Doctor" },
                    { "RegularMedic", "S", "Doctor" },
                    { "RegularMedic", "P", "Doctor" },
                    { "Driver", "N", "Doctor" },
                    { "RegularMedic", "T", "Doctor" },
                    { "Manager", "P", "Nurse" },
                    { "Driver", "S", "Nurse" },
                    { "Driver", "P", "BasicMedic" },
                    { "Driver", "T", "BasicMedic" },
                    { "Driver", "T", "Paramedic" },
                    { "Driver", "P", "Paramedic" },
                    { "Driver", "S", "Paramedic" },
                    { "Driver", "N", "Paramedic" },
                    { "Driver", "N", "Nurse" },
                    { "Manager", "P", "Paramedic" },
                    { "RegularMedic", "S", "Paramedic" },
                    { "RegularMedic", "P", "Paramedic" },
                    { "RegularMedic", "T", "Paramedic" },
                    { "RegularMedic", "N", "Paramedic" },
                    { "Driver", "T", "Nurse" },
                    { "Driver", "P", "Nurse" },
                    { "Manager", "T", "Paramedic" },
                    { "RegularMedic", "N", "Doctor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coordinators_UserId",
                schema: "coordinators",
                table: "Coordinators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_MedicalTeamId",
                schema: "medicalWorkers",
                table: "EmploymentContracts",
                column: "MedicalTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTeams_CoordinatorId",
                schema: "medicalTeams",
                table: "MedicalTeams",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalWorkerProfessions_MedicalWorkerId",
                schema: "medicalWorkers",
                table: "MedicalWorkerProfessions",
                column: "MedicalWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalWorkers_UserId",
                schema: "medicalWorkers",
                table: "MedicalWorkers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalWorkerShift_ShiftsId",
                table: "MedicalWorkerShift",
                column: "ShiftsId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_MedicalTeamId",
                schema: "schedules",
                table: "Schedules",
                column: "MedicalTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_CrewMemberId",
                schema: "shifts",
                table: "Shifts",
                column: "CrewMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DriverId",
                schema: "shifts",
                table: "Shifts",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ManagerId",
                schema: "shifts",
                table: "Shifts",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_MedicalTeamId",
                schema: "shifts",
                table: "Shifts",
                column: "MedicalTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ScheduleId",
                schema: "shifts",
                table: "Shifts",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaysOff",
                schema: "medicalWorkers");

            migrationBuilder.DropTable(
                name: "EmploymentContracts",
                schema: "medicalWorkers");

            migrationBuilder.DropTable(
                name: "MedicalWorkerProfessions",
                schema: "medicalWorkers");

            migrationBuilder.DropTable(
                name: "MedicalWorkerProfessionToPermission",
                schema: "medicalWorker");

            migrationBuilder.DropTable(
                name: "MedicalWorkerShift");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Shifts",
                schema: "shifts");

            migrationBuilder.DropTable(
                name: "MedicalWorkers",
                schema: "medicalWorkers");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "schedules");

            migrationBuilder.DropTable(
                name: "MedicalTeams",
                schema: "medicalTeams");

            migrationBuilder.DropTable(
                name: "Coordinators",
                schema: "coordinators");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "users");
        }
    }
}
