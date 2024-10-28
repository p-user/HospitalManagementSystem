
using FluentValidation;

namespace Doctors.Doctors.Features.CreateDoctor
{

    public record CreateDoctorCommand(DoctorDto DoctorDto  ) : IRequest<CreateDoctorResult>;


    public record CreateDoctorResult(Guid Id);


    public class CreateDoctorCommandHandler(DoctorsDbContext _dbContext, IValidator<DoctorDto> validator) : IRequestHandler<CreateDoctorCommand, CreateDoctorResult>
    {
        public async Task<CreateDoctorResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
           //TODO : inject repository pattern  in class INSTEAD OF dbcontext
            var validationResult = await validator.ValidateAsync(request.DoctorDto, cancellationToken);
            if (validationResult.Errors.Any())
            { 
                throw new ValidationException(validationResult.Errors.Select(s=>s.ErrorMessage).FirstOrDefault());

            }
            //create entity from dto 
            var doctorEntity = CreateNewDoctor(request.DoctorDto);

             _dbContext.Doctors.Add(doctorEntity);
            await _dbContext.SaveChangesAsync();

            //return obj
            return new CreateDoctorResult(doctorEntity.Id);

        }

        private Doctor CreateNewDoctor(DoctorDto DoctorDto) 
        {
            var doctor =  Doctor.Create(DoctorDto);
            return doctor;
                
        }
    }
}
