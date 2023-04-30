using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public static class Routes
    {
        private const string MvbBaseUrl = "https://www.multiversebridge.com/api/v1";
        private const string ScryfallBaseUrl = "https://api.scryfall.com";

        public static class MvbCards
        {
            private const string Base = $"{MvbBaseUrl}/cards/cs";

            public const string Get = $"{Base}";
        }

        public static class MvbEditions
        {
            private const string Base = $"{MvbBaseUrl}/sets";

            public const string Get = $"{Base}/cs";

            public const string GetByMtgJsonCode = $"{Base}/mtgjson";

            public const string GetByCsId = $"{Base}/cs";
        }

        public static class ScryfallCards
        {
            private const string Base = $"{ScryfallBaseUrl}/cards";

            public const string Get = $"{Base}";
        }
    }
}
