// ReSharper disable InconsistentNaming

using System;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;
using NUnit.Framework;

namespace specs_for_SystemTime
{
    [TestFixture]
    public class in_general
    {
        [Test]
        public void reseting_the_system_time_reverts_back_to_the_curent_date_time()
        {
            var the_original_action = SystemTime.Now;
            SystemTime.Now = () => new DateTime(2009, 01, 01);
            SystemTime.ResetDelegate();

            SystemTime.Now.should_be_equal_to(the_original_action);
        }
    }
}