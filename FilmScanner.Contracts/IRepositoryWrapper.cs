using System.Threading.Tasks;

namespace FilmScanner.Contracts
{
	public interface IRepositoryWrapper
	{
		IFilmRecordRepository Film { get; }
		IUserRepository User { get; }
		Task SaveAsync();
	}
}