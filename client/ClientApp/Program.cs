using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        public const string BaseUrl = "https://localhost:44301";

        static void Main(string[] args)
        {
            string token = UserLogin().Result;
            if (token == null)
            {
                return;
            }

            var basketItemModel1 = new BasketItemCreateViewModel
            {
                ProductId = 4,
                Quantity = 3,
                PriceInPounds = 5
            };

            var basketItemModel2 = new BasketItemCreateViewModel
            {
                ProductId = 8,
                Quantity = 1,
                PriceInPounds = 2
            };

            AddItemToBasket(token, basketItemModel1).Wait();
            int basketItemId = AddItemToBasket(token, basketItemModel2).Result;

            ChangeQuantity(token, basketItemId, 6).Wait();

            GetBasket(token).Wait();

            DeleteBasketItem(token, 1).Wait();

            GetBasket(token).Wait();

            ClearAllItems(token).Wait();

            GetBasket(token).Wait();

            Console.ReadLine();
        }

        public static async Task<string> UserLogin()
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("User login");

                var loginModel = new LoginViewModel
                {
                    Email = "marian_test@yahoo.com",
                    Password = "Pass@word1"
                };

                client.BaseAddress = new Uri($"{BaseUrl}/api/account/token");
                HttpResponseMessage response = await client.PostAsJsonAsync("", loginModel);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("An error has occurred");
                    return null;
                }

                string result = await response.Content.ReadAsStringAsync();
                var objResult = JObject.Parse(result);
                return objResult["token"].Value<string>();
            }
        }

        public static async Task<int> AddItemToBasket(string token, BasketItemCreateViewModel basketItemModel)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Adding item to basket");

                client.BaseAddress = new Uri($"{BaseUrl}/api/basket/items");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.PostAsJsonAsync("", basketItemModel);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("An error has occurred");
                    return 0;
                }

                string result = await response.Content.ReadAsStringAsync();
                var objResult = JObject.Parse(result);
                return objResult["id"].Value<int>();
            }
        }

        public static async Task GetBasket(string token)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Displaying basket");

                client.BaseAddress = new Uri($"{BaseUrl}/api/basket");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync("");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("An error has occurred");
                    return;
                }

                string result = await response.Content.ReadAsStringAsync();
                var objResult = JObject.Parse(result);
                Console.WriteLine(objResult.ToString());
            }
        }

        public static async Task ChangeQuantity(string token, int basketItemId, int quantity)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine($"Changing quantity for basket item with id = {basketItemId}");

                client.BaseAddress = new Uri($"{BaseUrl}/api/basket/items/{basketItemId}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PutAsJsonAsync("", quantity);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("An error has occurred");
                    return;
                }
            }
        }

        public static async Task DeleteBasketItem(string token, int basketItemId)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Deleting item from basket");

                client.BaseAddress = new Uri($"{BaseUrl}/api/basket/items/{basketItemId}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.DeleteAsync("");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("An error has occurred");
                    return;
                }
            }
        }

        public static async Task ClearAllItems(string token)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Clear all items");

                client.BaseAddress = new Uri($"{BaseUrl}/api/basket/items");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.DeleteAsync("");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("An error has occurred");
                    return;
                }
            }
        }
    }
}
