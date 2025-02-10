using APIServer.Models;

namespace APIServer.Repository.Interfaces;

public interface IMasterDb
{
    public VersionDAO _version { get; }
    public Task<bool> Load();
}
