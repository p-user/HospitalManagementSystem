
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Departments.Departments.Features.GetDepartment
{
    public record GetDepartmentByIdResponse(DepartmentDto DepartmentDto);
    public class GetDepartmentByIdEndpoint : ICarterModule
    {
       
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/departments/{id}", async ([FromRoute] Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetDepartmentByIdQuery(id));
                var response = result.Adapt<GetDepartmentByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetDepartment");
           
        }
    }
}
