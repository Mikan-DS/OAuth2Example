using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    public class Program
    {
        public const string authServerUrl = "https://localhost:7121/";

        static void Main(string[] args)
        {
            RunClient().Wait();
            Console.ReadLine();
        }

        public static async Task RunClient()
        {
            await CreateClientAsync();
            await CreateAdminUserAsync();
            string code = await Auth();
            string token = await GetToken(code);
            await AccessResourceServer(token);
        }

        public static async Task CreateClientAsync() {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, authServerUrl+"dev/modify_client");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("console_client"), "client_id");
            content.Add(new StringContent("cc.read cc.write cc.admin"), "scope");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        public static async Task CreateAdminUserAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, authServerUrl + "dev/modify_user");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("bossadmin"), "user_id");
            content.Add(new StringContent("test_cli.test test_cli.admin cc.read cc.write cc.admin"), "scope");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        public static async Task<string> Auth()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, authServerUrl + "oauth2/auth");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("console_client"), "client_id");
            content.Add(new StringContent("bossadmin"), "user_id");
            content.Add(new StringContent("code"), "response_type");
            content.Add(new StringContent("cc.read cc.write cc.admin"), "scope");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string authCode = await response.Content.ReadAsStringAsync();
            Console.WriteLine(authCode);

            return authCode;


        }

        public static async Task<string> GetToken(string authCode)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, authServerUrl + "oauth2/token");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("authorization_code"), "grant_type");
            content.Add(new StringContent(authCode), "code");
            content.Add(new StringContent("console_client"), "client_id");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string token = "Bearer " + await response.Content.ReadAsStringAsync();
            Console.WriteLine(token);

            return token;

        }

        public static async Task AccessResourceServer(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7150/");
            request.Headers.Add("Authorization", token);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }
    }
}
