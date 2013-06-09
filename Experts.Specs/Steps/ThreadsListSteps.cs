using System;
using System.Diagnostics;
using System.Threading;
using Experts.Specs.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Experts.Specs.Steps
{
    [Binding]
    public class ThreadsListSteps
    {
        [Then(@"Zobaczę wątki w tabeli:")]
        public void ThenISeeThreadsInTable(Table table)
        {
            var threads = ScenarioContext.Current.Get<Thread>();

            table.CompareToInstance<Thread>(threads);
        }
    }
}
