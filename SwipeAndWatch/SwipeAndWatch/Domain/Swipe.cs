using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeAndWatch.Domain
{
    class Swipe
    {
        public Swipe(Movie movie, User user, string type, DateTime when)
        {
            Id = Guid.NewGuid();
            Movie = movie;
            User = user;
            Type = type;
            When = when;
        }

        [BsonId]
        public Guid Id { get; private set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
        public string Type { get; set; }
        public DateTime When { get; set; }

        public override string ToString() => $"     {Id} mit Content {Movie.Id}";
    }
}
