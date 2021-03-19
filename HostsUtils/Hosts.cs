using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostsUtils
{
    public class Hosts
    {
        public static string getWinHostsFilePath() {
            return Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\drivers\etc\HOSTS";
        }
        private string HostsFilePath = "";

        private string[] Lines;

        public Hosts()
        {
            this.HostsFilePath = getWinHostsFilePath();
        }
        public Hosts(string hf) {
            this.HostsFilePath = hf;
        }
        /// <summary>
        /// 打开并读取Hosts文件
        /// </summary>
        public void Read() {
            FileAttributes fas = unlockFile(this.HostsFilePath);
            this.Lines = File.ReadAllLines(this.HostsFilePath);
            relockFile(this.HostsFilePath,fas);
        }

        public void Read(string filepath)
        {
            FileAttributes fas = unlockFile(filepath);
            this.Lines = File.ReadAllLines(filepath);
            relockFile(filepath, fas);
        }

        public FileAttributes unlockFile (string filepath) {
            FileAttributes fas = File.GetAttributes(filepath);
            if (fas.HasFlag(FileAttributes.ReadOnly))
            {
                File.SetAttributes(filepath, fas & (~FileAttributes.ReadOnly));//取消只读
            }
            return fas;
        }
        public FileAttributes relockFile(string filepath, FileAttributes fas)
        {
            File.SetAttributes(filepath,fas);
            return fas;
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        public void Save(string filepath)
        {
            if (File.Exists(filepath))
            {
                FileAttributes fas = unlockFile(filepath);
                File.WriteAllLines(filepath, this.Lines, Encoding.UTF8);
                relockFile(filepath, fas);
            }
            else
            {
                File.WriteAllLines(filepath, this.Lines, Encoding.UTF8);
            }
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        public void Save()
        {
            FileAttributes fas = unlockFile(this.HostsFilePath);
            File.WriteAllLines(this.HostsFilePath, this.Lines);
            relockFile(this.HostsFilePath, fas);
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="list"></param>
        public void UpdateHosts(IDictionary<string, string[]> list) {
            string[] keys = list.Keys.ToArray();
            IList<string> newlines = this.Lines.Where(line => line.StartsWith("#") || !keys.Any(key => line.Contains(key) && !line.Contains("." + key))).ToList();
            foreach (var item in list)
            {
                Array.ForEach(item.Value, ip =>
                {
                    newlines.Add(item.Key + " " + ip);
                });
            }
            this.Lines = newlines.ToArray();
        }

        /// <summary>
        /// 返回所有 #! 开头的行
        /// </summary>
        /// <param name="list">字符串数组</param>
        /// <returns>返回host列表</returns>
        public string[] QueryTargetHosts() {
            return this.queryTargetHosts(this.Lines);
        }
        public string[] QueryTargetHosts(string[] line)
        {
            return this.queryTargetHosts(line);
        }

        string[] queryTargetHosts(string[] list)
        {
            return list.Where(line => line.StartsWith("#!")).Select(line => line.Substring(2).Trim()).ToArray();
        }
    }
}
