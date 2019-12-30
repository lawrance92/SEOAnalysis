using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HtmlAgilityPack; //Have install this nugget in project for filter out html metatag
using SeoAnalysis2.Models;

namespace SeoAnalysis2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Return view for the web page from controller
            return View();
        }
        [HttpPost]
        public ActionResult CheckURL(HomeModel model)
        { 
            //Check whether the model is in use 
            if (ModelState.IsValid)
            {
                // Try to check the URL because some if the URL is not in use it will throw error or exception
                try
                {
                    //find out the URL existing with SYSTEM.NET library function
                    var req = (HttpWebRequest)WebRequest.Create(model.URL); //Request web information from the web server
                    var res = (HttpWebResponse)req.GetResponse(); //Get the response from web service
                    var stopWords = new List<string> { "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your"}; // Assign string list for stop words
                   
                    //check whether it is valid website or not
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        var outWords = new List<string>(); //New list string for store the similar string from stop-words
                        var web = new HtmlWeb(); // Connect the HTMLAgilityPack for get the content on website URL
                        var doc = web.Load(model.URL); //Load the websites
                        var text = doc.DocumentNode.Descendants()
                                  .Where(x => x.NodeType == HtmlNodeType.Text && x.InnerText.Trim().Length > 0)
                                  .Select(x => x.InnerText.Trim()); //filter out the DOM document from website URL

                        var urlContent = String.Join(" ", text); // join the string with spaces
                        
                        //loop for mach with stop words
                        foreach (var words in stopWords)
                        {
                            // Condition for check the string match with stop-words or not
                            if (urlContent.IndexOf(words) > -1)
                            {
                                outWords.Add(words);
                            }
                        }

                        // Assign to model list field for view purposes
                        model.words = outWords;

                        // Final will show that the website is in use
                        model.Exist = true;
                    }
                    else
                    {
                        // It will identify the website is invalid
                        model.Exist = false;
                    }
                }
                catch (Exception)
                {
                    // It will identify the website is invalid
                    model.Exist = false;
                }
            }

            // return the model to the view
            return View("Index", model);
        }
    }
}