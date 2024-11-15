
using Departments.Contracts.Departments.Features.GetWorkingShiftQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Departments.Departments.Features.GetWorkingShift
{
    public class GetWorkingShiftEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/departments/{departmentId}/workingShifts/{id}", async ([FromRoute] Guid id, ISender sender) =>
            {
                var query = new GetWorkingShiftQuery(id);
                var response = await sender.Send(query);
                return Results.Ok(response);
            }).WithDescription("Get a specific working shift ")
            .WithName("GetWorkingShift");
                
        }
    }
}
