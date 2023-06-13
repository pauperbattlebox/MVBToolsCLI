﻿using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models
{
    public class EditionModel
    {
        public int Id { get; set; }

        public int CardsphereId { get; set; }

        public string CardsphereName { get; set; }

        public string MtgJsonCode { get; set; }
    }
}
