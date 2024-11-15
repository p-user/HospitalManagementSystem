
namespace Doctors.Contracts.Doctors.Features.GetDoctorIdByEmail
{
    public record GetDoctorIdByEmailQuery(string Email) : IRequest<GetDoctorIdByEmailQueryResponse>;
    public record GetDoctorIdByEmailQueryResponse(string DoctorId);
}
