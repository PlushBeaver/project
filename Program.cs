using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            //string url = "http://www.novtex.ru/IT/it2011/number_02_annot.html#2";
            //string url = "http://www.jitcs.ru/index.php?option=com_content&view=article&id=456";
            //string url = "http://aidt.ru/index.php?option=com_content&view=article&id=366";
            //string url = "http://www.ipiran.ru/journal/issues/2012_06_03/annot.asp";
            string url = "http://ubs.mtas.ru/search/search_results_ubs_new.php?publication_id=17812&IBLOCK_ID=20";
            string str = "";
            str = SearchEncoding(url);
            HtmlWeb web = new HtmlWeb();
            if (str == "utf-8")
                web.OverrideEncoding = Encoding.UTF8;
            else
                web.OverrideEncoding = Encoding.Default;
            StringBuilder builder = new StringBuilder();
            //string url = "http://www.novtex.ru/IT/it2011/number_02_annot.html#2";
            //string url = "http://www.jitcs.ru/index.php?option=com_content&view=article&id=456";
            HtmlAgilityPack.HtmlDocument document = web.Load(url);
                //Список всех строк
                HtmlNodeCollection trList = document.DocumentNode.SelectNodes("//tr");
                //Теперь для каждой строки tr, получаем все столбцы td
                foreach (var tr in trList)
                {
                    var tdList = tr.ChildNodes.Where(x => x.Name == "td");
                        foreach (var p in tdList)
                        {
                            var pList = p.ChildNodes.Where(x => (x.Name == "p") || (x.Name == "h3"));
                            {
                                foreach (var p1 in pList)
                                {
                                    {
                                        Console.WriteLine(p1.InnerText);
                                        Console.WriteLine('\n');
                                        builder.Append(p1.InnerText).Append('\n');
                                    }
                                }
                            }
                        }
                        foreach (var div in tdList)
                        {/*DocumentNode.SelectNodes("//div[contains(@class,'listevent')]")*/
                            var divList = div.SelectNodes("//div[contains(@class,'attr')]"/*ChildNodes.Where(x => (x.Name == "div") /*&& (x.Attributes == "attr")*/);
                            {
                                foreach (var divs in divList)
                                {
                                        //var divsList = divs.Attributes["attr"].Value; ;
                                        {
                                            var s = divs.InnerText.TrimStart().TrimEnd();
                                            Console.WriteLine(s/*divsList*//*divs.InnerText*/);
                                            Console.WriteLine('\n');
                                            builder.Append(s/*divsList*//*divs.InnerText*/).Append(' ').Append('\n');
                                        }
                                }
                            }
                        }
                }
                File.WriteAllText("test.txt", builder.ToString());
                Console.ReadLine();
            
        }

        public static string SearchEncoding(string url)
        {

            HtmlWeb web1 = new HtmlWeb();
            web1.AutoDetectEncoding = false;
            HtmlAgilityPack.HtmlDocument document1 = web1.Load(url);
            HtmlNodeCollection headList = document1.DocumentNode.SelectNodes("//meta");
            string str = "";
            string s = "";
            foreach (var headLists in headList)
            {
                s = headLists.WriteTo();
                int strLI = s.LastIndexOf("charset");
                if (strLI != -1)
                {
                    string LI = s.Substring(strLI);
                    str = LI.Replace(" ", string.Empty);
                    str = str.Replace("charset=", string.Empty);
                    try
                    {
                        str = str.Replace("\">", string.Empty);
                    }
                    finally
                    {
                        str = str.Replace("\" />", string.Empty);
                    }
                    Console.WriteLine(str);
                }
            }
            return str;
        }
    }
}
