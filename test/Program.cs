using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HostsUtils;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string[] list = "xxxx.github.com github.com".Split(" ");
            string[] keys = new string[] { "github.com","observablehq.com"};

            string[] newlist = list.Where(line => {
                return keys.Any(key =>
                    {
                        return line.Contains(key) && !line.Contains("." + key);
                });
            }).ToArray();

            Console.WriteLine(newlist);
            */


            Helper.UpdateHosts(Hosts.getWinHostsFilePath(), @"D:\host2.txt");
            Helper.UpdateHosts(@"D:\host1.txt", @"D:\host2.txt");
            //HostsUtils.Helper.UpdateHosts("D:\\HOSTS");
            //HostsUtils.Helper.UpdateHosts("D:\\HOSTS", new string[] { "github.com", "observablehq.com" });
        }

        public static void test(){
            string postString = "host=gist.github.com";
            byte[] PostData = Encoding.UTF8.GetBytes(postString);
            string url = "https://www.ipaddress.com/search/";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = webClient.UploadData(url,"POST",PostData);
            string srcString = Encoding.UTF8.GetString(responseData);
            Console.WriteLine(srcString);
        }

    }
}
