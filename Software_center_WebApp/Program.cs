using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();
        var url = "https://5d80b2ae99f8a20014cf977a.mockapi.io/api/v1/Users";
        string table = "id    createdAt          Name\n";
        var client = new HttpClient();
        var res = await client.GetFromJsonAsync<List<Data>>(url);
        app.Run(async (context) =>
        {
            if (res != null)
            {
                foreach (var item in res)
                {
                    table += item.ToString() + '\n';
                }
            }
            await context.Response.WriteAsync(table);
        });
        app.Run();
    }
    public class Data
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }


        public override string ToString()
        {
            return $"{Id};{CreatedAt};{Name}";
        }
    }
}