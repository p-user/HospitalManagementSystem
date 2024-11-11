
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Authentication.EventHandlers.EventConsumers
{
    public class PatientAddedToApplicationUsersIntegrationEventConsumer(ISender sender, ILogger<PatientAddedToApplicationUsersIntegrationEventConsumer> logger) : IConsumer<PatientAddedToApplicationUsersIntegrationEvent>
    {
        public async  Task Consume(ConsumeContext<PatientAddedToApplicationUsersIntegrationEvent> context)
        {
            var patientUserEmail = context.Message.Email;
            var patientUserRole = context.Message.Role;

            var applicationUserDto = new ApplicationUserDto { Role = patientUserRole, Email = patientUserEmail };

            var result = await sender.Send(new RegisterUserCommand(applicationUserDto));

            //save log
            if (result.Succeded)
            {
                logger.LogInformation($" ApplicationUser with email {patientUserEmail} was registered successfully!");
            }

            logger.LogError($" ApplicationUser with email {patientUserEmail} was Not registered successfully! Please check further for the error");
        }
    }
}
