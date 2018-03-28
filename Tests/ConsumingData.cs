using System;
using System.IO;
using System.Threading.Tasks;
using ConsumingData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ConsumingData
    {
        public Book Book => new Book
        {
            Author = "Joseph & Ben Albahari",
            Info = "Some info",
            Title = "C# 5.0 in a Nutshell",
            Publisher = "O’Reilly Media, Inc.",
            Genre = "Educational",
            PublishDate = DateTime.Parse("08.06.2012")
        };

        [TestMethod]
        public void Book_SaveToXml() 
            => Book.SaveToXml(new FileStream("D:/book.xml", FileMode.OpenOrCreate, FileAccess.Write));

        [TestMethod]
        public void Book_SaveToJson() 
            => Book.SaveToJson(new FileStream("D:/book.json", FileMode.OpenOrCreate, FileAccess.Write));

        [TestMethod]
        public void Book_LoadFromXml()
        {
            Book loaded = Book.LoadFromXml(new FileStream("D:/book.xml", FileMode.OpenOrCreate, FileAccess.Read));
            Assert.AreEqual(Book, loaded);
        }

        [TestMethod]
        public void Book_LoadFromJson()
        {
            Book loaded = Book.LoadFromJson(new FileStream("D:/book.json", FileMode.OpenOrCreate, FileAccess.Read));
            Assert.AreEqual(Book, loaded);
        }
    }
}
