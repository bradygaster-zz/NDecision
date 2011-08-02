using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDecision;
using System.Diagnostics;

namespace Decision.Tests
{
    public class BugsBunny
    {
        public string City { get; set; }

        public void Turn(string direction)
        {
        }
    }

    public class Program
    {
        public void NavigateBunny(BugsBunny arg)
        {
            Spec<BugsBunny>
                .When(bunny => bunny.City.Equals("Albequerque"))
                    .Then(bunny =>
                        {
                            bunny.Turn("Left");
                            Debug.WriteLine("I'm not making that mistake again!");
                        })
                .OrWhen(bunny => bunny.City.Equals("Phoenix"))
                    .Then(bunny => bunny.Turn("Right"))
                .Run(arg);
        }
    }
}
