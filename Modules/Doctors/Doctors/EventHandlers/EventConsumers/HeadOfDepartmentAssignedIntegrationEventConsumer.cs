using Doctors.Doctors.Features.SetHeadOfDepartment;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Doctors.EventHandlers.EventConsumers
{
    public class HeadOfDepartmentAssignedIntegrationEventConsumer(ISender sender, ILogger<HeadOfDepartmentAssignedIntegrationEventConsumer> logger) : IConsumer<HeadOfDepartmentAssignedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<HeadOfDepartmentAssignedIntegrationEvent> context)
        {
            var doctorId = context.Message.DoctorId;
            var result = await sender.Send(new SetHeadOfDepartmentCommand(doctorId));

            //save log
            if (result.Succeded)
            {
                logger.LogInformation($" doctor with id {doctorId} was set to head of department successfully!");
            }

            logger.LogError($" doctor with id {doctorId} was NOT set to head of department!");

        }
    }
}
