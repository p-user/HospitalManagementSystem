using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Constants;

namespace Departments.Departments.Features.AssignHeadOfDepartment
{

    public record AssignHeadOfDepartmentsRequest(Guid DoctorId);
    public record AssignHeadOfDepartmentsResponse(bool Succeded);
    public class AssignHeadOfDepartmentsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/departments/{id}/assignHeadOfDepartment", async ([FromRoute] Guid id, [FromBody]AssignHeadOfDepartmentsRequest request, ISender sender) =>
            {
                var command = new AssignHeadOfDepartmentCommand(id, request.DoctorId);
                var result = await sender.Send(command);
                var response = result.Adapt<AssignHeadOfDepartmentsResponse>();
                return Results.Created($"/departments/{id}", response);
            }).WithDescription("Assign the chief doctor of department")
            .WithName("AssignHeadOfDepartment")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<AssignHeadOfDepartmentsResponse>(StatusCodes.Status200OK)
            .RequireAuthorization(Policies.AdminOnly);
        }
    }
}
