
namespace Doctors.Contracts.Doctors.Features.GetDoctorById
{
    public record GetDoctorByIdQuery(Guid Id) : IRequest<GetDoctorByIdQueryResponse>;
    public record GetDoctorByIdQueryResponse(DoctorDto Entity);

}
