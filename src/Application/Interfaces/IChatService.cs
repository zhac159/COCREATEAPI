using Application.DTOs.Chat;
using Domain.Enums;

namespace Application.Interfaces;

public class CreateChatParams
{
    public ChatType ChatType { get; set; }
    public int ChatTypeId { get; set; }
    public int? AdditionalUserId { get; set; }
    public string? GroupChatName { get; set; }
}

public interface IChatService
{
    public Task<ChatDTO> CreateAsync(
        CreateChatParams createChatParams
    );
}
