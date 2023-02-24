namespace DataAccessLibrary.Models.Interfaces
{
    public interface IEditionModel
    {
        int CsId { get; set; }
        string CsName { get; set; }        
        string MtgJsonCode { get; set; }
    }
}