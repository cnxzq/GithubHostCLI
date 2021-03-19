using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace HostsUtils
{
    public static class Helper
    {
        public static void flushdns()
        {
            HostsUtils.DNS.flushdns();
        }
        /// <summary>
        /// 更新指定hosts，并指定 域名列表
        /// </summary>
        /// <param name="hostfile"></param>
        /// <param name="hosts"></param>
        public static void UpdateHosts(string hostfile, string target, string[] hosts) {
            Hosts host = new HostsUtils.Hosts(hostfile);
            host.Read();
            string[] hs = host.QueryTargetHosts().Union(hosts).ToArray();
            host.UpdateHosts(QueryDNS(hs));
            host.Save(target);
        }
        public static void UpdateHosts(string hostfile, string target)
        {
            UpdateHosts(hostfile, target, new string[] { });
        }
        public static void UpdateHosts(string hostfile){
            UpdateHosts(hostfile, hostfile);
        }
        public static void UpdateHosts()
        {
            string hostfile = HostsUtils.Hosts.getWinHostsFilePath();
            UpdateHosts(hostfile);
        }


        public static IDictionary<string, string[]> QueryDNS(string[] hosts) {
            IDictionary<string, string[]> list = new Dictionary<string, string[]>();
            string[] hosts2 = hosts.Distinct().ToArray();
            foreach (var host in hosts2)
            {
                string[] ips = HostsUtils.DNS.query(host);
                list.Add(host, ips);
            }
            return list;
        }
    }
}
