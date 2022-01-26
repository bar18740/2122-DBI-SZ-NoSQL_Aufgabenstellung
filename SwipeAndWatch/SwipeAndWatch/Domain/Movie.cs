using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeAndWatch.Domain
{
    class Movie
    {
        public Movie(string title, int length, string language, List<Actor> actors, string genre, List<StreamingPlatform> streamingPlatforms, decimal rating)
        {
            Id = Guid.NewGuid();
            Title = title;
            Length = length;
            Language = language;
            Actors = actors;
            Genre = genre;
            StreamingPlatforms = streamingPlatforms;
            Rating = rating;
        }

        [BsonId]
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public int Length { get; set; }
        public string? Status { get; set; }
        public string Language { get; set; }
        // in Mio
        public int? Budget { get; set; }
        public string? Director { get; set; }
        public List<Actor> Actors { get; set; }
        public string Genre { get; set; }
        public List<StreamingPlatform> StreamingPlatforms { get; set; }
        public decimal Rating { get; set; }

        public override string ToString() => $"{Id} - {Title}";
    }
}
