using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetGitHubRepos
{
  class Program
  { 
    //Enter your Github Login Vairables
    private const string Username = "";
    private const string Password = "";

    static void Main(string[] args)
    {
      var client = new RestClient("https://api.github.com");
      client.Authenticator = new HttpBasicAuthenticator(Username, Password);
      IList<string> repos = new List<string>();
      for (int i = 1; i < 4; i++)
      {
        var request = new RestRequest("orgs/mdh-se/repos?page=" + i + "&per_page=200", DataFormat.Json);
        var response = client.Get(request);
        var jsonListRepos = response.Content;
        var jsonArray = JToken.Parse(jsonListRepos);
       
        foreach (var record in jsonArray)
        {
          var name = record["name"];
          repos.Add(name.ToString());
        }
        Console.WriteLine("Number of Repos :" + repos.Count);
      }
      // Kolla i bin folder för att hitta filen
      System.IO.File.WriteAllLines("savedGitRepos.txt", repos);
      repos.ToList().ForEach(m => Console.WriteLine(m));
      Console.WriteLine("Number of Repos :" + repos.Count);
      Console.ReadLine();
    }
  }
}
