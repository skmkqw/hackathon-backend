using HackathonBackend.Application.Chat.Commands.SendMessage;
using HackathonBackend.Application.Chat.Commands.StartChat;
using HackathonBackend.Contracts.Chat;
using HackathonBackend.Domain.UserAggregate.ValueObjects;
using Mapster;

namespace HackathonBackend.API.Mapping;

public class ChatMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<string, SendMessageCommand>()
            .Map(dest => dest.Message, src => src);
        
        config.NewConfig<(StartChatRequest request, Guid? userId), StartChatCommand>()
            .Map(dest => dest.Message, src => src.request.Message)
            .Map(dest => dest.UserId, src => UserId.Create(src.userId!.Value));
    }
}