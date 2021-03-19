using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace hosts
{
    class Args {
        public bool Help = false;
        public bool View = false;
        public string Out = null;
        public string Path = null;
        public string[] Input = null;
        public Args(string[] args) {
            int i = 0;
            int maxi = args.Length - 1;
            while (i < args.Length) {
                switch (args[i]) {
                    case "-h":
                    case "--help":
                    case "help":
                        this.Help = true;
                        break;
                    case "-v":
                    case "--view":
                        this.View = true;
                        break;
                    case "-o":
                    case "--out":
                        this.Out = args[++i];
                        break;
                    case "-p":
                    case "--path":
                        this.Path = args[++i];
                        break;
                    case "-i":
                    case "--input":
                        this.Input = args[++i].Split(",").Where(item=>item!="").ToArray();
                        break;
                }
                i++;
            }
        }
        public string[] getHelp() {
            return new string[]{
                "用法：hosts [-p path]",
                "选项：",
                "    -p path    HOSTS文件地址，默认为系统目录下HOSTS文件",
                "    -v view    打开HOSTS文件",
                "    -o out     另存到.."
            };
        }
    }
    class Program
    {
        static void print(string[] list) {
            list.ToList().ForEach(line => Console.WriteLine(line));
        }
        static void Main(string[] args)
        {
            Args Pars = new Args(args);

            if (Pars.Help) {

                print(Pars.getHelp());
                return;
            }

            string path;
            if (Pars.Path != null)
            {
                path = Pars.Path;
            }
            else
            {
                path = HostsUtils.Hosts.getWinHostsFilePath();
            }


            if (Pars.View)
            {
                Process.Start("explorer", path);
                return;
            }

            Console.WriteLine("源文件地址：" + path);
            string outpath = path;
            if (Pars.Out!=null)
            {
                outpath = Pars.Out;
                Console.WriteLine("另存到：" + outpath);
            }
            HostsUtils.Helper.UpdateHosts(path, outpath);

            Console.WriteLine("更新完成");
        }
    }
}
