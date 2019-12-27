using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HtmlAgilityPack;
using SeoAnalysis2.Models;

namespace SeoAnalysis2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckURL(HomeModel model)
        { 
            if (ModelState.IsValid)
            {
                try
                {
                    //find out the URL existing with SYSTEM.NET library function
                    var req = (HttpWebRequest)WebRequest.Create(model.URL);
                    var res = (HttpWebResponse)req.GetResponse();
                    var client = new WebClient();
                    var stopWords = new List<string> { "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your"};
                   
                    //check whether it is valid website or not
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        var outWords = new List<string>();
                        var web = new HtmlWeb();
                        var doc = web.Load(model.URL);
                        var text = doc.DocumentNode.Descendants()
                                  .Where(x => x.NodeType == HtmlNodeType.Text && x.InnerText.Trim().Length > 0)
                                  .Select(x => x.InnerText.Trim());

                        var urlContent = String.Join(" ", text);
                        
                        //loop for mach with stop words
                        foreach (var words in stopWords)
                        {
                            if (urlContent.IndexOf(words) > -1)
                            {
                                outWords.Add(words);
                            }
                        }

                        model.words = outWords;

                        model.Exist = true;
                    }
                    else
                    {
                        model.Exist = false;
                    }
                }
                catch (Exception)
                {
                    model.Exist = false;
                }
            }
            return View("Index", model);
        }
    }
}