using MediatR;

namespace Producer.Application.Commands
{
    public sealed record ReadFileCommand(string FilePath) : IRequest;
}
