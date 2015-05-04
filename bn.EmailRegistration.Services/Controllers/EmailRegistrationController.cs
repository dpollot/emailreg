using bn.EmailRegistration.Common;
using bn.EmailRegistration.Services.Models;
using bn.EmailRegistration.Services.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bn.EmailRegistration.Services.Controllers
{
    public class EmailRegistrationController : ApiController
    {
        /// <summary>
        /// Returns a record for the given email, null if no record exists
        /// </summary>
        /// <param name="email">the email address to check against (new email)</param>
        /// <returns>EmailRegistrationRecord</returns>
        /// <remarks>
        /// when you use this action, you need not include the latter part of the email address
        /// e.g., api/emailregistration/byemail/dp123
        /// </remarks>
        /// <example>
        /// api/emailregistration/byemail/dp123
        /// </example>
        [HttpGet]
        [ActionName("byemail")]
        [Route("api/{controller}/{action}/{email}")]
        public IEnumerable<EmailRegistrationRecord> GetByEmail(string email)
        {
            var records = new EmailRegistrationRepository().GetByEmail(string.Format("{0}@bn.co", email));
            return records;
        }
        
        // GET api/emailregistration?email = dp123@bn.co
        /// <summary>
        /// Returns a record for the given email, null if no record exists
        /// </summary>
        /// <param name="email">the email address to check against (new email)</param>
        /// <returns>EmailRegistrationRecord</returns>
        /// <remarks>
        /// when using this resource, you must supply email in query string
        /// e.g., api/emailregistration?email=dp123@bn.co
        /// </remarks>
        /// <example>
        /// api/emailregistration?email=dp123@bn.co
        /// </example>
        public IEnumerable<EmailRegistrationRecord> Get(string email)
        {
            var records = new EmailRegistrationRepository().GetByEmail(email);
            return records;
        }

        /// <summary>
        /// inserts a new record
        /// </summary>
        /// <param name="value">the record to insert</param>
        /// <returns>EmailRegistrationResponse</returns>
        public EmailRegistrationResponse Post([FromBody]EmailRegistrationRecord value)
        {
            try
            {
                EmailRegistrationResponse response = new EmailRegistrationResponse();
                var repo = new EmailRegistrationRepository();
                
                var inserted = repo.InsertRecord(value);
                var records = repo.GetByEmail(value.NewEmailAddress);

                response.InsertedRecord = inserted;
                if (records.Count() > 1)
                {
                    response.Status = "The email has been registered, however, there are conflicting records.  The inserted record is NOT the owner of the email address.  Please see the aptly named 'conflictingRecords' property for a list of conflicting records";
                    response.ConflictingRecords = records.Where(r => r.Id != inserted.Id);
                }
                else
                {
                    response.Status = "The email has been successfully registered.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new EmailRegistrationResponse { Status = string.Format("Error:{0}", ex.Message) };
            }
        }
    }
}
