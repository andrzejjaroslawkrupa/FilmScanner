namespace FilmScanner.Contracts
{
	public interface IRepositoryWrapper
	{
		IFilmRepository Film { get; }

		IUserRepository User { get; }

		void Save();
	}
}