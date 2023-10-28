using devops_23_24_net_a02.Shared.DTOs;

namespace Server.Services;

public interface IEmailService
{
  Task<int> Create(EmailDto.CreateEmail model);
}
