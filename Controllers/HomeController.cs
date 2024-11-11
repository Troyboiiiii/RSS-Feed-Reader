using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Models;
using System.Collections.Generic;
using CodeHollow.FeedReader; // Required for FeedItem
using System.Threading.Tasks; // Required for async/await


namespace RSSFeedReader.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RssFeedService _rssFeedService;

    public HomeController(ILogger<HomeController> logger, RssFeedService rssFeedService)
    {
        _logger = logger;
        _rssFeedService = rssFeedService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string rssUrl){
        if (string.IsNullOrEmpty(rssUrl)){
            ViewBag.Error = "Please enter a valid RSS URL.";
            return View();
        }

        try {
            List<FeedItem> feedItems = await _rssFeedService.ParseFeedAsync(rssUrl);
            return View("FeedResult", feedItems);
        } catch (Exception ex){
            ViewBag.Error = $"Failed to load the RSS feed: {ex.Message}";
            return View();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
