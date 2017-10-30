using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;
using System;
using System.Collections.Generic;

namespace DatabaseTest
{
    [TestClass]
    public class DB_APITest
    {
        [ClassInitialize]
        public static void OnStartUp(TestContext testContext)
        {            
        }

        [TestMethod]
        public void GetUserTest()
        {
            Assert.AreEqual(null, DB_API.GetUser(0));

            User user = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user);

            User userTest = DB_API.GetUser(user.Id);
            TestUserEqualityWithId(user, userTest);

            DB_API.RemoveUser(user.Id);
        }

        [TestMethod]
        public void RegisterUserTest()
        {
            User user = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user);

            Assert.ThrowsException<ArgumentException>(() => DB_API.RegisterUser(user));

            User userTest = DB_API.GetUser(user.Id);

            TestUserEquality(user, userTest);

            DB_API.RemoveUser(user.Id);
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            User user = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user);

            DB_API.RemoveUser(user.Id);

            Assert.IsNull(DB_API.GetUser(user.Id));

            User user1 = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user1);

            Place place = new Place() { Title = "newPlace", Coordinates = "newPlace", Rating = 10 };
            DB_API.AddPlace(place);

            DB_API.RemoveUser(user1.Id);

            Assert.IsNull(DB_API.GetUser(user1.Id));
            DB_API.RemovePlace(place);
        }

        [TestMethod]
        public void AuthorizeTest()
        {
            Assert.IsTrue(DB_API.Authorize("user1", "user1"));
            Assert.IsFalse(DB_API.Authorize("user", "user"));
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            User user = DB_API.GetUser(1);
            user.UpdateUser(login: "user111", password: "user111", name: "user111", surname: "user111", age: 20, tags: "tag");

            Assert.AreEqual(1, user.Id);
            Assert.AreNotEqual("user1", user.Login);
            Assert.AreNotEqual("user1", user.Password);
            Assert.AreNotEqual("user1", user.Name);
            Assert.AreNotEqual("user1", user.Surname);
            Assert.IsNotNull(user.Age);
            Assert.IsNotNull(user.Tags);

            Assert.AreEqual("user111", user.Login);
            Assert.AreEqual("user111", user.Password);
            Assert.AreEqual("user111", user.Name);
            Assert.AreEqual("user111", user.Surname);

            user.UpdateUser(login: "user1", password: "user1", name: "user1", surname: "user1");

            Assert.AreEqual("user1", user.Login);
            Assert.AreEqual("user1", user.Password);
            Assert.AreEqual("user1", user.Name);
            Assert.AreEqual("user1", user.Surname);
        }

        [TestMethod]
        public void AddPlaceTest()
        {
            Place place = new Place() { Title = "newPlace", Coordinates = "newPlace", Rating = 1 };
            DB_API.AddPlace(place);

            Assert.IsNotNull(DB_API.GetPlace(place.Id));

            DB_API.RemovePlace(place.Id);
        }

        [TestMethod]
        public void RemovePlaceTest()
        {
            Place place = new Place() { Title = "newPlace", Coordinates = "newPlace", Rating = 1 };
            DB_API.AddPlace(place);

            Assert.ThrowsException<ArgumentException>(() => DB_API.RemovePlace(3));

            DB_API.RemovePlace(place.Id);

            Assert.IsNull(DB_API.GetPlace(place.Id));
        }

        [TestMethod]
        public void AddPlaceToUserTest()
        {
            User user = DB_API.GetUser(1);
            Place place = new Place() { Title = "newPlace", Coordinates = "newPlace", Rating = 10 };

            Assert.ThrowsException<ArgumentException>(() => user.AddPlace(place));

            DB_API.AddPlace(place);
            user.AddPlace(place);

            Assert.AreEqual(2, user.GetPlaces().Count);

            Assert.ThrowsException<ArgumentException>(() => DB_API.RemovePlace(place));

            user.RemovePlace(place);
            DB_API.RemovePlace(place);
        }

        [TestMethod]
        public void RemovePlaceFromUserTest()
        {
            User user = DB_API.GetUser(1);
            Place place = new Place() { Title = "newPlace", Coordinates = "newPlace", Rating = 10 };

            DB_API.AddPlace(place);
            user.AddPlace(place);

            user.RemovePlace(place);
            DB_API.RemovePlace(place);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            DB_API.CloseConnection();
        }

        #region Helper methods        

        private void TestUserEquality(User left, User right)
        {
            Assert.AreEqual(left.Login, right.Login);
            Assert.AreEqual(left.Password, right.Password);
            Assert.AreEqual(left.Name, right.Name);
            Assert.AreEqual(left.Surname, right.Surname);
        }

        private void TestUserEqualityWithId(User left, User right)
        {
            Assert.AreEqual(left.Id, right.Id);
            Assert.AreEqual(left.Login, right.Login);
            Assert.AreEqual(left.Password, right.Password);
            Assert.AreEqual(left.Name, right.Name);
            Assert.AreEqual(left.Surname, right.Surname);
        }

        #endregion
    }
}
