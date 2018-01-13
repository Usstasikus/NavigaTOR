using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;
using System;
using System.Collections.Generic;

namespace DatabaseTest
{
    /// <summary>
    /// Test class for API of NavigaTOP database
    /// </summary>
    [TestClass]
    public class DB_APITest
    {
        #region UserTest

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
        public void AuthorizeTest()
        {
            Assert.IsTrue(DB_API.Authorize("user1", "user1"));
            Assert.IsFalse(DB_API.Authorize("user", "user"));
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            User user = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user);

            DB_API.RemoveUser(user.Id);

            Assert.IsNull(DB_API.GetUser(user.Id));

            User user0 = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user0);

            DB_API.RemoveUser(user0);

            Assert.IsNull(DB_API.GetUser(user.Id));

            User user1 = new User() { Login = "userTest1", Password = "passwordUserTest1", Name = "userTest1", Surname = "userTest1" };
            DB_API.RegisterUser(user1);

            Place place = new Place() { Title = "newPlace", Coordinates = "newPlace", Rating = 10 };
            DB_API.AddPlace(place);

            DB_API.RemoveUser(user1.Login);

            Assert.IsNull(DB_API.GetUser(user1.Id));
            DB_API.RemovePlace(place);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            User user = DB_API.GetUser(1);
            user.Update(login: "user111", password: "user111", name: "user111", surname: "user111", age: 20, tags: "tag");

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

            user.Update(login: "user1", password: "user1", name: "user1", surname: "user1");

            Assert.AreEqual("user1", user.Login);
            Assert.AreEqual("user1", user.Password);
            Assert.AreEqual("user1", user.Name);
            Assert.AreEqual("user1", user.Surname);
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

        [TestMethod]
        public void AddRouteToUserTest()
        {
            User user = DB_API.GetUser(1);

            Route route = new Route() { Rating = 5, DateTime = DateTime.Now, UserId = 1 };

            DB_API.AddRoute(route);

            user.AddRoute(route);

            Assert.AreEqual(user, route.User);
            Assert.AreEqual(2, user.GetRoutes().Count);

            DB_API.RemoveRoute(route);
        }

        [TestMethod]
        public void GetUserPlacesTest1()
        {
            User user = DB_API.GetUser(1);

            List<Place> list = user.GetPlaces();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(3, list[0].Id);
        }

        [TestMethod]
        public void GetUserRoutesTest()
        {
            User user = DB_API.GetUser(1);

            Assert.AreEqual(1, user.GetRoutes().Count);
            Assert.AreEqual(1010, new List<Route>(user.Routes)[0].Id);
        }

        #endregion

        #region PlaceTest

        [TestMethod]
        public void GetPlaceTest()
        {
            Assert.AreEqual(null, DB_API.GetPlace(0));

            Place place = new Place() { Title = "titTest", Coordinates = "coordTest", Rating = 10 };
            DB_API.AddPlace(place);

            Place placeTest = DB_API.GetPlace(place.Id);
            Assert.AreEqual(place, placeTest);

            DB_API.RemovePlace(place.Id);
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
        public void AddPlacesTest()
        {
            List<Place> list = new List<Place>() { new Place() { Title = "newPlace1", Coordinates = "newPlace1", Rating = 1 }, new Place() { Title = "newPlace2", Coordinates = "newPlace2", Rating = 1 } };
            DB_API.AddPlaces(list, false);

            Assert.IsNotNull(DB_API.GetPlace(list[0].Id));
            Assert.IsNotNull(DB_API.GetPlace(list[1].Id));

            DB_API.RemovePlace(list[0].Id);
            DB_API.RemovePlace(list[1].Id);
        }

        [TestMethod]
        public void GetPlacesTest()
        {
            Place placeTest1 = new Place() { Title = "newPlace1", Coordinates = "newPlace1", Rating = 1, Tags = "chineese restaurant;italian restaurant;mexican restaurant" };
            Place placeTest2 = new Place() { Title = "newPlace2", Coordinates = "newPlace2", Rating = 1, Tags = "chineese restaurant;american restaurant;spanish restaurant" };
            DB_API.AddPlace(placeTest1);
            DB_API.AddPlace(placeTest2);

            List<Place> list1 = DB_API.GetPlaces("chineese restaurant");
            Assert.AreEqual(list1.Count, 2);

            List<Place> list2 = DB_API.GetUser(1).GetPlaces();
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list2[0], DB_API.GetPlace(3));

            List<Place> list3 = DB_API.GetRoute(1010).GetPlaces();
            Assert.AreEqual(list3.Count, 1);
            Assert.AreEqual(list3[0], DB_API.GetPlace(3));

            DB_API.RemovePlace(placeTest1);
            DB_API.RemovePlace(placeTest2);
        }

        [TestMethod]
        public void GetUserPlacesTest()
        {
            List<Place> list = DB_API.GetUserPlaces(1);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(3, list[0].Id);
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
        public void UpdatePlaceTest()
        {
            Place place = DB_API.GetPlace(3);
            place.Update("tit2");

            Assert.AreEqual("tit2", place.Title);

            place.Update("tit1");
        }

        [TestMethod]
        public void GetRoutePlacesTest()
        {
            List<Place> list = DB_API.GetRoutePlaces(1010);

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void GetRoutesForPlaceTest()
        {
            Place place = DB_API.GetPlace(3);

            List<Route> list = place.GetRoutes();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1010, list[0].Id);
        }

        #endregion

        #region RouteTest

        [TestMethod]
        public void GetRouteTest()
        {
            Route route = DB_API.GetRoute(1010);

            Assert.AreEqual(route.UserId, 1);
            Assert.AreEqual(route.Rating, 5);
        }

        [TestMethod]
        public void AddRouteTest()
        {
            Route route = new Route() { Rating = 5, DateTime = DateTime.Now, UserId = 1 };
            DB_API.AddRoute(route);
            Place place1 = new Place() { Title = "tit2", Coordinates = "coord2", Rating = 10 };
            DB_API.AddPlace(place1);
            Place place2 = DB_API.GetPlace(3);
            route.AddPlaces(place1, place2);

            Assert.IsNotNull(DB_API.GetRoute(route.Id));

            Assert.AreEqual(2, route.GetPlaces().Count);

            DB_API.RemoveRoute(route.Id);

            DB_API.RemovePlace(place1.Id);
        }

        [TestMethod]
        public void GetRoutesByTagsTest()
        {
            List<Route> list = DB_API.GetRoutes("cool");

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1010, list[0].Id);
        }

        [TestMethod]
        public void GetUserRoutesTest1()
        {
            List<Route> list = DB_API.GetUserRoutes(1);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(DB_API.GetRoute(1010), list[0]);
        }

        [TestMethod]
        public void GetPlaceRoutesTest()
        {
            List<Route> list = DB_API.GetPlaceRoutes(3);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1010, list[0].Id);
        }

        [TestMethod]
        public void GetPlacesRoutesTest()
        {
            Place place = DB_API.GetPlace(3);

            List<Route> list = DB_API.GetPlacesRoutes(place);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1010, list[0].Id);
        }

        [TestMethod]
        public void UpdateRouteTest()
        {
            Route route = DB_API.GetRoute(1010);
            route.Update(6);

            Assert.AreEqual(6, route.Rating);

            route.Update(5);
        }

        #endregion

        #region FeedbackTest

        [TestMethod]
        public void AddFeedbackTest()
        {
            Feedback feedbackPlace = new Feedback(1, 10, "place is good");
            Feedback feedbackRoute = new Feedback(1, 10, "route is good");
            Place place = DB_API.GetPlace(3);
            Route route = DB_API.GetRoute(1010);

            place.AddFeedback(feedbackPlace);
            route.AddFeedback(feedbackRoute);
        }

        [TestMethod]
        public void RemoveFeedbackTest()
        {
            List<Feedback> list = DB_API.GetFeedbacksByUser(1);

            foreach (Feedback feed in list)
                DB_API.RemoveFeedback(feed);
        }

        [TestMethod]
        public void GetFeedbacksTest()
        {
            Feedback feedbackPlace = new Feedback(1, 10, "place is good");
            Feedback feedbackRoute = new Feedback(1, 10, "route is good");
            Place place = DB_API.GetPlace(3);
            Route route = DB_API.GetRoute(1010);
            place.AddFeedback(feedbackPlace);
            route.AddFeedback(feedbackRoute);

            List<Feedback> list = place.GetFeedbacks();
            List<Feedback> list1 = route.GetFeedbacks();
            List<Feedback> list2 = DB_API.GetFeedbacksByUser(1);

            Assert.AreEqual(list.Count + list1.Count, list2.Count);

            foreach (Feedback item in list)
                DB_API.RemoveFeedback(item);
            foreach (Feedback item in list1)
                DB_API.RemoveFeedback(item);
        }

        [TestMethod]
        public void GetFeedbacksByDateTest()
        {
            Feedback feedbackPlace = new Feedback(1, 10, "place is good");
            Feedback feedbackRoute = new Feedback(1, 10, "route is good");
            Place place = DB_API.GetPlace(3);
            Route route = DB_API.GetRoute(1010);
            place.AddFeedback(feedbackPlace);
            route.AddFeedback(feedbackRoute);

            List<Feedback> list = place.GetFeedbacksByDate(d => d > DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture));
            List<Feedback> list1 = place.GetFeedbacksByDate(d => d < DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture));
            Assert.AreNotEqual(list.Count, list1.Count);

            List<Feedback> list2 = route.GetFeedbacksByDate(d => d > DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture));
            List<Feedback> list3 = route.GetFeedbacksByDate(d => d < DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture));
            Assert.AreNotEqual(list2.Count, list3.Count);

            User user = DB_API.GetUser(1);
            List<Feedback> list4 = user.GetFeedbacksByDate(d => d > DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture));
            List<Feedback> list5 = user.GetFeedbacksByDate(d => d < DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture));
            Assert.AreNotEqual(list4.Count, list5.Count);

            foreach (Feedback item in user.GetFeedbacks())
                DB_API.RemoveFeedback(item);
        }

        [TestMethod]
        public void GetFeedbacksByRatingTest()
        {
            Feedback feedbackPlace = new Feedback(1, 10, "place is good");
            Feedback feedbackRoute = new Feedback(1, 10, "route is good");
            Place place = DB_API.GetPlace(3);
            Route route = DB_API.GetRoute(1010);
            place.AddFeedback(feedbackPlace);
            route.AddFeedback(feedbackRoute);

            List<Feedback> list = place.GetFeedbacksByRating(r => r > 5);
            List<Feedback> list1 = place.GetFeedbacksByRating(r => r <= 5);
            Assert.AreNotEqual(list.Count, list1.Count);

            List<Feedback> list2 = route.GetFeedbacksByRating(r => r > 5);
            List<Feedback> list3 = route.GetFeedbacksByRating(r => r <= 5);
            Assert.AreNotEqual(list2.Count, list3.Count);

            User user = DB_API.GetUser(1);
            List<Feedback> list4 = user.GetFeedbacksByRating(r => r > 5);
            List<Feedback> list5 = user.GetFeedbacksByRating(r => r <= 5);
            Assert.AreNotEqual(list4.Count, list5.Count);

            foreach (Feedback item in user.GetFeedbacks())
                DB_API.RemoveFeedback(item);
        }

        [TestMethod]
        public void GetFeedbacksByCondition()
        {
            Feedback feedbackPlace = new Feedback(1, 10, "place is good");
            Feedback feedbackRoute = new Feedback(1, 10, "route is good");
            Place place = DB_API.GetPlace(3);
            Route route = DB_API.GetRoute(1010);
            place.AddFeedback(feedbackPlace);
            route.AddFeedback(feedbackRoute);

            List<Feedback> list = DB_API.GetFeedbacks((u, p, r, f) => u.Id == 1 && p != null);
            List<Feedback> list1 = DB_API.GetFeedbacks((u, p, r, f) => u.Id == 2 && p != null);
            List<Feedback> list2 = DB_API.GetFeedbacks((u, p, r, f) => u.Id == 1 && r != null);
            List<Feedback> list3 = DB_API.GetFeedbacks((u, p, r, f) => f.Rating > 1);
            
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list1.Count, 0);
            Assert.AreEqual(list2.Count, 1);
            Assert.AreEqual(list3.Count, 2);

            User user = DB_API.GetUser(1);
            foreach (Feedback item in user.GetFeedbacks())
                DB_API.RemoveFeedback(item);
        }

        #endregion

        #region Test Service

        [ClassInitialize]
        public static void OnStartUp(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            DB_API.CloseConnection();
        }

        #endregion

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
