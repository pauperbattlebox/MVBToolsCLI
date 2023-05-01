using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public static class Routes
    {
        private const string MVBBASEURL = "https://www.multiversebridge.com/api/v1";
        private const string SCRYFALLBASEURL = "https://api.scryfall.com";

        public static class MvbCards
        {
            private const string BASE = $"{MVBBASEURL}/cards/cs";

            public const string GET = $"{BASE}";
        }

        public static class MvbEditions
        {
            private const string BASE = $"{MVBBASEURL}/sets";

            public const string GET = $"{BASE}/cs";

            public const string GETBYMTGJSONCODE = $"{BASE}/mtgjson";

            public const string GETBYCSID = $"{BASE}/cs";
        }

        public static class ScryfallCards
        {
            private const string BASE = $"{SCRYFALLBASEURL}/cards";

            public const string GET = $"{BASE}";
        }
    }
}
