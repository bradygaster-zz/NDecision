using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NDecision.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void does_ndecision_engine_allow_adults_to_drink()
        {
            Handle<Person>
                .If(x => x.Age >= 21)
                    .Do(p =>
                    {
                        Console.WriteLine(string.Format("{0} is old enough to drink", p.Name));
                        Assert.Pass();
                    })
                .WithTargets(new Person
                {
                    Name = "Old Goat",
                    Age = 38
                })
                .Go();
        }

        [Test]
        [ExpectedException(typeof(UnderLegalDrinkingAgeException))]
        [Explicit("When run alongside multiple tests the exception expectations causes this one to barf")]
        public void does_ndecision_engine_NOT_allow_kids_to_drink()
        {
            Handle<Person>
                .If(x => x.Age < 21)
                    .Do(p =>
                    {
                        Console.WriteLine(string.Format("{0} is NOT old enough to drink", p.Name));
                        throw new UnderLegalDrinkingAgeException();
                    })
                .WithTargets(new Person
                {
                    Name = "Spring Chicken",
                    Age = 18
                })
                .Go();
        }

        [Test]
        public void does_ndecision_engine_support_multiple_handlers()
        {
            var young = new Person { Name = "Spring Chicken", Age = 18, Approved = false };
            var goat = new Person { Name = "Old Goat", Age = 38, Approved = false };

            Handle<Person>
                .If(x => x.Age < 21)
                .Do
                (
                    p => { Console.WriteLine(string.Format("{0} is NOT old enough to drink", p.Name)); },
                    p => { Console.WriteLine("Setting Approved to false"); },
                    p => { p.Approved = false; }
                )
                .ElseIf(x => x.Age >= 21)
                .Do
                (
                    p => { Console.WriteLine(string.Format("{0} IS old enough to drink", p.Name)); },
                    p => { Console.WriteLine("Setting Approved to true"); },
                    p => { p.Approved = true; }
                )
                .WithTargets(goat, young)
                .Go();

            Assert.That(!young.Approved);
            Assert.That(goat.Approved);
        }

        public class UnderLegalDrinkingAgeException : ApplicationException
        {
        }

        public class Person
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public bool Approved { get; set; }
        }
    }
}
