#define DEBUG
#undef DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    /// <summary>
    /// API of NavigaTOP database
    /// </summary>
    public static class DB_API
    {
        #region Private static variables

        /// <summary>
        /// Connection to navigator database
        /// </summary>
        private static navigatordbContext db = new navigatordbContext();

        /// <summary>
        /// Flag that users table has to be loaded
        /// </summary>
        private static bool needToLoadUsers = true;

        /// <summary>
        /// Flag that places table has to be loaded
        /// </summary>
        private static bool needToLoadPlaces = true;

        /// <summary>
        /// Flag that routes table has to be loaded
        /// </summary>
        private static bool needToLoadRoutes = true;

        /// <summary>
        /// Flag that user_places table has to be loaded
        /// </summary>
        private static bool needToLoadUserPlaces = true;

        /// <summary>
        /// Flag that route_places table has to be loaded
        /// </summary>
        private static bool needToLoadRoutePlaces = true;

        /// <summary>
        /// Flag that feedbacks table has to be loaded
        /// </summary>
        private static bool needToLoadFeedbacks = true;

        #endregion

        #region User

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user">User to register</param>
        public static void RegisterUser(User user)
        {
            // Load local data
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }

            // If login is occupied...
            if (db.Users.Local.ToList().Any(u => u.Login == user.Login))
                throw new ArgumentException("Login is occupied. Choose another one!");

            // Add user
            user = db.Users.Add(user).Entity;
            needToLoadUsers = true;
            SaveChanges();
        }

        /// <summary>
        /// Remove user from db
        /// </summary>
        /// <param name="user">User to delete</param>
        public static void RemoveUser(User user)
        {
            // Remove by login
            RemoveUser(user.Id);
        }

        /// <summary>
        /// Remove user from db by id
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        public static void RemoveUser(int userId)
        {
            // Load local data
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }

            // Get user to delete
            User userToDelete = db.Users.Local.ToList().Find(u => u.Id == userId);

            // If user exists...
            if (userToDelete == null)
                return;

            // Load local data
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }
            // Delete all user connections to places
            foreach (UserPlace item in db.UserPlaces.Local.ToList())
            {
                if (item.UserId == userToDelete.Id)
                    db.UserPlaces.Remove(item);
            }

            // Delete all user connections to routes
            foreach (Route item in db.Routes.Local.ToList())
            {
                if (item.UserId == userToDelete.Id)
                {
                    item.UserId = 0;
                    db.Routes.Update(item);
                }
            }

            db.Users.Remove(userToDelete); // Delete user
            needToLoadUsers = true;
            needToLoadUserPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Remove user from db by login
        /// </summary>
        /// <param name="userLogin">Login of user to delete</param>
        public static void RemoveUser(string userLogin)
        {
            // Load local data
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }

            User userToDelete = db.Users.Local.ToList().Find(u => u.Login == userLogin);

            // If user exists...
            if (userToDelete == null)
                return;

            // Load local data
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }

            // Delete all user connections to places
            foreach (UserPlace item in db.UserPlaces.Local.ToList())
            {
                if (item.UserId == userToDelete.Id)
                    db.UserPlaces.Remove(item);
            }

            // Delete all user connections to routes
            foreach (Route item in db.Routes.Local.ToList())
            {
                if (item.UserId == userToDelete.Id)
                {
                    item.UserId = 0;
                    db.Routes.Update(item);
                }
            }

            db.Users.Remove(userToDelete); // Delete user
            needToLoadUsers = true;
            needToLoadUserPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Authorize the user by login and password
        /// </summary>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password (hash)</param>
        /// <returns><see langword="true"/>, if <paramref name="login"/> and <paramref name="password"/> are correct. Else - <see langword="false"/></returns>
        public static bool Authorize(string login, string password)
        {
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }
            return db.Users.Local.ToList().Find(user => user.Login == login && user.Password == password) != null;
        }

        /// <summary>
        /// Update information about user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <param name="name">User's name</param>
        /// <param name="surname">User's surname</param>
        /// <param name="age">User's age</param>
        /// <param name="tags">User's favorite tags</param>
        public static void Update(this User user, string login = null, string password = null,
                                                      string name = null, string surname = null,
                                                      int? age = null, string tags = null)
        {
            // Update what needed...
            if (login != null)
                user.Login = login;

            if (password != null)
                user.Password = password;

            if (name != null)
                user.Name = name;

            if (surname != null)
                user.Surname = surname;

            if (age != null)
                user.Age = age;

            if (tags != null)
                user.Tags = tags;

            // Update user
            user = db.Users.Update(user).Entity;
        }

        /// <summary>
        /// Add route to list of user's routes
        /// </summary>
        /// <param name="user">User to update</param>
        /// <param name="route">Route to add</param>
        public static void AddRoute(this User user, Route route)
        {
            if (route.Id == 0)
                throw new ArgumentException("Route doesn't exist in database! Add it first.");

            user.Routes.Add(route);

            db.Entry(user).State = EntityState.Modified;

            SaveChanges();
        }

        /// <summary>
        /// Add place to list of user's favorite places
        /// </summary>
        /// <param name="user">User to update</param>
        /// <param name="place">Place to add</param>
        public static void AddPlace(this User user, Place place)
        {
            if (place.Id == 0)
                throw new ArgumentException("Place doesn't exist in database! Add it to database before adding to user.");

            // Load local data
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }

            if (!db.UserPlaces.Local.ToList().Any(up => up.PlaceId == place.Id && up.UserId == user.Id))
                db.UserPlaces.Add(new UserPlace() { UserId = user.Id, PlaceId = place.Id });

            needToLoadUserPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Remove place from list of user's favorite places
        /// </summary>
        /// <param name="user">User to update</param>
        /// <param name="place">Place to remove</param>
        public static void RemovePlace(this User user, Place place)
        {
            // Load local data
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }

            // Search for such connection
            var entity = db.UserPlaces.Local.ToList().Find(up => up.UserId == user.Id && up.PlaceId == place.Id);
#if DEBUG
                if (entity == null) // If no connection found...
                    throw new KeyNotFoundException("There is no such connection (user to place) in database!");
#endif
            // Remove connection
            db.UserPlaces.Remove(entity);

            needToLoadUserPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Get information about user by ID
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>Entity of user</returns>
        public static User GetUser(int id)
        {
            // Load local data
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }

            // Search for user with given ID
            User user = db.Users.Local.ToList().Find(u => u.Id == id);
#if DEBUG
                // If no user found...
                if (user == null)
                    throw new KeyNotFoundException("There is no such user in database!");
#endif
            return user;
        }

        /// <summary>
        /// Get information about user by Login
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Entity of user</returns>
        public static User GetUser(string login)
        {
            // Load local data
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }

            // Search for user with given Login
            User user = db.Users.Local.ToList().Find(u => u.Login == login);
#if DEBUG
                // If no user found...
                if (user == null)
                    throw new KeyNotFoundException("There is no such user in database!");
#endif
            return user;
        }

        /// <summary>
        /// Get places by user
        /// </summary>
        /// <param name="user">User to analize</param>
        /// <returns>Collection of <see cref="Place"/> of user</returns>
        public static List<Place> GetPlaces(this User user)
        {
            // Load local data
            if (needToLoadPlaces)
            {
                db.Places.Load();
                needToLoadPlaces = false;
            }
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }

            // Get list of connections
            List<UserPlace> connections = db.UserPlaces.Local.ToList();

            // Select list of places from user's favorite places
            List<Place> places = (from userPlace in connections
                                  where userPlace.UserId == user.Id
                                  select db.Places.Local.ToList().Find(p => p.Id == userPlace.PlaceId)).ToList();
#if DEBUG
                if (!places.Any())
                    throw new ArgumentException("There are no places of this user!");
#endif

            return places;
        }

        /// <summary>
        /// Get routes by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Collection of <see cref="Route"/> of user</returns>
        public static List<Route> GetRoutes(this User user)
        {
            // Load data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }

            // Get list of user's routes
            List<Route> userRoutes = db.Routes.Local.Where(r => r.UserId == user.Id).ToList();
            
#if DEBUG
                if (!routes.Any())
                    throw new ArgumentException("There are no routes of this user!");
#endif

            return userRoutes;
        }

        #endregion

        #region Place

        /// <summary>
        /// Get information about place by ID
        /// </summary>
        /// <param name="id">ID of place</param>
        /// <returns>Entity of place</returns>
        public static Place GetPlace(int id)
        {
            // Load local data
            if (needToLoadPlaces)
            {
                db.Places.Load();
                needToLoadPlaces = false;
            }

            // Search for user with given ID
            Place place = db.Places.Local.ToList().Find(p => p.Id == id);
#if DEBUG
                // If no user found...
                if (place == null)
                    throw new KeyNotFoundException("There is no such place in database!");
#endif
            return place;
        }

        /// <summary>
        /// Add place to database
        /// </summary>
        /// <param name="place">Place to add</param>
        public static void AddPlace(Place place)
        {
            // Load local data
            if (needToLoadPlaces)
            {
                db.Places.Load();
                needToLoadPlaces = false;
            }

            if (!db.Places.Local.ToList().Any(pl => pl.Id == place.Id))
                // Add place to database
                place = db.Places.Add(place).Entity;

            needToLoadPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Get places by tags
        /// </summary>
        /// <param name="tags">Tags to search</param>
        /// <returns>Collection of <see cref="Place"/> with given tags</returns>
        public static List<Place> GetPlaces(string tags)
        {
            // Load local data
            if (needToLoadPlaces)
            {
                db.Places.Load();
                needToLoadPlaces = false;
            }

            // Get array of tags
            string[] tagsArr = tags.Split(';');
            // Get list of places
            List<Place> placesList = db.Places.Local.ToList();

            // Select places with given tags from list of all places
            List<Place> places = (from place in placesList
                                  where place.Tags?.Split(';')?.Count(tag => tagsArr.Contains(tag)) == tagsArr.Length
                                  select place).ToList();
#if DEBUG
                if (!places.Any())
                    throw new ArgumentException("There are no places with such tags!");
#endif

            return places;
        }

        /// <summary>
        /// Get places by user ID
        /// </summary>
        /// <param name="userId">ID of user to analize</param>
        /// <returns>Collection of <see cref="Place"/> of user</returns>
        public static List<Place> GetUserPlaces(int userId)
        {
            // Load local data
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }

            // Get user by id
            User user = db.Users.Local.ToList().Find(u => u.Id == userId);
            // Get list of connections
            List<UserPlace> connections = db.UserPlaces.Local.ToList();

            // Select list of places from user's favorite places
            List<Place> places = (from userPlace in connections
                                  where userPlace.User == user
                                  select userPlace.Place).ToList();
#if DEBUG
                if (!places.Any())
                    throw new ArgumentException("There are no places of this user!");
#endif

            return places;
        }

        /// <summary>
        /// Get places by route ID
        /// </summary>
        /// <param name="routeId">Route to analize</param>
        /// <returns>Collection of <see cref="Place"/> of route</returns>
        public static List<Place> GetRoutePlaces(int routeId)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }
            // Get route by ID
            Route route = db.Routes.Local.ToList().Find(r => r.Id == routeId);
            // Get list of connections
            ICollection<RoutePlace> connections = db.RoutePlaces.Local.ToList();

            // Select list of places from route's places
            List<Place> places = (from routePlace in connections
                                  where routePlace.Route == route
                                  select routePlace.Place).ToList();
#if DEBUG
                if (!places.Any())
                    throw new ArgumentException("There are no places in this route!");
#endif

            return places;
        }

        /// <summary>
        /// Get all the routes that contain given place
        /// </summary>
        /// <param name="place">Place to analize</param>
        /// <returns>Collection of <see cref="Route"/> that contain given place</returns>
        public static List<Route> GetRoutes(this Place place)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // Get list of routes
            List<Route> routesList = db.Routes.Local.ToList();

            // Select list of routes by place
            List<Route> routes = (from route in routesList
                                  from routePlace in db.RoutePlaces.Local
                                  where routePlace.PlaceId == place.Id
                                  select route).ToList();
#if DEBUG
                if (!routes.Any())
                    throw new ArgumentException("There are no routes with this place!");
#endif

            return routes;
        }

        /// <summary>
        /// Remove place from database
        /// </summary>
        /// <param name="place">Place to remove</param>
        public static void RemovePlace(Place place)
        {
            if (place == null)
                return;

            // Load local data
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // If there are some connections to users or routes
            if (db.UserPlaces.Local.ToList().Any(up => up.PlaceId == place.Id)
                || db.RoutePlaces.Local.ToList().Any(rp => rp.PlaceId == place.Id))
                throw new ArgumentException("Place can't be deleted! It is in some routes or user's lists.");

            // Remove place from database
            db.Places.Remove(place);

            needToLoadPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Remove place from database by ID
        /// </summary>
        /// <param name="placeId">Place to remove (by ID)</param>
        public static void RemovePlace(int placeId)
        {
            // Load local data
            if (needToLoadPlaces)
            {
                db.Places.Load();
                needToLoadPlaces = false;
            }

            // Get place from database
            Place place = db.Places.Local.ToList().Find(p => p.Id == placeId);

            if (place == null)
                return;

            // Load local data
            if (needToLoadUserPlaces)
            {
                db.UserPlaces.Load();
                needToLoadUserPlaces = false;
            }
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // If there are some connections to users or routes
            if (db.UserPlaces.Local.ToList().Any(up => up.PlaceId == place.Id)
                || db.RoutePlaces.Local.ToList().Any(rp => rp.PlaceId == place.Id))
                throw new ArgumentException("Place can't be deleted! It is in some routes or user's lists.");

            // Remove place from database
            db.Places.Remove(place);

            needToLoadPlaces = true;
            SaveChanges();
        }

        /// <summary>
        /// Update place
        /// </summary>
        /// <param name="place">Place to update</param>
        /// <param name="title">Place's title</param>
        /// <param name="address">Place's address</param>
        /// <param name="coordinates">Place's coordinates</param>
        /// <param name="source">Place's source</param>
        /// <param name="tags">Place's tags</param>
        /// <param name="description">Place's description</param>
        /// <param name="contacts">Contacts of this place</param>
        /// <param name="rating">Place's rating</param>
        /// <param name="limitations">Place's limitations</param>
        public static void Update(this Place place, string title = null, string address = null,
                                                         string coordinates = null, string source = null,
                                                         string tags = null, string description = null,
                                                         string contacts = null, double rating = Double.NaN,
                                                         string limitations = null)
        {
            // Update where needed
            if (title != null)
                place.Title = title;

            if (address != null)
                place.Address = address;

            if (coordinates != null)
                place.Coordinates = coordinates;

            if (source != null)
                place.Source = source;

            if (tags != null)
                place.Tags = tags;

            if (description != null)
                place.Description = description;

            if (contacts != null)
                place.Contacts = contacts;

            if (!Double.IsNaN(rating))
                place.Rating = rating;

            if (limitations != null)
                place.Limitations = limitations;

            // Update place
            db.Places.Update(place);
        }

        #endregion

        #region Route
        
        /// <summary>
        /// Get information about route by ID
        /// </summary>
        /// <param name="id">ID of route</param>
        /// <returns>Entity of route</returns>
        public static Route GetRoute(int id)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }

            // Search for user with given ID
            Route route = db.Routes.Local.ToList().Find(r => r.Id == id);
#if DEBUG
                // If no user found...
                if (route == null)
                    throw new KeyNotFoundException("There is no such route in database!");
#endif
            return route;
        }

        /// <summary>
        /// Add place to route
        /// </summary>
        /// <param name="route">Route to update</param>
        /// <param name="place">Place to add</param>
        public static void AddPlace(this Route route, Place place)
        {
            if (place.Id == 0)
                throw new ArgumentException("Place doesn't exist in database! Add it to database before adding to route.");

            // Load local data
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            RoutePlace routePlace = new RoutePlace() { RouteId = route.Id, PlaceId = place.Id };

            if (!db.RoutePlaces.Local.ToList().Any(rp => rp.PlaceId == place.Id && rp.RouteId == route.Id))
                db.RoutePlaces.Add(routePlace);

            route.RoutePlaces.Add(routePlace);
            needToLoadRoutePlaces = true;
        }

        /// <summary>
        /// Add places to route
        /// </summary>
        /// <param name="route">Route to update</param>
        /// <param name="places">Places to add</param>
        public static void AddPlaces(this Route route, params Place[] places)
        {
            if (route.Id == 0)
                throw new ArgumentException($"Route doesn't exist in database! Add it to database before adding places.");

            foreach (Place place in places)
            {
                if (place.Id == 0)
                    throw new ArgumentException($"Place \"{place.Title}\" doesn't exist in database! Add it to database before adding to route.");
            }
            // Load local data
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            foreach (Place place in places)
            {
                RoutePlace routePlace = new RoutePlace() { RouteId = route.Id, PlaceId = place.Id };

                if (!db.RoutePlaces.Local.ToList().Any(rp => rp.PlaceId == place.Id && rp.RouteId == route.Id))
                    db.RoutePlaces.Add(routePlace);

                route.RoutePlaces.Add(routePlace);
            }
            needToLoadRoutePlaces = true;
        }

        /// <summary>
        /// Get places by route
        /// </summary>
        /// <param name="routeId">Route to analize</param>
        /// <returns>Collection of <see cref="Place"/> of route</returns>
        public static List<Place> GetPlaces(this Route route)
        {
            // Load local data
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // Get list of connections
            ICollection<RoutePlace> connections = db.RoutePlaces.Local.ToList();

            // Select list of places from route's places
            List<Place> places = (from routePlace in connections
                                  where routePlace.RouteId == route.Id
                                  select routePlace.Place).ToList();
#if DEBUG
                if (!places.Any())
                    throw new ArgumentException("There are no places in this route!");
#endif

            return places;
        }

        /// <summary>
        /// Get routes by tags
        /// </summary>
        /// <param name="tags">Tags to search</param>
        /// <returns>Collection of <see cref="Route"/> with given tags</returns>
        public static List<Route> GetRoutes(string tags)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }

            // Get array of tags
            string[] tagsArr = tags.Split(';');
            // Get list of routes
            List<Route> routesList = db.Routes.Local.ToList();

            // Select routes with given tags from list of all places
            List<Route> routes = (from route in routesList
                                  where route.Tags.Split(';').Count(tag => tagsArr.Contains(tag)) == tagsArr.Length
                                  select route).ToList();
#if DEBUG
                if (!routes.Any())
                    throw new ArgumentException("There are no routes with such tags!");
#endif

            return routes;
        }

        /// <summary>
        /// Get routes by user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Collection of <see cref="Route"/> of user</returns>
        public static List<Route> GetUserRoutes(int userId)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }

            // Get user by id
            User user = db.Users.Find(userId);
            // Get list of user's routes
            List<Route> userRoutes = db.Routes.Local.ToList();

            // Select list of routes by user
            List<Route> routes = (from route in userRoutes
                                  where route.UserId == user.Id
                                  select route).ToList();
#if DEBUG
                if (!routes.Any())
                    throw new ArgumentException("There are no routes of this user!");
#endif

            return routes;
        }

        /// <summary>
        /// Get all the routes that contain given place (by id)
        /// </summary>
        /// <param name="placeId">ID of place to analize</param>
        /// <returns>Collection of <see cref="Route"/> that contain given place (by id)</returns>
        public static List<Route> GetPlaceRoutes(int placeId)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // Get place by place ID
            Place place = db.Places.Find(placeId);
            // Get list of routes
            List<Route> routesList = db.Routes.Local.ToList();

            // Select list of routes by place 
            List<Route> routes = (from route in routesList
                                  from routePlace in db.RoutePlaces.Local
                                  where routePlace.PlaceId == place.Id
                                  select route).ToList();
#if DEBUG
                if (!routes.Any())
                    throw new ArgumentException("There are no routes with this place!");
#endif

            return routes;
        }

        /// <summary>
        /// Get all the routes that contain given places
        /// </summary>
        /// <param name="places">Places to analize</param>
        /// <returns>Collection of <see cref="Route"/> that contain given place</returns>
        public static List<Route> GetPlacesRoutes(params Place[] places)
        {
            // Load local data
            if (needToLoadRoutes)
            {
                db.Routes.Load();
                needToLoadRoutes = false;
            }
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // Get list of routes
            List<Route> routesList = db.Routes.Local.ToList();

            // Select list of routes that contain places
            List<Route> routes = (from route in routesList
                                  where db.RoutePlaces.Local.Count(routePlace => places.Contains(routePlace.Place)) >= places.Length // We could have route where one place occurs twice, that's why it is >= (not ==)
                                  select route).ToList();
#if DEBUG
                if (!routes.Any())
                    throw new ArgumentException("There are no places with such places!");
#endif

            return routes;
        }

        /// <summary>
        /// Add route to database
        /// </summary>
        /// <param name="route">Route to add</param>
        public static void AddRoute(Route route)
        {
            // Add route to database
            route = db.Routes.Add(route).Entity;

            User user = db.Users.Find(route.UserId);

            user.Routes.Add(route);

            needToLoadRoutes = true;
            SaveChanges();
        }

        /// <summary>
        /// Remove route from database
        /// </summary>
        /// <param name="route">Route to remove</param>
        public static void RemoveRoute(Route route)
        {
            RemoveRoute(route.Id);
        }

        /// <summary>
        /// Remove route from database by ID
        /// </summary>
        /// <param name="routeId">Route to remove (by ID)</param>
        public static void RemoveRoute(int routeId)
        {
            // Get route from database
            Route route = db.Routes.Find(routeId);

            if (route == null)
                return;

            // Load local data
            if (needToLoadRoutePlaces)
            {
                db.RoutePlaces.Load();
                needToLoadRoutePlaces = false;
            }

            // Delete all user connections to places
            foreach (RoutePlace item in db.RoutePlaces.Local.ToList())
            {
                if (item.RouteId == route.Id)
                    db.RoutePlaces.Remove(item);
            }

            User user = db.Users.Find(route.UserId);

            user.Routes.Remove(route);

            // Remove route from database
            db.Routes.Remove(route);

            needToLoadRoutes = true;
            SaveChanges();
        }

        

        /// <summary>
        /// Update route
        /// </summary>
        /// <param name="route">Route to update</param>
        /// <param name="rating">Route's rating</param>
        /// <param name="feedback">Feedback to route</param>
        /// <param name="access">Type of access to route</param>
        /// <param name="description">Route's description</param>
        /// <param name="tags">Route's tags</param>
        public static void Update(this Route route, double rating = Double.NaN, string feedback = null,
                                                         string access = null, string description = null,
                                                         string tags = null)
        {
            // Update where needed
            if (!Double.IsNaN(rating))
                route.Rating = rating;

            if (feedback != null)
                route.Feedback = feedback;

            if (access != null)
                route.Access = access;

            if (description != null)
                route.Description = description;

            if (tags != null)
                route.Tags = tags;

            // Update place
            db.Routes.Update(route);
        }

        #endregion

        #region Feedback

        /// <summary>
        /// Add feedback to place
        /// </summary>
        /// <param name="place">Place</param>
        /// <param name="feedback">Feedback</param>
        public static void AddFeedback(this Place place, Feedback feedback)
        {
            // Bind feedback to place
            feedback.PlaceId = place.Id;
            // Set current time
            feedback.DateTime = DateTime.Now;
            // Add to db
            feedback = db.Add(feedback).Entity;
            // Add to list of place's feedbacks
            //place.Feedbacks.Add(feedback);
            needToLoadFeedbacks = true;
            // Save changes
            SaveChanges();
        }

        /// <summary>
        /// Add feedback to route
        /// </summary>
        /// <param name="route">Route</param>
        /// <param name="feedback">Feedback</param>
        public static void AddFeedback(this Route route, Feedback feedback)
        {
            // Bind feedback to route
            feedback.RouteId = route.Id;
            // Set current time
            feedback.DateTime = DateTime.Now;
            // Add to db
            feedback = db.Feedbacks.Add(feedback).Entity;
            // Add to list of route's feedbacks
            route.Feedbacks.Add(feedback);
            needToLoadFeedbacks = true;
            // Save changes
            SaveChanges();
        }

        /// <summary>
        /// Remove feedback from db
        /// </summary>
        /// <param name="feedbackId">Feedback id</param>
        public static void RemoveFeedback(int feedbackId)
        {
            // Get feedback by id
            Feedback feedbackToDelete = db.Feedbacks.Find(feedbackId);

            // If it doesn't exists...
            if (feedbackToDelete == null)
                return;

            // Remove from db 
            db.Feedbacks.Remove(feedbackToDelete);
            needToLoadFeedbacks = true;
            SaveChanges();
        }

        /// <summary>
        /// Remove feedback from db
        /// </summary>
        /// <param name="feedback">Feedback</param>
        public static void RemoveFeedback(Feedback feedback)
        {
            // Remove feedback
            RemoveFeedback(feedback.Id);
        }

        /// <summary>
        /// Get feedbacks by place
        /// </summary>
        /// <param name="route">Place to get feedbacks</param>
        /// <returns>List of feedbacks on <paramref name="route"/></returns>
        public static List<Feedback> GetFeedbacksByPlace(int placeId)
        {
            Place place = db.Places.Find(placeId);
            return place.GetFeedbacks();
        }

        /// <summary>
        /// Get feedbacks by route
        /// </summary>
        /// <param name="route">Route to get feedbacks</param>
        /// <returns>List of feedbacks on <paramref name="route"/></returns>
        public static List<Feedback> GetFeedbacksByRoute(int routeId)
        {
            Route route = db.Routes.Find(routeId);
            return route.GetFeedbacks();
        }

        /// <summary>
        /// Get feedbacks by user
        /// </summary>
        /// <param name="user">User to get feedbacks</param>
        /// <returns>List of feedbacks on <paramref name="user"/></returns>
        public static List<Feedback> GetFeedbacksByUser(int userId)
        {
            User user = db.Users.Find(userId);
            return user.GetFeedbacks();
        }

        /// <summary>
        /// Get feedbacks by place
        /// </summary>
        /// <param name="place">Place</param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacks(this Place place)
        {
            // If place is null...
            if (place == null)
                throw new ArgumentException("Place can't be null!");

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByPlace = (from feedback in place.Feedbacks
                                               where feedback.PlaceId == place.Id
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByPlace;
        }

        /// <summary>
        /// Get feedbacks by route
        /// </summary>
        /// <param name="route">Route</param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacks(this Route route)
        {
            // If route is null...
            if (route == null)
                throw new ArgumentException("Route can't be null!");

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByRoute = (from feedback in route.Feedbacks
                                               where feedback.RouteId == route.Id
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByRoute;
        }

        /// <summary>
        /// Get feedbacks by user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacks(this User user)
        {
            // If user is null...
            if (user == null)
                throw new ArgumentException("User can't be null!");

            // Load data
            if (needToLoadFeedbacks)
            {
                db.Feedbacks.Load();
                needToLoadFeedbacks = false;
            }
            List<Feedback> feedbacksList = db.Feedbacks.Local.ToList();

            // Get feedbacks by user id by LINQ query
            List<Feedback> feedbacksByUser = (from feedback in feedbacksList
                                              where feedback.UserId == user.Id
                                              select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByUser;
        }

        /// <summary>
        /// Get feedbacks by place and boolean condition on date
        /// </summary>
        /// <param name="place">Place</param>
        /// <param name="predicate">Boolean condition on <see cref="DateTime"/></param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacksByDate(this Place place, Predicate<DateTime> predicate = null)
        {
            // Default predicate
            if (predicate == null)
                predicate = f => true;

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByPlace = (from feedback in place.Feedbacks
                                               where feedback.PlaceId == place.Id && predicate.Invoke(feedback.DateTime)
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByPlace;
        }

        /// <summary>
        /// Get feedbacks by route and boolean condition on date
        /// </summary>
        /// <param name="route">Route</param>
        /// <param name="predicate">Boolean condition on <see cref="DateTime"/></param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacksByDate(this Route route, Predicate<DateTime> predicate = null)
        {
            // Default predicate
            if (predicate == null)
                predicate = f => true;

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByRoute = (from feedback in route.Feedbacks
                                               where feedback.RouteId == route.Id && predicate.Invoke(feedback.DateTime)
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByRoute;
        }

        /// <summary>
        /// Get feedbacks by user and boolean condition on date
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="predicate">Boolean condition on <see cref="DateTime"/></param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacksByDate(this User user, Predicate<DateTime> predicate = null)
        {
            // Load data
            if (needToLoadFeedbacks)
            {
                db.Feedbacks.Load();
                needToLoadFeedbacks = false;
            }
            List<Feedback> feedbacksList = db.Feedbacks.Local.ToList();

            // Default predicate
            if (predicate == null)
                predicate = f => true;

            // Get feedbacks by user id by LINQ query
            List<Feedback> feedbacksByUser = (from feedback in feedbacksList
                                               where feedback.UserId == user.Id && predicate.Invoke(feedback.DateTime)
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByUser;
        }

        /// <summary>
        /// Get feedbacks by place and boolean condition on rating
        /// </summary>
        /// <param name="place">Place</param>
        /// <param name="predicate">Boolean condition on <see cref="int"/></param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacksByRating(this Place place, Predicate<int> predicate = null)
        {
            // Default predicate
            if (predicate == null)
                predicate = f => true;

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByRoute = (from feedback in place.Feedbacks
                                               where feedback.PlaceId == place.Id && predicate.Invoke(feedback.Rating)
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByRoute;
        }

        /// <summary>
        /// Get feedbacks by route and boolean condition on rating
        /// </summary>
        /// <param name="route">Route</param>
        /// <param name="predicate">Boolean condition on <see cref="int"/></param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacksByRating(this Route route, Predicate<int> predicate = null)
        {
            // Default predicate
            if (predicate == null)
                predicate = f => true;

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByRoute = (from feedback in route.Feedbacks
                                               where feedback.RouteId == route.Id && predicate.Invoke(feedback.Rating)
                                               select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByRoute;
        }

        /// <summary>
        /// Get feedbacks by user and boolean condition on rating
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="predicate">Boolean condition on <see cref="int"/></param>
        /// <returns>List of feedbacks</returns>
        public static List<Feedback> GetFeedbacksByRating(this User user, Predicate<int> predicate = null)
        {
            // Load data
            if (needToLoadFeedbacks)
            {
                db.Feedbacks.Load();
                needToLoadFeedbacks = false;
            }
            List<Feedback> feedbacksList = db.Feedbacks.Local.ToList();

            // Default predicate
            if (predicate == null)
                predicate = f => true;

            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByUser = (from feedback in feedbacksList
                                              where feedback.UserId == user.Id && predicate.Invoke(feedback.Rating)
                                              select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByUser;
        }

        /// <summary>
        /// Get feedbacks by any condition
        /// </summary>
        /// <param name="function">Condition</param>
        /// <returns></returns>
        public static List<Feedback> GetFeedbacks(Func<User, Place, Route, Feedback, bool> function)
        {
            // If function is null...
            if (function == null)
                throw new ArgumentException("Condition can't be null!");

            // Load data
            if (needToLoadFeedbacks)
            {
                db.Feedbacks.Load();
                needToLoadFeedbacks = false;
            }
            if (needToLoadUsers)
            {
                db.Users.Load();
                needToLoadUsers = false;
            }
            //if (needToLoadPlaces)
            //{
            //    db.Places.Load();
            //    needToLoadPlaces = false;
            //}
            //if (needToLoadRoutes)
            //{
            //    db.Routes.Load();
            //    needToLoadRoutes = false;
            //}

            List<Feedback> feedbacks = db.Feedbacks.Local.ToList();
            List<User> users = db.Users.Local.ToList();
            //List<Place> places = db.Places.Local.ToList();
            //List<Route> routes = db.Routes.Local.ToList();
            
            // Get feedbacks by LINQ query
            List<Feedback> feedbacksByFunc = (from feedback in feedbacks
                                              where function(users.Find(u => u.Id == feedback.UserId),
                                                             feedback.Place,//places.Find(p => p.Id == feedback.PlaceId),
                                                             feedback.Route,//routes.Find(r => r.Id == feedback.RouteId),
                                                             feedback)
                                              select feedback).ToList();

            // Return list of feedbacks
            return feedbacksByFunc;
        }

        #endregion

        #region Service methods

        /// <summary>
        /// Save changes to navigator db
        /// </summary>
        public static void SaveChanges()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Close the connection to navigator db
        /// </summary>
        public static void CloseConnection()
        {
            db.SaveChanges();
            db.Dispose();
        }

        #endregion
    }
}
