using Bogus;
using MongoDB.Driver;
using SwipeAndWatch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace SwipeAndWatch.Infastructure
{
    internal class SwipeAndWatchDatabase
    {
        private readonly MongoClient _client;
        public IMongoDatabase db { get; }

        public SwipeAndWatchDatabase(string host, string database)
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host)
            };

            _client = new MongoClient(settings);
            db = _client.GetDatabase(database);
        }

        public void SeedDatabase()
        {
            db.DropCollection(nameof(StreamingPlatform));
            db.DropCollection(nameof(Actor));
            db.DropCollection(nameof(Movie));
            db.DropCollection(nameof(User));
            db.DropCollection(nameof(Swipe));

            Randomizer.Seed = new Random(46546);

            //StreamingPlatform
            var sp = new Faker<StreamingPlatform>().CustomInstantiator(f =>
            {
                var s = new StreamingPlatform(
                    name: f.Company.CompanyName(),
                    webaddress: f.Internet.Url()
                    );
                return s;
            })
                .Generate(20)
                .GroupBy(s => s.Id)
                .Select(s => s.First())
                .ToList();

            //Actor
            var gender = new string[] { "Male", "Female" };
            var actor = new Faker<Actor>().CustomInstantiator(f =>
            {
                var _gender = f.PickRandom<Gender>();
                var a = new Actor(
                    firstName: f.Name.FirstName(_gender),
                    lastName: f.Name.LastName(),
                    gender: _gender.ToString(),
                    birthday: f.Date.Past(40, DateTime.Now.AddYears(-10)).Date
                    );
                return a;
            })
                .Generate(500)
                .GroupBy(a => a.Id)
                .Select(a => a.First())
                .ToList();

            //Movie
            var status = new string[] { "Finished", "Planning", "Shooting", "Stopped" };
            var languages = new string[] { "English", "German", "Spanish", "Russian", "French" };
            var genres = new string[] { "Action", "Romance", "Horror", "Thriller", "Mystery", "Drama" };
            var movie = new Faker<Movie>().CustomInstantiator(f =>
            {
                var m = new Movie(
                    title: f.Lorem.Sentence(),
                    length: f.Random.Number(100, 140),
                    language: f.Random.ListItem(languages),
                    actors: (List<Actor>)f.Random.ListItems(actor, f.Random.Number(2, 6)),
                    genre: f.Random.ListItem(genres),
                    streamingPlatforms: (List<StreamingPlatform>)f.Random.ListItems(sp, f.Random.Number(1, 3)),
                    rating: f.Random.Decimal(max: 5)
                    )
                {
                    Status = f.Random.ListItem(status),
                    Budget = f.Random.Number(1, 1000),
                    Director = f.Name.FullName()
                };
                return m;
            })
                .Generate(300)
                .GroupBy(a => a.Id)
                .Select(a => a.First())
                .ToList();

            //User
            var user = new Faker<User>().CustomInstantiator(f =>
            {
                var u = new User(
                    userName: f.Internet.UserName(),
                    gender: f.Random.ListItem(gender),
                    birthday: f.Date.Past(40, DateTime.Now.AddYears(-10)).Date,
                    password: f.Internet.Password()
                    );
                return u;
            })
                .Generate(20)
                .GroupBy(a => a.Id)
                .Select(a => a.First())
                .ToList();

            //Swipe
            var type = new string[] { "Like", "Dislike" };
            var swipe = new Faker<Swipe>().CustomInstantiator(f =>
            {
                var s = new Swipe(
                    movie: f.Random.ListItem(movie),
                    user: f.Random.ListItem(user),
                    type: f.Random.ListItem(type),
                    when: f.Date.Past()
                    );
                return s;
            })
                .Generate(100)
                .GroupBy(a => a.Id)
                .Select(a => a.First())
                .ToList();



            db.GetCollection<StreamingPlatform>(nameof(StreamingPlatform)).InsertMany(sp);
            db.GetCollection<Actor>(nameof(Actor)).InsertMany(actor);
            db.GetCollection<Movie>(nameof(Movie)).InsertMany(movie);
            db.GetCollection<User>(nameof(User)).InsertMany(user);
            db.GetCollection<Swipe>(nameof(Swipe)).InsertMany(swipe);
        }
    }
}
