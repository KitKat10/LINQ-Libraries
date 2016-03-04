using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Configuration;
using LinqToTwitterDemo;

namespace Twitter2LINQDemo
{
    class Program
    {
        static TwitterContext twitterCtx;

        static void Main(string[] args)
        {
            twitterCtx = new TwitterContext(DoSingleUserAuth());
            run();
        }

        static void run()
        {
            DoSearchAsync(twitterCtx).Wait();
            GetTrendsAsync(twitterCtx).Wait();
        }

        static async Task DoSearchAsync(TwitterContext twitterCtx)
        {
            string searchTerm = "#MakeDonaldDrumpfAgain";

            Search searchResponse =
                await
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                       search.Query == searchTerm
                 select search)
                 .Take(100)
                .SingleOrDefaultAsync();

            if (searchResponse != null && searchResponse.Statuses != null)
                searchResponse.Statuses
                    .ToList()
                    .ForEach(tweet =>
                    Console.WriteLine(
                        "\n  User: {0} ({1})\n  Tweet: {2}",
                        tweet.User.ScreenNameResponse,
                        tweet.User.UserIDResponse,
                        tweet.Text));
        }

        static async Task GetTrendsAsync(TwitterContext twitterCtx)
        {
            var trends =
                await twitterCtx.Trends
                .Where(trend => trend.Type == TrendType.Place &&
                       trend.WoeID == 1)
                .OrderByDescending(x => x.TweetVolume)
                .ToListAsync();

            trends.ForEach(x => Console.WriteLine("Trend title: {0}, volume: {1}", x.Name, x.TweetVolume));
        }

        static IAuthorizer DoSingleUserAuth()
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = AppRes.ConsumerKey,
                    ConsumerSecret = AppRes.ConsumerSecret,
                    AccessToken = AppRes.AccessToken,
                    AccessTokenSecret = AppRes.AccessTokenSecret
                }
            };

            return auth;
        }
    }
}
