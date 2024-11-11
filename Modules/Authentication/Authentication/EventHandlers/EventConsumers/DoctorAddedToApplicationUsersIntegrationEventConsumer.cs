

using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Authentication.EventHandlers.EventConsumers
{
    public class DoctorAddedToApplicationUsersIntegrationEventConsumer
        (ISender sender, ILogger<DoctorAddedToApplicationUsersIntegrationEventConsumer> logger) : IConsumer<DoctorAddedToApplicationUsersIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<DoctorAddedToApplicationUsersIntegrationEvent> context)
        {
            var doctorUserEmail = context.Message.Email;
            var doctorUserRole = context.Message.Role;

            var applicationUserDto = new ApplicationUserDto { Role = doctorUserRole, Email= doctorUserEmail };

            var result = await sender.Send(new RegisterUserCommand(applicationUserDto));

            //save log
            if (result.Succeded)
            {
                logger.LogInformation($" ApplicationUser with email {doctorUserEmail} was registered successfully!");
            }

            logger.LogError($" ApplicationUser with email {doctorUserEmail} was Not registered successfully! Please check further for the error");
        }
    }
}
