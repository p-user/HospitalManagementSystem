﻿using Doctors.Doctors.Features.GetDoctors;
using Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Doctors.Features.GetSpecializations
{
    public record GetSpecializationsResponse(List<SpecializationDto> SpecializationDtos);
    public class GetSpecializationsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/specializations", async (ISender sender) =>
            {
                var result = await sender.Send(new GetSpecializationsQuery());
                var response = result.Adapt<GetSpecializationsResponse>();
                return Results.Ok(response);
            })
                 .WithName("GetDoctorSpecializations")
                 .WithDescription("Get all specialization entities")
                 .Produces<GetSpecializationsResponse>(StatusCodes.Status200OK);
        }
    }
}