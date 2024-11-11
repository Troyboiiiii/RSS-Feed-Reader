using CodeHollow.FeedReader;
using System.Collections.Generic;
using System.Linq; //Required for .ToList()
using System.Threading.Tasks; // Required for async/await

public class RssFeedService
{
    public async Task<List<FeedItem>> ParseFeedAsync(string url)
    {
        try
        {
            var feed = await FeedReader.ReadAsync(url); // Asynchronous fetch and parse
            return feed.Items.ToList(); // Convert to List<FeedItem>
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to parse the RSS feed: {ex.Message}");
        }
    }
}