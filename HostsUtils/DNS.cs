using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HostsUtils
{
    public static class DNS
    {

        public static string[] query(string host)
        {
            string postString = "host="+ host;
            byte[] PostData = Encoding.UTF8.GetBytes(postString);
            string url = "https://www.ipaddress.com/search/";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = webClient.UploadData(url, "POST", PostData);
            string srcString = Encoding.UTF8.GetString(responseData);
            return queryFromHtml(srcString);
        }

        static string[] queryFromHtml(string content)
        {
            string regex = "<ul class=\"comma-separated\">(.*?)</ul>";
            string[] lis = search(content, regex);
            string[] ips;
            List<string> list = new List<string>();
            Array.ForEach(lis, li =>
            {
                ips = search(li, "<li>([0-9|.]*?)</li>");
                Array.ForEach(ips, ip =>
                {
                    if (!list.Contains(ip))
                    {
                        list.Add(ip);
                    }
                });
            });
            return list.ToArray();
        }

        public static string[] search(string content, string strReg) {
            Regex re = new Regex(strReg);
            MatchCollection matches = re.Matches(content);
            System.Collections.IEnumerator enu = matches.GetEnumerator();
            List<string> list = new List<string>();
            while (enu.MoveNext() && enu.Current != null)
            {
                Match match = (Match)(enu.Current);
                list.Add(match.Groups[1].Value);
            }
            return list.ToArray();
        }
    }
}
