using APIServer.Models;

namespace APIServer.Repository.Interfaces;

public interface IMasterDb : IDisposable
{
    public VersionDAO _version { get; }
    public Task<bool> Load();
}
