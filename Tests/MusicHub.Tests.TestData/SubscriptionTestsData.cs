namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.SubscriptionClaimModels;
    using MusicHub.Web.ViewModels.SubscriptionModels;

    public static class SubscriptionTestsData
    {
        public static SubscriptionCreateInputModel CreateModel => new SubscriptionCreateInputModel()
        {
            Name = "Simple Name",
            Price = 5,
            Type = SubscriptionType.MusicCreator,
            SubcriptionClaims = new List<SubscriptionClaimCreateInputModel>()
            {
                new SubscriptionClaimCreateInputModel() { Description = "simple desctiption" },
            },
        };

        public static SubscriptionEditInputModel UpdateModel => new SubscriptionEditInputModel()
        {
            Name = "edit Name",
            Price = 5,
            Type = SubscriptionType.MusicCreator,
            SubcriptionClaims = new List<SubscriptionClaimCreateInputModel>()
            {
                new SubscriptionClaimCreateInputModel() { Description = "edit desctiption" },
            },
        };

        public static SubscriptionEditInputModel UpdateModelEmptyModel => new SubscriptionEditInputModel()
        {
            Name = "",
            Price = 3,
            Type = SubscriptionType.MusicCreator,
            SubcriptionClaims = new List<SubscriptionClaimCreateInputModel>()
            {
                new SubscriptionClaimCreateInputModel() { Description = "simple description" },
            },
        };

        public static string SubscriptionId => Subscriptions[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static string UserId => UserTestsData.Users[0].Id;

        public static SubscriptionFilter FilterModel => new SubscriptionFilter()
        {
            Name = "s",
            Type = SubscriptionType.MusicCreator,
        };

        public static SubscriptionFilter FilterModelWithoutType => new SubscriptionFilter()
        {
            Name = "s",
        };

        public static SubscriptionFilter FilterModelWithAllTypes => new SubscriptionFilter()
        {
            Name = "s",
            Type = SubscriptionType.All,
        };

        public static SubscriptionCreateInputModel CreateEmptyModel => new SubscriptionCreateInputModel()
        {
            Name = "",
            Price = 3,
            Type = SubscriptionType.MusicCreator,
            SubcriptionClaims = new List<SubscriptionClaimCreateInputModel>()
            {
                new SubscriptionClaimCreateInputModel() { Description = "simple description" },
            },
        };

        public static SubscriptionCreateInputModel CreateModelSubcriptionClaimsHasNoEntities => new SubscriptionCreateInputModel()
        {
            Name = "",
            Price = 0,
            Type = SubscriptionType.MusicCreator,
            SubcriptionClaims = new List<SubscriptionClaimCreateInputModel>(),
        };

        public static SubscriptionCreateInputModel CreateModelPriceIsLessThanZiro => new SubscriptionCreateInputModel()
        {
            Name = "",
            Price = -3,
            Type = SubscriptionType.MusicCreator,
            SubcriptionClaims = new List<SubscriptionClaimCreateInputModel>()
            {
                new SubscriptionClaimCreateInputModel() { Description = "simple description" },
            },
        };

        public static List<Subscription> Subscriptions => new List<Subscription>()
        {
            new Subscription() { Id = "Subscription1", Name = "Subsctiption name 1", Price = 5, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription2", Name = "Subsctiption name 2", Price = 5, Type = SubscriptionType.User, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription3", Name = "Subsctiption name 3", Price = 4, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription4", Name = "Subsctiption name 4", Price = 3, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription5", Name = "Subsctiption name 5", Price = 2, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription6", Name = "Subsctiption name 6", Price = 10, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription7", Name = "Subsctiption name 7", Price = 18, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription8", Name = "Subsctiption name 8", Price = 5, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription9", Name = "Subsctiption name 9", Price = 15, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
            new Subscription() { Id = "Subscription10", Name = "Subsctiption name 10", Price = 15, Type = SubscriptionType.MusicCreator, SubcriptionClaims = new List<SubscriptionClaim>(), PaySubscriptions = new List<PaySubscription>() },
        };

        public static List<SubscriptionClaim> SubscriptionsClaims()
        {
            var result = new List<SubscriptionClaim>();
            var rnd = new Random(0);
            var subscriptionsCount = Subscriptions.Count;
            int id = 0;
            for (int i = 0; i < subscriptionsCount; i++)
            {
                int subscriptionClaimCount = rnd.Next(0, 5);
                for (int j = 0; j < subscriptionClaimCount; j++)
                {
                    result.Add(new SubscriptionClaim()
                    {
                        Id = id.ToString(),
                        Description = "simple description" + id,
                        SubcriptionId = Subscriptions[i].Id,
                    });
                    id++;
                }
            }

            return result;
        }
    }
}
