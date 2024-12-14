using HackathonBackend.Application.Chat.Commands.StartChat;
using HackathonBackend.Contracts.Chat;
using Mapster;

namespace HackathonBackend.API.Mapping;

public class ChatMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StartChatRequest, StartChatCommand>();
        // .Map(dest => dest.Message, src => src.Message);
    }
}