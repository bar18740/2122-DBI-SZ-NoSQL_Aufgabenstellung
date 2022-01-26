using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeAndWatch.Domain
{
    class User
    {
        public User(string userName, string gender, DateTime birthday, string password)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Gender = gender;
            Birthday = birthday;
            Password = password;
        }

        [BsonId]
        public Guid Id { get; private set; }
        public string UserName { get; set; }
        public string Gender { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
    }
}
