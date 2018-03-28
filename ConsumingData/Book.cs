using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsumingData
{
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
            using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("book");
                WritePropToXml(writer, nameof(Title), Title);
                WritePropToXml(writer, nameof(Info), Info);
                WritePropToXml(writer, nameof(Author), Author);
                WritePropToXml(writer, nameof(Publisher), Publisher);
                WritePropToXml(writer, nameof(Genre), Genre);
                WritePropToXml(writer, nameof(PublishDate), PublishDate);
                writer.WriteFullEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        protected void WritePropToXml(XmlWriter writer, string name, object value)
        {
            writer.WriteStartElement(name);
            writer.WriteString(value.ToString());
            writer.WriteFullEndElement();
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
            Book book = new Book();
            using (XmlReader reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                reader.ReadStartElement("book");
                book.Title = ReadPropFromXml(reader, nameof(Title));
                book.Info = ReadPropFromXml(reader, nameof(Info));
                book.Author = ReadPropFromXml(reader, nameof(Author));
                book.Publisher = ReadPropFromXml(reader, nameof(Publisher));
                book.Genre = ReadPropFromXml(reader, nameof(Genre));
                book.PublishDate = DateTime.Parse(ReadPropFromXml(reader, nameof(PublishDate)));
                reader.ReadEndElement();
            }
            return book;
        }

        protected static string ReadPropFromXml(XmlReader reader, string name)
        {
            reader.ReadStartElement(name);
            string result = reader.ReadContentAsString();
            reader.ReadEndElement();
            return result;
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
