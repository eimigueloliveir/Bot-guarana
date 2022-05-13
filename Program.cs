using Tweetinvi;
using Tweetinvi.Models.V2;

string[] config = File.ReadAllLines("/home/ubuntu/github/config/bot-guarana.config");
//Line 0 of file contains CONSUMER_KEY
//Line 1 of file contains CONSUMER_SECRET
//Line 2 of file contains ACCESS_TOKEN
//Line 3 of file contains ACCESS_TOKEN_SECRET
TwitterClient client = new(config[0], config[1], config[2], config[3]);
Console.WriteLine("Starting...");
while (true)
{
    try
    {
        SearchTweetsV2Response search = await client.SearchV2.SearchTweetsAsync("guarana");
        TweetV2[] tweet = search.Tweets;
        for (int i = 0; i < tweet.Length; i++)
        {
            if (tweet[i].Text.Contains("guarana") || tweet[i].Text.Contains("guaraná"))
            {
                await client.Tweets.PublishRetweetAsync(long.Parse(tweet[i].Id));
                Console.WriteLine($"{i}-{tweet[i].Id} Retweeted");
                await client.Tweets.FavoriteTweetAsync(long.Parse(tweet[i].Id));
                Console.WriteLine($"{i}-{tweet[i].Id} Favorited");
                Thread.Sleep(36000);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}