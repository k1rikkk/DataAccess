using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Collections
    {
        [TestMethod]
        public void Crew_IList()
        {
            IList<Worker> crew = new Crew
            {
                new Worker
                {
                    FirstName = "Worker A",
                    WorkingPosition = WorkingPosition.President
                },
                new Worker
                {
                    FirstName = "Worker B",
                    WorkingPosition = WorkingPosition.GeneralManager
                }
            };
            crew.Add(new Worker
            {
                FirstName = "Worker C",
                WorkingPosition = WorkingPosition.Director
            });
            Assert.AreEqual(true, crew.Contains(new Worker { FirstName = "Worker C" }));
            crew.RemoveAt(1);
            Assert.AreEqual(false, crew.Contains(new Worker { FirstName = "Worker B" }));
            crew.Insert(0, new Worker
            {
                FirstName = "Worker D",
                WorkingPosition = WorkingPosition.AssistantManager
            });
            Assert.AreEqual(3, crew.Count);
            Assert.AreEqual(WorkingPosition.AssistantManager, crew[0].WorkingPosition);
        }

        [TestMethod]
        public void Crew_SortByWorkPos()
        {
            Worker f = new Worker
            {
                FirstName = "Worker A",
                WorkingPosition = WorkingPosition.President
            };
            Worker h = new Worker
            {
                FirstName = "Worker B",
                WorkingPosition = WorkingPosition.GeneralManager
            };
            Worker c = new Worker
            {
                FirstName = "Worker C",
                WorkingPosition = WorkingPosition.Director
            };
            Crew crew = new Crew { f, h, c };
            crew.SortByWorkingPosition();
            Worker[] workers = new Worker[] { c, f, h };
            for (int i = 0; i < crew.Count; i++)
                Assert.AreEqual(workers[i], crew[i]);
        }
    }
}
