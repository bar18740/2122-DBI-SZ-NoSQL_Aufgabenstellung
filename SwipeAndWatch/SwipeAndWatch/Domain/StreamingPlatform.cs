using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeAndWatch.Domain
{
    class StreamingPlatform
    {
        public StreamingPlatform(string name, string webaddress)
        {
            Id = Guid.NewGuid();
            Name = name;
            Webaddress = webaddress;
        }

        [BsonId]
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Webaddress { get; set; }
    }
}
