using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO public.""Users"" (""Id"",""Username"",""Password"",""Phone"",""Email"",""Status"",""Role"",""CreatedAt"",""UpdatedAt"") VALUES
	 ('87a452b9-2145-4f88-9222-60d0ac3e8346'::uuid,'Victor Santana','$2a$11$WA8fqmBv8yK1jRDdCTE./O7vsoj1h4Jcdi8/fZd98.g4WecSxxoIO','13988105267','victor.santana684@gmail.com','Active','Customer','2026-01-21 15:37:32.579465-03',NULL),
	 ('1d10f1b6-ff0a-4b74-aa1e-352e6d321478'::uuid,'Anne Santana','$2a$11$xmoGjpDJAfpvjmEcmaKseeCAVOIkFnNrWy972Ao6cnh3vAMCmNmX2','11954546362','Anne.santana@gmail.com','Inactive','Manager','2026-01-21 23:01:57.256479-03',NULL);

INSERT INTO public.""Branches"" (""Id"",""Name"",""IsActive"",""CreatedAt"",""UpdatedAt"",""DeletedAt"") VALUES
	 ('ee92352a-e096-44fe-8c6a-60d44e70ef54'::uuid,'Store Two',true,'2026-01-21 15:31:00.105107-03','2026-01-21 16:57:54.85712-03',NULL),
	 ('9de80184-04b7-4427-bf02-c45072ccb92f'::uuid,'Store One',true,'2026-01-23 07:07:58.160711-03',NULL,NULL),
	 ('f936f870-8e4d-4845-ac43-6cf45b08ef78'::uuid,'Store Three',true,'2026-01-23 07:14:06.554665-03',NULL,NULL);

INSERT INTO public.""Products"" (""Id"",""Name"",""Price"",""IsActive"",""CreatedAt"",""UpdatedAt"",""DeletedAt"") VALUES
	 ('50f8aea6-150f-47f8-a370-e962ade73d5b'::uuid,'Xbox',2000.00,true,'2026-01-22 18:16:06.500659-03',NULL,NULL),
	 ('b82956ba-cf82-41ae-9aff-cf29ed40f855'::uuid,'PS4',2000.00,true,'2026-01-22 18:16:28.746911-03',NULL,NULL),
	 ('868f12a5-b850-444d-9f9e-31cca501ec17'::uuid,'Atari',10000.00,true,'2026-01-22 18:16:41.970674-03',NULL,NULL),
	 ('979c38d7-6312-4cd3-9efe-7619669601ed'::uuid,'Nintendo Wii',4100.00,true,'2026-01-22 18:17:28.041039-03',NULL,NULL),
	 ('48df750a-4b30-4633-82ff-ee1c64e2b6ca'::uuid,'Super Nintendo',300.00,false,'2026-01-23 07:29:07.699844-03',NULL,NULL),
	 ('4ebfc9ed-3107-4a28-9e2b-9dd1aaa49c66'::uuid,'PS5 Slim',3999.00,false,'2026-01-21 20:57:42.962391-03','2026-01-23 07:38:53.991617-03',NULL);

INSERT INTO public.""Sales"" (""Id"",""UserId"",""BranchId"",""Total"",""IsActive"",""CreatedAt"",""UpdatedAt"",""DeletedAt"") VALUES
	 ('71b1ec34-6bb4-4092-aa93-3f2f21592f13'::uuid,'87a452b9-2145-4f88-9222-60d0ac3e8346'::uuid,'9de80184-04b7-4427-bf02-c45072ccb92f'::uuid,5120.00,true,'2026-01-23 07:41:27.688248-03',NULL,NULL),
	 ('18a8c048-2991-436c-b36b-e48a1408f916'::uuid,'87a452b9-2145-4f88-9222-60d0ac3e8346'::uuid,'9de80184-04b7-4427-bf02-c45072ccb92f'::uuid,2600.00,true,'2026-01-23 07:44:04.681563-03',NULL,NULL);

INSERT INTO public.""SaleItems"" (""Id"",""SalesId"",""ProductId"",""Quantity"",""Price"",""Discount"",""Total"",""IsActive"",""CreatedAt"",""UpdatedAt"",""DeletedAt"") VALUES
	 ('53b494d9-dd4c-482e-898f-fd5e68cd0727'::uuid,'71b1ec34-6bb4-4092-aa93-3f2f21592f13'::uuid,'48df750a-4b30-4633-82ff-ee1c64e2b6ca'::uuid,13,300.00,0.20,3120.00,true,'2026-01-23 07:41:27.874122-03',NULL,NULL),
	 ('b087cc3c-5d6b-4aae-8856-04db9066bb41'::uuid,'71b1ec34-6bb4-4092-aa93-3f2f21592f13'::uuid,'50f8aea6-150f-47f8-a370-e962ade73d5b'::uuid,1,2000.00,0.00,2000.00,true,'2026-01-23 07:41:27.901464-03',NULL,NULL),
	 ('39e10e2f-c85e-4aef-b2b2-7e4a138021a3'::uuid,'18a8c048-2991-436c-b36b-e48a1408f916'::uuid,'48df750a-4b30-4633-82ff-ee1c64e2b6ca'::uuid,2,300.00,0.00,600.00,true,'2026-01-23 07:44:04.700803-03',NULL,NULL),
	 ('2034590c-0d5c-4192-8a66-6ae97bbdf770'::uuid,'18a8c048-2991-436c-b36b-e48a1408f916'::uuid,'50f8aea6-150f-47f8-a370-e962ade73d5b'::uuid,1,2000.00,0.00,2000.00,true,'2026-01-23 07:44:04.714881-03',NULL,NULL);

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
