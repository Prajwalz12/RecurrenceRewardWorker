using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductMaster
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("lobEnumId")]
        public int LobEnumId { get; set; }
        [Required]
        [JsonProperty("productName")]
        public string ProductName { get; set; }
        [Required]
        [JsonProperty("productCode")]
        public string ProductCode { get; set; }
        [JsonProperty("createdDate")]
        public DateTime? CreatedDate { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}