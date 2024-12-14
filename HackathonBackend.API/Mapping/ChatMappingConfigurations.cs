using HackathonBackend.Application.Chat.Commands.SendMessage;
using Mapster;

namespace HackathonBackend.API.Mapping;

public class ChatMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<string, SendMessageCommand>()
            .Map(dest => dest.Message, src => src);
    }
}