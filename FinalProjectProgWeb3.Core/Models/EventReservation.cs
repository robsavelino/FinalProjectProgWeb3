using System.Text.Json.Serialization;

namespace FinalProjectProgWeb3.Core.Models
{
    public class EventReservation
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        private long ? IdReservation { get; set; }
        public long IdEvent { get; set; }
        public string PersonName { get; set; }
        public long Quantity { get; set; }

    }
}
