namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.PaySubscriptionModels;

    public static class PaySubscriptionTestsData
    {
        public static PaySubscriptionCreateInputModel CreateModel => new PaySubscriptionCreateInputModel
        {
            SubscriptionId = Subscriptions[0].Id,
            ValidFrom = DateTime.Now,
            ValidTo = DateTime.Now.AddMonths(6),
        };

        public static PaySubscriptionEditModel UpdateModel => new PaySubscriptionEditModel()
        {
            SubscriptionId = Subscriptions[0].Id,
            ValidFrom = DateTime.Now,
            ValidTo = DateTime.Now.AddMonths(6),
        };

        public static string UserId => Users[0].Id;

        public static PaySubscriptionFilter FilterModel => new PaySubscriptionFilter()
        {
            ValidFrom = new DateTime(1995, 1, 1).AddDays(-500),
            ValidTo = new DateTime(1995, 1, 1).AddDays(500),
        };

        public static List<User> Users => UserTestsData.Users;

        public static List<Subscription> Subscriptions => SubscriptionTestsData.Subscriptions;

        public static List<PaySubscription> PaySubscriptions()
        {
            var result = new List<PaySubscription>();
            var rnd = new Random(0);
            var usersCount = Users.Count;
            var id = 0;
            var subscriptionsCount = Subscriptions.Count;
            int[] validationRanges = { 1, 2, 3, 6, 12 };
            var validationRangesCount = validationRanges.Length;
            for (int i = 0; i < usersCount; i++)
            {
                var paySubscriptionsCountOfUser = rnd.Next(0, 5);
                for (int j = 0; j < paySubscriptionsCountOfUser; j++)
                {
                    DateTime validFrom, validTo;
                    var userId = Users[i].Id;
                    var valid = validationRanges[rnd.Next(0, validationRangesCount)];
                    do
                    {
                        validFrom = new DateTime(1995, 1, 1);
                        var rndDays = rnd.Next(-10000, 10000);
                        validFrom = validFrom.AddDays(rndDays);
                        validTo = validFrom.AddMonths(valid);
                        if (!result.Any(ps => ps.UserId == userId))
                        {
                            break;
                        }

                        var isTimePerfect = result.Any(ps => (ps.UserId == userId
                            && !(validFrom <= ps.ValidFrom && validTo >= ps.ValidFrom))
                                || (validFrom >= ps.ValidFrom && validTo <= ps.ValidFrom)
                                || (validFrom <= ps.ValidFrom && validTo >= ps.ValidFrom)
                                || (validFrom >= ps.ValidFrom && validTo <= ps.ValidFrom));
                        if (isTimePerfect)
                        {
                            break;
                        }
                    }
                    while (true);

                    var subscriptionId = Subscriptions[rnd.Next(0, subscriptionsCount)].Id;
                    var userPaySubscription = new PaySubscription()
                    {
                        Id = "PaySubscription " + id,
                        SubscriptionId = subscriptionId,
                        UserId = userId,
                        ValidFrom = validFrom,
                        ValidTo = validTo,
                    };

                    result.Add(userPaySubscription);
                    id++;
                }
            }

            return result;
        }
    }
}
