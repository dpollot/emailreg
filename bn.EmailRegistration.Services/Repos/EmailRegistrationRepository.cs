using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bn.EmailRegistration.Common;
using bn.EmailRegistration.Data;
using System.Data.EntityClient;
using bn.EmailRegistration.Services.Models;

namespace bn.EmailRegistration.Services.Repos
{
    public class EmailRegistrationRepository
    {
        /// <summary>
        /// Returns an EmailRegistrationRecord for the give email (if it exists),
        /// null otherwise
        /// </summary>
        /// <param name="email">the "new" email... the only one we'd be checking for conflicts against</param>
        /// <returns>The conflicting record if it exists, null otherwise</returns>
        public IEnumerable<EmailRegistrationRecord> GetByEmail(string email)
        {
            using (gappsEntities context = new gappsEntities())
            {
                List<EmailRegistrationRecord> list = new List<EmailRegistrationRecord>();
                context.EmailRegistrations.Where(er => er.NewEmailAddress == email).ToList()
                    .ForEach(c =>
                    {
                        list.Add(new EmailRegistrationRecord
                        {
                            Id = c.Id.ToString(),
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            CurrentEmailAddress = c.CurrentEmailAddress,
                            NewEmailAddress = c.NewEmailAddress,
                            DateRegistered = c.DateRegistered,
                            IsOwner = c.IsOwner
                        });
                    });
                return list;
            }
        }

        /// <summary>
        /// Inserts a new row, and returns the record with the guid of the newly inserted row
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public EmailRegistrationRecord InsertRecord(EmailRegistrationRecord record)
        {
            using (gappsEntities context = new gappsEntities())
            {
                Guid id = Guid.NewGuid();
                bool existing = context.EmailRegistrations.Any(c => c.NewEmailAddress == record.NewEmailAddress);

                context.EmailRegistrations.Add(new Data.EmailRegistration { 
                    Id = id,
                    FirstName = record.FirstName,
                    LastName = record.LastName,
                    CurrentEmailAddress = record.CurrentEmailAddress,
                    NewEmailAddress = record.NewEmailAddress.ToLower(),
                    DateRegistered = DateTime.Now,
                    IsOwner = !existing
                });

                context.SaveChanges();
                record.Id = id.ToString();

                return record;
            }
        }
    }
}