
using Authentication.Authentication.Features.GenerateOTP;
using Authentication.Data.Constants;
using Doctors.Contracts.Doctors.Features.CreateDoctor;
using Doctors.Doctors.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using Shared.Services;
using System;

namespace Authentication.Authentication.Features.RegisterDoctor
{
    public record RegisterDoctorDto(DoctorDto DoctorDto) : IRequest<RegisterDoctorResponse>;
    public record RegisterDoctorResponse(bool Succeded);
    public class RegisterDoctorHandler(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager, AuthenticationDbContext authenticationDbContext, ISender sender) : IRequestHandler<RegisterDoctorDto, RegisterDoctorResponse>
    {
        public async Task<RegisterDoctorResponse> Handle(RegisterDoctorDto request, CancellationToken cancellationToken)
        {

            var doctor = new ApplicationUser 
            { 
                UserName = request.DoctorDto.Email,
                Email = request.DoctorDto.Email 
            };

            //check if user exists
            var existingUser = await _userManager.FindByEmailAsync(doctor.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }


            var result = await _userManager.CreateAsync(doctor);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(doctor, DefaultRoles.DoctorRole);
            }

            //create otp and send email
            var otpResponse = await sender.Send(new GenerateOTPCommand(doctor.Id));
            if (otpResponse.Succeded)
            {
                var createDoctorObj = await sender.Send(new CreateDoctorCommand(request.DoctorDto));

            }
            else
            {
                throw new  InternalServerException("Error in generating and sending otp to the client");
            }

       

            return new RegisterDoctorResponse(true);




        }

      
    }
}
