using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsumingData
{
    [Serializable]
    public class Book : IEquatable<Book>
    {
        public string Title { get; set; }
        public string Info { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }

        public bool Equals(Book other)
        {
            return Title == other.Title && Info == other.Info && Author == other.Author
                && PublishDate == other.PublishDate && Publisher == other.Publisher && Genre == other.Genre;
        }

        public override bool Equals(object obj)
        {
            if (obj is Book)
                return Equals(obj as Book);
            else
                return base.Equals(obj);
        }

        public void SaveToXml(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            serializer.Serialize(stream, this);
        }

        public void SaveToJson(Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            using (StreamWriter writer = new StreamWriter(stream))
                serializer.Serialize(writer, this);
        }

        public static Book LoadFromXml(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            return (Book)serializer.Deserialize(stream);
        }

        public static Book LoadFromJson(Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader reader = new StreamReader(stream))
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                    return serializer.Deserialize<Book>(jsonReader);
        }
    }
}
