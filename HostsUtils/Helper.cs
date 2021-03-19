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
        /// <summary>
        /// 更新指定hosts，并指定 域名列表
        /// </summary>
        /// <param name="hostfile"></param>
        /// <param name="hosts"></param>
        public static void UpdateHosts(string hostfile, string target, string[] hosts) {
            Hosts host = new HostsUtils.Hosts(hostfile);
            host.Read();
            host.UpdateHosts(QueryDNS(hosts));
            host.Save(target);
        }

        /// <summary>
        /// 更新有 #!标识的
        /// </summary>
        /// <param name="hostfile"></param>
        public static void UpdateHosts(string hostfile, string target)
        {
            Hosts host = new HostsUtils.Hosts(hostfile);
            host.Read();
            string[] hs = host.QueryTargetHosts();
            host.UpdateHosts(QueryDNS(hs));
            host.Save(target);
        }

        public static IDictionary<string, string[]> QueryDNS(string[] hosts) {
            IDictionary<string, string[]> list = new Dictionary<string, string[]>();
            foreach (var host in hosts)
            {
                string[] ips = HostsUtils.DNS.query(host);
                list.Add(host, ips);
            }
            return list;
        }
    }
}
