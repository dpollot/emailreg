using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using bn.EmailRegistration.Common;

namespace bn.EmailRegistration.Services.Models
{
    public class EmailRegistrationResponse
    {
        [JsonProperty("statusMessage")]
        public string Status { get; set; }
        [JsonProperty("conflictingRecords")]
        public IEnumerable<EmailRegistrationRecord> ConflictingRecords { get; set; }
        [JsonProperty("insertedRecord")]
        public EmailRegistrationRecord InsertedRecord { get; set; }
    }
}