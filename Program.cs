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
    private const string Resource = "orgs/<your Org>/repos?page=1&per_page=200";

    static void Main(string[] args)
    {
      var client = new RestClient("https://api.github.com");    
      client.Authenticator = new HttpBasicAuthenticator(Username, Password);
     
      var request = new RestRequest(Resource, DataFormat.Json);
      var response = client.Get(request);
      var jsonListRepos =  response.Content;
      var jsonArray = JToken.Parse(jsonListRepos);
      IList<string> repo = new List<string>();
      foreach (var record in jsonArray)
      {
        var name = record["name"];
        repo.Add(name.ToString());
      }   
      repo.ToList().ForEach(m => Console.WriteLine(m));
      Console.WriteLine("Number of Repos :"+ repo.Count);
      Console.ReadLine();
    }
  }
}
