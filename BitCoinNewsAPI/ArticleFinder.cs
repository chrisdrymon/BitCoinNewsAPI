using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using System.Threading;

namespace BitCoinNewsAPI
{
    class ArticleFinder
    {
        static void Main(string[] args)
        {
            // init with your API key
            var newsApiClient = new NewsApiClient("81c072dd1ec540069da4d4069ce99f39");
            int hitsperpage = 80;
            int i = 2;
            int j = 1;
            int tot_arts = 300;
            while (i <= tot_arts/hitsperpage)
            {
                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = "Bitcoin",
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    From = new DateTime(2020, 4, 1),
                    PageSize = hitsperpage,
                    Page = i
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    // total results found
                    tot_arts = articlesResponse.TotalResults;
                    int start = (i - 1) * 100 + 1;
                    int end = start + 99;
                    Console.WriteLine(String.Format("Articles {0}-{1} of {2}", start, end, tot_arts));
                    foreach (var article in articlesResponse.Articles)
                    {
                        // title
                        Console.WriteLine(j.ToString(), article.Title);
                        // url
                        Console.WriteLine(article.Url);
                        // published at
                        Console.WriteLine(article.PublishedAt);

                        if (article.Author != null && article.Description != null && article.PublishedAt != null && article.Title != null)
                        {
                            // Convert to unixtime
                            long unixtime = ((DateTimeOffset)article.PublishedAt).ToUnixTimeSeconds();
                            DataAccess.InsertNews((DateTime)article.PublishedAt, unixtime, article.Url, article.Title, article.Author, article.Description);
                            j++;
                        }
                    }
                }
                Thread.Sleep(3000);
                i++;
            }
            Console.ReadLine();
        }
    }
}