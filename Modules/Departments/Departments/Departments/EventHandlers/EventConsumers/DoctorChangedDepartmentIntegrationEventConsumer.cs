using Departments.Departments.Features.AssignDoctorToDepartment;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Departments.Departments.EventHandlers.EventConsumers
{
    public class DoctorChangedDepartmentIntegrationEventConsumer(ISender sender, ILogger<DoctorChangedDepartmentIntegrationEventConsumer> logger) : IConsumer<DoctorChangedDepartmentIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<DoctorChangedDepartmentIntegrationEvent> context)
        {
            var departmentId = context.Message.DepartmentId;
            var doctorId = context.Message.DoctorId;
            var result = await sender.Send(new AssignDoctorToDepartmentCommand(departmentId, doctorId));
            if (result.Succeded) 
            {
                logger.LogInformation($"Department was changed for doctor id {doctorId}");

            }
            logger.LogError($"Department was NOT changed for doctor id {doctorId}");

        }
    }
}
