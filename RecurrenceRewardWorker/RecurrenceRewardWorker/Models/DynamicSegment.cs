using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Models.CustomerSegmentModel
{
    public class CustomerSegmentChangeRequest
    {
        [JsonProperty("lob")]
        public string Lob { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("segments")]
        public List<Common.CustomerModel.Segment> Segments { get; set; }
    }
    public class CustomerSegmentResponse
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("responseData")]
        public CustomerModel.ResponseData ResponseData { get; set; }
    }    
}
