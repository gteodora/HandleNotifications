using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsHandling
{
    public class Person
    {
        public const string SUBSCRIBED_TO_SMS = "sms";
        public const string SUBSCRIBED_TO_EMAIL = "email";
        public const string SUBSCRIBED_TO_PUSH = "push";

        public string[] SubsribedTo { get; set; }
        public Person(string[] SubsribedTo)
        {
            this.SubsribedTo = SubsribedTo;
        }
    }
}
