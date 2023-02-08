using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public static class Constants
    {
        public const string BaseSetsUrl = "https://www.cardsphere.com/sets";
        public const string BaseCardsUrl = "https://www.cardsphere.com/cards/";
        public static readonly List<string> JsonFields = new List<string>
        {
            "borderColor",
            "name",
            "number",
            "faceName",
            "finishes",
            "frameEffects",
            "identifiers",
            "isFullArt",
            "isPromo",
            "promoTypes",
            "setCode",
            "uuid",
        };
    }
}
