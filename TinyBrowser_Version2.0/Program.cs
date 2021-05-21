using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TinyBrowser.Browsers;
using TinyBrowser.Interfaces;

namespace TinyBrowser{
    internal class Program{

        const string host = "acme.com";
        const int port = 80;
        static IBrowser browser;
        static void Main(string[] args){

            browser = GetBrowser();
            
            if (!browser.CanReceiveWebPage(host, string.Empty, port)){
                Console.WriteLine("Closing browser...");
            }

            do{
                Console.Clear();
                Console.WriteLine(browser.GetCurrentWebPageUri);
                var uriPages = browser.GetCurrentWebPage();
                var optionsMenu = new[]{
                    "\nOPTIONS: ", 
                    $"1. Show links ({browser.WebPageHtmlCount})",
                    $"2. Show sub-pages ({uriPages.Length})", 
                    "3. Forward", 
                    "4. Back",
                    "5. History", 
                    "6. Exit"
                };
                
                var input = NavigationOptions(optionsMenu);
                Console.Clear();
                if (input == 6){
                    break;
                }
                Run(input, uriPages);
            } while (true);
            
            Console.Clear();
            Console.WriteLine("Closing...");
        }
        static void Run(int input, IReadOnlyList<string> uriPages){
            switch (input){
                    case 1:
                        SelectHyperLink();
                        break;
                    case 2:
                        SelectSubPage(uriPages);
                        break;
                    case 3:
                        if (!browser.CanGoForward()){
                            Console.WriteLine("Can't go forward!");
                        }
                        break;
                    case 4:
                        if (!browser.CanGoBack()){
                            Console.WriteLine("Can't go back!");
                        }
                        break;
                    case 5:
                        browser.GetSearchHistory();
                        Console.WriteLine("Press enter to continue!");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
            }
        }

        static void SelectHyperLink(){
            if (browser.WebPageHtmlCount == 0){
                Console.WriteLine("No links available");
                return;
            }
            browser.DisplayHyperLinks();
            Console.WriteLine($"Select link between 0 and {browser.WebPageHtmlCount - 1}");
            var indexInput = GetInput(0, browser.WebPageHtmlCount - 1);
            browser.TryGoToHtmlIndex(indexInput, port);
            Console.Clear();
        }

        static void SelectSubPage(IReadOnlyList<string> uriPages){
            if (uriPages.Count == 0){
                Console.WriteLine("No sub-pages available");
                return;
            }
            browser.DisplaySubPages();
            Console.WriteLine($"Select sub-page between 0 and {uriPages.Count - 1}");
            var linkOption2 = GetInput(0, uriPages.Count - 1);
            browser.CanGoToSubPage(uriPages[linkOption2]);
            Console.Clear();
        }

        static IBrowser GetBrowser(){
            while (true){
                try{
                    Console.WriteLine("\nWelcome to Tiny Browser");
                    Console.WriteLine("\nChoose a browser: \n1. TcpClient, \n2. Local, \n3. HttpWebRequest");
                    var input = GetInput(1, BrowserSetup.options);
                    Console.Clear();
                    return BrowserSetup.GetBrowser(input);
                }
                catch (Exception e){
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
        }

        static int GetInput(int startIndex, int maxLenght){
            while (true){
                if (int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out var result) &&
                    result == Math.Clamp(result, startIndex, maxLenght)){
                    return result;
                }
                Console.WriteLine($"Only numbers between {startIndex} and {maxLenght} is allowed");
            }
        }

        static int NavigationOptions(IReadOnlyCollection<string> options){
            var temp = options.Aggregate(string.Empty, (current, option) => current + $"{option}\n");
            Console.WriteLine(temp);
            var inputValue = GetInput(1, options.Count - 1);
            Console.Clear();
            return inputValue;
        }
    }
}