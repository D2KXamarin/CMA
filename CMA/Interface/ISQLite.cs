using SQLite;

namespace CMA
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

