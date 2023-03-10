using System.Threading.Tasks;

namespace FilmScanner.Contracts
{
	public interface IRepositoryWrapper
	{
		IFilmRecordRepository FilmRecord { get; }
		Task SaveAsync();
	}
}