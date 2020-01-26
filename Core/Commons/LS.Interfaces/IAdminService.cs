using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LS.Interfaces {
    public interface IAdminService {
        Task<IEnumerable<string>> GetTopicsAsync();
        Task DeleteTopicsAsync(IEnumerable<string> topic);
        Task AddTopicAsync(string topic);
    }
}
