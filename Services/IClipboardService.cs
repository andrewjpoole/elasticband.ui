using System.Threading.Tasks;

namespace elasticband.ui.Services
{
    public interface IClipboardService
    {
        Task<bool> SetClipboard(string content);
        Task<string> GetClipboard();
    }
}
