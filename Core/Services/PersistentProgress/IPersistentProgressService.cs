using System.Threading.Tasks;

public interface IPersistentProgressService
{
    Task Load();
    Task Save(Result result);
}
