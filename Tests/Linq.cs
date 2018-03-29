using System;
using System.Linq;
using System.Collections.Generic;
using DataAccess.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Linq
    {
        public IEnumerable<Animal> Animals { get; }
        public IEnumerable<Species> Species { get; }
        public IEnumerable<Zoo> Zoo { get; set; }
        public IEnumerable<ZooAnimal> ZooAnimals { get; set; }

        public Linq()
        {
            ZooAnimals = new List<ZooAnimal>
            {
                new ZooAnimal
                {
                    Id = 1,
                    AnimalId = 1,
                    ZooId = 1
                },
                new ZooAnimal
                {
                    Id = 2,
                    AnimalId = 2,
                    ZooId = 2
                },
                new ZooAnimal
                {
                    Id = 3,
                    AnimalId = 3,
                    ZooId = 1
                },
                new ZooAnimal
                {
                    Id = 4,
                    AnimalId = 4,
                    ZooId = 2
                },
                new ZooAnimal
                {
                    Id = 5,
                    AnimalId = 5,
                    ZooId = 1
                },
                new ZooAnimal
                {
                    Id = 6,
                    AnimalId = 6,
                    ZooId = 2
                }
            };
            Zoo = new List<Zoo>()
            {
                new Zoo
                {
                    Title = "Zoo A",
                    Id = 1,
                    Address = "Address A"
                },
                new Zoo
                {
                    Title = "Zoo B",
                    Id = 2,
                    Address = "Address B"
                }
            };
            Species = new List<Species>
            {
                new Species
                {
                    Id = 1,
                    Title = "Dogs",
                    Info = "Dogs info"
                },
                new Species
                {
                    Id = 2,
                    Title = "Cats",
                    Info = "Cats info"
                },
                new Species
                {
                    Id = 3,
                    Title = "Raccoons",
                    Info = "Raccoons info"
                }
            };
            Animals = new List<Animal>
            {
                new Animal
                {
                    Id = 1,
                    Name = "Baron",
                    Info = "Baron info",
                    SpeciesId = 1
                },
                new Animal
                {
                    Id = 2,
                    Name = "Tuzik",
                    Info = "Tuzik info",
                    SpeciesId = 2
                },
                new Animal
                {
                    Id = 3,
                    Name = "Teddi",
                    Info = "Teddi info",
                    SpeciesId = 1
                },
                new Animal
                {
                    Id = 4,
                    Name = "Tom",
                    Info = "Tom info",
                    SpeciesId = 2
                },
                new Animal
                {
                    Id = 5,
                    Name = "Baton",
                    Info = "Baton info",
                    SpeciesId = 3
                },
                new Animal
                {
                    Id = 6,
                    Name = "Yarik",
                    Info = "Yarik info",
                    SpeciesId = 3
                }
            };
        }

        [TestMethod]
        public void Query_NameAndZoo()
        {
            List<Tuple<string, string>> result = ZooAnimals.Join(Animals, za => za.AnimalId, a => a.Id
                , (za, a) => new Tuple<ZooAnimal, Animal>(za, a)).Join(Zoo, t => t.Item1.ZooId, z => z.Id
                , (t, z) => new Tuple<string, string>(t.Item2.Name, z.Title)).ToList();
            Assert.AreEqual("Baron - Zoo A, "
                          + "Tuzik - Zoo B, "
                          + "Teddi - Zoo A, "
                          + "Tom - Zoo B, "
                          + "Baton - Zoo A, "
                          + "Yarik - Zoo B"
                , string.Join(", ", result.Select(t => t.Item1 + " - " + t.Item2)));
        }

        [TestMethod]
        public void Query_OrderByName2PageOf2()
        {
            List<Animal> result = Animals.OrderBy(a => a.Name).Skip(2).Take(2).ToList();
            Assert.AreEqual("Teddi, Tom", string.Join(", ", result.Select(a => a.Name)));
        }

        [TestMethod]
        public void Query_DifferentSpecies()
        {
            List<string> result = Animals.Join(Species, a => a.SpeciesId, s => s.Id
                , (a, s) => s.Title).Distinct().ToList();
            Assert.AreEqual("Dogs, Cats, Raccoons", string.Join(", ", result));
        }

        [TestMethod]
        public void Query_AnimalsFromZooA()
        {
            List<Animal> result = ZooAnimals.Join(Animals, za => za.AnimalId, a => a.Id
                   , (za, a) => new Tuple<ZooAnimal, Animal>(za, a)).Join(Zoo, t => t.Item1.ZooId, z => z.Id
                   , (t, z) => new Tuple<ZooAnimal, Animal, Zoo>(t.Item1, t.Item2, z))
                   .Where(t => t.Item3.Title == "Zoo A").Select(t => t.Item2).ToList();
            Assert.AreEqual("Baron, Teddi, Baton", string.Join(", ", result.Select(r => r.Name)));
        }
    }
}
