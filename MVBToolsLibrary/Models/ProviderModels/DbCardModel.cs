namespace MVBToolsLibrary.Models.ProviderModels
{
    public class DbCardModel
    {
        public int CsId { get; set; }

        public string Name { get; set; }

        public string MtgJsonId { get; set; }

        public string ScryfallId { get; set; }

        public string MtgJsonCode { get; set; }

        public decimal CsPrice { get; set; }

        public decimal ScryfallPrice { get; set; }
    }
}
