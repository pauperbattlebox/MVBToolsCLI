﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ScryfallBulkDataModel : IModel
    {

        [JsonProperty(PropertyName = "download_uri")]
        public string BulkDataUrl { get; set; }
    }
}
