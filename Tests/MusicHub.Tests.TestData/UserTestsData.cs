using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public static class UserTestsData
    {
        public static List<User> Users => new List<User>()
        {
            new User() { Id ="testUserId1", FirstName = "First Name1", LastName = "Last Name1", UserName= "UserName1", ImageUrl = "image"},
            new User() { Id ="testUserId2", FirstName = "First Name2", LastName = "Last Name2",  UserName= "UserName2", ImageUrl = "image"},
            new User() { Id ="testUserId3", FirstName = "First Name3", LastName = "Last Name3",  UserName= "UserName3", ImageUrl = "image"},
            new User() { Id ="testUserId4", FirstName = "First Name4", LastName = "Last Name4",  UserName= "UserName4", ImageUrl = "image"},
            new User() { Id ="testUserId5", FirstName = "First Name5", LastName = "Last Name5",  UserName= "UserName5", ImageUrl = "image"},
            new User() { Id ="testUserId6", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName6", ImageUrl = "image"},
            new User() { Id ="testUserId7", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName7", ImageUrl = "image"},
            new User() { Id ="testUserId8", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName8", ImageUrl = "image"},
            new User() { Id ="testUserId9", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName9", ImageUrl = "image"},
            new User() { Id ="testUserId10", FirstName = "First Name10", LastName = "Last Name10",  UserName= "UserName10", ImageUrl = "image"},
        };
    }
}
