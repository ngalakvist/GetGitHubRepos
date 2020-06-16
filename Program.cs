using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetGitHubRepos
{
  class Program
  {
    static void Main(string[] args)
    {
      var client = new RestClient("https://api.github.com");
      client.Authenticator = new HttpBasicAuthenticator("ngalakvist", "r7dyAaHyYG38Jmt");
     
      var request = new RestRequest("orgs/mdh-se/repos?page=1&per_page=200", DataFormat.Json);
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
      Console.WriteLine("Antal :"+ repo.Count);
      Console.ReadLine();
    }
  }
}
