using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Constants;

namespace Departments.Departments.Features.CreateDepartment
{

    public record CreateDepartmentRequest(DepartmentDto DepartmentDto);
    public record CreateDepartmentResponse(Guid Id);
    public class CreateDepartmentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/departments", async ([FromBody] CreateDepartmentRequest request, ISender sender) =>
            {
                var toSendEntity = new CreateDepartmentCommand(request.DepartmentDto);
                var result = await sender.Send(toSendEntity);
                var response = result.Adapt<CreateDepartmentResponse>();
                return Results.Ok(response);
            })
                .WithDescription("Create Deapartment of a hospital")
                .WithName("Create Department")
                .Produces<CreateDepartmentResponse>(StatusCodes.Status200OK)
                .Produces<CreateDepartmentResponse>(StatusCodes.Status201Created)
                .Produces<CreateDepartmentResponse>(StatusCodes.Status400BadRequest)
                .RequireAuthorization(Policies.AdminOnly);
            ;
        }
    }
}
