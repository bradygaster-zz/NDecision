using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NDecision.Aspects;

namespace NDecision.MVCDemo.Models
{
    public class TaskViewModel
    {
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }

    public class TaskSpecs : IHasSpec<TaskViewModel>
    {
        public Spec<TaskViewModel> GetSpec()
        {
            return Spec<TaskViewModel>
                .When(t => string.IsNullOrEmpty(t.Description))
                .Then(t => t.IsComplete = true);
        }
    }
}