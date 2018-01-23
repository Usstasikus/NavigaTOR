using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using DataMosAPI;
using Database;

using Newtonsoft.Json;

namespace NavDB
{
    class DataPicker
    {
        string APIKey;
        string path;
        string tags;
        public DataPicker(string pathToAPIKey, string path)
        {
            using (StreamReader sm = new StreamReader(pathToAPIKey))
            {
                APIKey = sm.ReadLine();//получение ключа API
            }
            this.path = path; //получение пути файла последнего состояния
        }

        public void GetData()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != "")
                {
                    string[] words = line.Split();
                    string dataSetID = words[0];
                    tags = line.Substring(dataSetID.Length); //words.Skip(1).Take(words.Length - 1).ToArray();

                    //формируем запрос по набору для получения количества объектов в нем и даты последнего изменения (в будущем)
                    string dataAmountUrl = String.Format("https://apidata.mos.ru/v1/datasets/{0}/count?api_key={1}", dataSetID, APIKey);
                    HttpWebRequest dataAmountRequest = (HttpWebRequest)WebRequest.Create(dataAmountUrl);
                    HttpWebResponse dataAmountResponse = (HttpWebResponse)dataAmountRequest.GetResponse();
                    //количество объектов в наборе
                    int dataAmount;
                    using (var reader = new StreamReader(dataAmountResponse.GetResponseStream()))
                    {
                        if (!int.TryParse(reader.ReadToEnd(), out dataAmount)) throw new Exception("Unknown data amount");
                    }

                    //получение всех данных из набора
                    SplitToSmallerRequests(dataSetID, 0, dataAmount, dataAmount);


                }
                DB_API.CloseConnection();
            }
        }

        void SplitToSmallerRequests(string dataSetID, int skip, int step, int dataAmount)
        {
            //int requestAmount = (int)Math.Ceiling((double)dataAmount / dataConstraint);

            //for (int i = 0; i < requestAmount; i++)
            while (skip < dataAmount)
            {
                //формирование запроса для получения данных из категории
                string url = String.Format("https://apidata.mos.ru/v1/datasets/{0}/rows?api_key={1}&$skip={2}&$top={3}", dataSetID, APIKey, skip, step);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                try
                {
                    Console.Write(dataSetID + " started: ");

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    AddData(response, dataSetID);
                    Console.WriteLine("OK\n");
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed\n");
                    if (step != 1)
                    {
                        SplitToSmallerRequests(dataSetID, skip, step / 2, skip + step);
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter("log.txt"))
                        {
                            sw.WriteLine(dataSetID + '\n');
                        }

                    }
                }
                skip += step;
            }

        }

        void AddData(HttpWebResponse response, string dataSetID)
        {

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                //текст ответа на запрос
                string objText = reader.ReadToEnd();

                try
                {
                    //преобразовываем текст в список мест
                    List<Database.Place> places = TransfromToPlaceList(objText, dataSetID);


                    Database.DB_API.AddPlaces(places, false); // добавление списка мест в БД 
                }
                catch (JsonSerializationException ex)
                {
                    using (StreamWriter sw = new StreamWriter("log.txt"))
                    {
                        sw.WriteLine(objText);
                    }
                    throw ex;
                }

            }
        }

        List<Database.Place> TransfromToPlaceList(string objText, string dataSetID)
        {
            List<Database.Place> places = new List<Place>();

            //парсим входной текст в зависимости от набора данных, который обрабатывается
            if (dataSetID == "526" || dataSetID == "527" || dataSetID == "493" || dataSetID == "495" || dataSetID == "528" || dataSetID == "529" || dataSetID == "531")
            {
                List<PlaceStrings<List<String>, List<Phone<String[]>>, List<EmailBox>>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<List<String>, List<Phone<String[]>>, List<EmailBox>>>>(objText);

                foreach (var rawPlace in data)
                {
                    Database.Place place = new Place();

                    place.Address = rawPlace.Cells.Address;
                    place.Title = rawPlace.Cells.FinalName;
                    if (rawPlace.Cells.geoData.coordinates == null)
                        continue;
                    place.Coordinates = rawPlace.Cells.geoData.coordinates[0][0] + " " + rawPlace.Cells.geoData.coordinates[0][1];
                    place.Description = rawPlace.Cells.Description + "\n" + rawPlace.Cells.EntranceAdditionalInformation;
                    place.Contacts = rawPlace.Cells.WebSite;
                    if (rawPlace.Cells.PublicPhone != null)
                        place.Contacts += String.Join('\n', rawPlace.Cells.PublicPhone.Select(a => a.PublicPhone));
                    place.Source = "https://data.mos.ru/";
                    if (rawPlace.Cells.WorkingHours != null)
                        place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));
                    place.Tags = tags;
                    place.DateTime = DateTime.Now;
                    places.Add(place);
                }

            }
            else if (dataSetID == "1903" || dataSetID == "2624")
            {
                List<PlaceStrings<String, List<Phone<String>>, String>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<String, List<Phone<String>>, String>>>(objText);

                foreach (var rawPlace in data)
                {
                    Database.Place place = new Place();

                    place.Address = rawPlace.Cells.Address;
                    place.Title = rawPlace.Cells.FinalName;
                    if (rawPlace.Cells.geoData.coordinates == null)
                        continue;
                    place.Coordinates = rawPlace.Cells.geoData.coordinates[0] + " " + rawPlace.Cells.geoData.coordinates[1];
                    place.Description = rawPlace.Cells.Description + "\n" + rawPlace.Cells.EntranceAdditionalInformation;
                    place.Contacts = rawPlace.Cells.WebSite;
                    if (rawPlace.Cells.PublicPhone != null)
                        place.Contacts += String.Join('\n', rawPlace.Cells.PublicPhone.Select(a => a.PublicPhone));
                    place.Source = "https://data.mos.ru/";
                    if (rawPlace.Cells.WorkingHours != null)
                        place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));
                    place.Tags = tags;
                    place.DateTime = DateTime.Now;
                    places.Add(place);
                }
            }
            else if (dataSetID == "530")
            {
                List<PlaceStrings<List<List<String>>, String, String>> data = JsonConvert.DeserializeObject<List<PlaceStrings<List<List<String>>, String, String>>>(objText);

                foreach (var rawPlace in data)
                {
                    Database.Place place = new Place();

                    place.Address = rawPlace.Cells.Address;
                    place.Title = rawPlace.Cells.FinalName;
                    if (rawPlace.Cells.geoData.coordinates == null)
                        continue;
                    place.Coordinates = rawPlace.Cells.geoData.coordinates[0][0][0] + " " + rawPlace.Cells.geoData.coordinates[0][0][1];
                    place.Description = rawPlace.Cells.Description + "\n" + rawPlace.Cells.EntranceAdditionalInformation;
                    place.Contacts = rawPlace.Cells.WebSite + rawPlace.Cells.PublicPhone;
                    place.Source = "https://data.mos.ru/";
                    place.Tags = tags;
                    place.DateTime = DateTime.Now; if (rawPlace.Cells.WorkingHours != null)
                        place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));
                    places.Add(place);
                }
            }
            else if (dataSetID == "1465")
            {
                try
                {
                    List<PlaceStrings<List<List<List<String>>>, String, String>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<List<List<List<String>>>, String, String>>>(objText);

                    foreach (var rawPlace in data)
                    {
                        Database.Place place = new Place();

                        place.Address = rawPlace.Cells.Address;
                        place.Title = rawPlace.Cells.FinalName;
                        if (rawPlace.Cells.geoData.coordinates == null)
                            continue;
                        place.Coordinates = rawPlace.Cells.geoData.coordinates[0][0][0][0] + " " + rawPlace.Cells.geoData.coordinates[0][0][0][1];
                        place.Description = rawPlace.Cells.Description + "\n" + rawPlace.Cells.EntranceAdditionalInformation;
                        place.Contacts = rawPlace.Cells.WebSite + rawPlace.Cells.PublicPhone;
                        place.Source = "https://data.mos.ru/";
                        place.Tags = tags;
                        place.DateTime = DateTime.Now; if (rawPlace.Cells.WorkingHours != null)
                            place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));
                        places.Add(place);
                    }
                }
                catch (Exception)
                {

                    List<PlaceStrings<List<List<String>>, String, String>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<List<List<String>>, String, String>>>(objText);

                    foreach (var rawPlace in data)
                    {
                        Database.Place place = new Place();

                        place.Address = rawPlace.Cells.Address;
                        place.Title = rawPlace.Cells.FinalName;
                        if (rawPlace.Cells.geoData.coordinates == null)
                            continue;
                        place.Coordinates = rawPlace.Cells.geoData.coordinates[0][0][0] + " " + rawPlace.Cells.geoData.coordinates[0][0][1];
                        place.Description = rawPlace.Cells.Description + "\n" + rawPlace.Cells.EntranceAdditionalInformation;
                        place.Contacts = rawPlace.Cells.WebSite + rawPlace.Cells.PublicPhone;
                        place.Source = "https://data.mos.ru/";
                        place.Tags = tags;
                        place.DateTime = DateTime.Now;
                        if (rawPlace.Cells.WorkingHours != null)
                            place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));
                        places.Add(place);
                    }
                }


            }

            else if (dataSetID == "2251")
            {
                List<PlaceStringsEvent> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStringsEvent>>(objText);
                foreach (var rawPlace in data)
                {
                    Database.Place place = new Place();

                    place.Address = rawPlace.Cells.Address;
                    place.Title = rawPlace.Cells.FinalName;
                    if (rawPlace.Cells.geoData.coordinates == null)
                        continue;
                    place.Coordinates = rawPlace.Cells.geoData.coordinates[0] + " " + rawPlace.Cells.geoData.coordinates[1];
                    place.Description = rawPlace.Cells.Description;
                    place.Source = "https://data.mos.ru/";
                    place.Limitations = rawPlace.Cells.EventDate + "\nНачало:" + rawPlace.Cells.StartTime;
                    place.Tags = tags;
                    place.DateTime = DateTime.Now;
                    places.Add(place);
                }
            }

            //else if (dataSetID == "2252")
            //{
            //    List<PlaceStrings<List<String>, String, String>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<List<String>, String, String>>>(objText);
            //    place.Address = rawPlace.Cells.Address;
            //    place.Title = rawPlace.Cells.FinalName;
            //    place.Coordinates = rawPlace.Cells.geoData.coordinates[0] + " "  + rawPlace.Cells.geoData.coordinates[1];
            //    place.Description = rawPlace.Cells.Description;
            //    place.Contacts = rawPlace.Cells.WebSite + rawPlace.Cells.PublicPhone;
            //    place.Source = "https://data.mos.ru/";
            //    place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));

            //}
            //else if (dataSetID == "2254")
            //{
            //    List<PlaceStrings<List<String>, String, String>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<List<String>, String, String>>>(objText);
            //    place.Address = rawPlace.Cells.Address;
            //    place.Title = rawPlace.Cells.FinalName;
            //    place.Coordinates = rawPlace.Cells.geoData.coordinates[0][0] + " "  + rawPlace.Cells.geoData.coordinates[0][1];
            //    place.Description = rawPlace.Cells.Description;
            //    place.Contacts = rawPlace.Cells.WebSite + rawPlace.Cells.PublicPhone;
            //    place.Source = "https://data.mos.ru/";
            //    place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));

            //}

            else
            {
                List<PlaceStrings<String, String, String>> data = /*serializer.Deserialize*/JsonConvert.DeserializeObject<List<PlaceStrings<String, String, String>>>(objText);

                foreach (var rawPlace in data)
                {
                    Database.Place place = new Place();

                    place.Address = rawPlace.Cells.Address;
                    place.Title = rawPlace.Cells.FinalName;
                    if (rawPlace.Cells.geoData.coordinates == null)
                        continue;
                    place.Coordinates = rawPlace.Cells.geoData.coordinates[0] + " " + rawPlace.Cells.geoData.coordinates[1];
                    place.Description = rawPlace.Cells.Description;
                    place.Contacts = rawPlace.Cells.WebSite + rawPlace.Cells.PublicPhone;
                    place.Source = "https://data.mos.ru/";
                    place.Tags = tags;
                    place.DateTime = DateTime.Now;
                    if (rawPlace.Cells.WorkingHours != null)
                        place.Limitations = String.Join('\n', rawPlace.Cells.WorkingHours.Select(a => a.ToString()));
                    places.Add(place);
                }
            }
            return places;
        }


        class Program
        {
            static void Main(string[] args)
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://apidata.mos.ru/v1/datasets/658/rows?$top=2");
                //// Set the Method property of the request to POST.
                //request.Method = "POST";
                //// Create POST data and convert it to a byte array.
                //string postData = "[\"TypeOfTransport\",\"global_id\"]";
                //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                //// Set the ContentType property of the WebRequest.
                //request.ContentType = "application/x-www-form-urlencoded";
                //// Set the ContentLength property of the WebRequest.
                //request.ContentLength = byteArray.Length;
                //// Get the request stream.
                //Stream dataStream = request.GetRequestStream();
                //// Write the data to the request stream.
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //// Close the Stream object.
                //dataStream.Close();

                DataPicker dp = new DataPicker("../resources/DataMosResources/APIKey.txt", "../resources/DataMosResources/DataMos_DataSet_IDs.txt");
                dp.GetData();

                //Console.Write(a);
            }
        }
    }
}
