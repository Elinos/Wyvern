namespace CoffeeCompany.Import
{
    public interface IDataImport
    {
        void ImportFromMongoDb();
        void ImportFromExcel();
        void ImportFromXml();
    }
}
