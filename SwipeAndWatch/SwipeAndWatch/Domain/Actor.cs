using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeAndWatch.Domain
{
    class Actor
    {
        public Actor(string firstName, string lastName, string gender, DateTime birthday)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Birthday = birthday;
        }

        [BsonId]
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Birthday { get; set; }
    }
}
