using MVBToolsLibrary.Repository;

namespace MVBToolsLibrary.Factories
{
    public class DatabaseRepositoryFactory
    {
        public DatabaseRepository EditionDatabaseRepository()
        {
            return new DatabaseRepository();
        }
    }
}
