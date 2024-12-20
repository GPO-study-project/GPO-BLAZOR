﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GPO_BLAZOR.Client.Class.Date
{
    /// <summary>
    /// Модель строки таблицы
    /// </summary>
    public record StatmenTableLineModel: IStatmenTableLineModel
    {
        public string Type { get; init; }
        public string id { get; init; }
        public DateTime Time { get; init; }
        [JsonProperty("state")]
        public string State { get; init; }
        public PracticType PracticType { get; init; }

        public int Number { get; set; }

    }
}
