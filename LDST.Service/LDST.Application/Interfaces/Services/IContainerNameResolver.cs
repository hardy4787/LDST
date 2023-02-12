namespace LDST.Application.Interfaces.Persistance;

public interface IContainerNameResolver
{
    public string GetContainerName(string contentType, string containerPrefix);
}