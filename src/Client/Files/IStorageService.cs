using Microsoft.AspNetCore.Components.Forms;

namespace devops_23_24_net_a02.Client.Files;

public interface IStorageService
{
  Task UploadImageAsync(string sas, IBrowserFile file);
}
