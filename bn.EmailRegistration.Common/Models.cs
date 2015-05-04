using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace bn.EmailRegistration.Common
{
    public class EmailRegistrationRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("currentEmailAddress")]
        public string CurrentEmailAddress { get; set; }
        [JsonProperty("newEmailAddress")]
        public string NewEmailAddress { get; set; }
        [JsonProperty("regDateTime")]
        public DateTime DateRegistered { get; set; }
        [JsonProperty("isOwner")]
        public bool IsOwner { get; set; }
    }
}
