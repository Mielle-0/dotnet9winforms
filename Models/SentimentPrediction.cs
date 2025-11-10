using Newtonsoft.Json;

namespace it13Project.Models
{
    public class SentimentPrediction
    {
        [JsonProperty("review_id")]
        public int ReviewId { get; set; }

        [JsonProperty("predicted_sentiment")]
        public string PredictedSentiment { get; set; }

        [JsonProperty("confidence")]
        public decimal Confidence { get; set; }
    }

}