using AngleSharp.Parser.Html;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Linq;
using System;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            getQuestions();
        }

        static void getQuestions()
        {
            var uri = "http://www.stackoverflow.com/";
            string content = null;
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                // Or you can get the file content without saving it:
                content = client.DownloadString(uri);
                //...
            }

            var parser = new HtmlParser();
            var document = parser.Parse(content);

            Console.WriteLine("-----All questions-----");
            var questions = document.All
                .Where(q => q.ClassName == "question-hyperlink")
                .ToList();

            foreach (var q in questions)
            {
                Console.WriteLine(q.InnerHtml);
            }

            Console.WriteLine("-----End all questions-----");

            Console.WriteLine("-----Questions with tags-----");

            var questionsAndTags =
                document.All
                .Where(q => q.ClassName == "summary")
                .Take(10)
                .ToList();
                
            questionsAndTags.ForEach(x => printQuestionWithTags(x));
            Console.WriteLine("-----End uestions with tags-----");

            printQuestionWithAnswers(document);

        }

        static void printQuestionWithAnswers(IHtmlDocument document)
        {
            var questions =
                document.All
                .Where(x => x.ClassName == "question-summary narrow")
                .Select(x => new {
                    question = x.GetElementsByClassName("question-hyperlink").First().InnerHtml,
                    numberOfAnswers = int.Parse(x.GetElementsByClassName("mini-counts").First().ChildNodes.First().TextContent)
                })
                .Where(x => x.numberOfAnswers > 0)
                .OrderByDescending(x => x.numberOfAnswers)
                .ToList();

            questions.ForEach(x => Console.WriteLine("qustion:{0}\n answers:{1}", x.question, x.numberOfAnswers));

        }

        static void printQuestionWithTags(IElement q)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine(q.GetElementsByClassName("question-hyperlink").First().InnerHtml);

            var title = q.GetElementsByClassName("question-hyperlink")
                .First()
                .GetAttribute("title");
            Console.WriteLine();
            Console.WriteLine("\nTitle: {0}\n", title);

            q.GetElementsByClassName("post-tag")
                .OrderBy(x => x.InnerHtml)
                .ToList()
                .ForEach(x => Console.WriteLine("TAGS: " + x.InnerHtml));
            Console.WriteLine("-----------------------");
        }
    }
}
