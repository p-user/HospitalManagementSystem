using MediatR;

public record GetPatientIdByEmailQuery(string email) : IRequest<string>;