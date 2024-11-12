
using Departments.Contracts.Departments.Features.GetDepartments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Departments.Departments.Features.GetDepartments
{
    public class GetDepartmentsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/departments", async (ISender sender) =>
            {
                var result = await sender.Send(new GetDepartmentsQuery());
                return Results.Ok(result);

            }).WithDescription("Get All Departments of the hospital")
            .WithName("GetDepartments");
        }
    }
}
