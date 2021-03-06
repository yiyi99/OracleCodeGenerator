﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace OracleCodeGenerator
{
    public class CreateVo
    {
        //不含通知机制
        private static string pathVo1 = @"Dis/VoWithoutNotify";
        //包含通知机制
        private static string pathVo2 = @"Dis/VoWithNotify";
        //通知类文件
        private static string path = @"Dis";

        /// <summary>
        /// 判断是否存在文件夹，不存在则创建
        /// </summary>
        /// <returns></returns>
        public static bool CreateDirectory()
        {
            if (!Directory.Exists(pathVo1))
            {
                Directory.CreateDirectory(pathVo1);
            }
            if (!Directory.Exists(pathVo2))
            {
                Directory.CreateDirectory(pathVo2);
            }

            if (Directory.Exists(pathVo1) && Directory.Exists(pathVo2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 生成通知类文件
        /// </summary>
        /// <param name="NameSpace"></param>
        public static void CreateRaisePropertyChangedCS(string NameSpace)
        {
            try
            {
                FileStream fs = new FileStream(path + "/ObjectNotifyPropertyChanged.cs", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(CreateRaisePropertyChanged(NameSpace));
                sw.Flush();
                sw.Close();
                fs.Close();
                MessageBox.Show("ObjectNotifyPropertyChanged.cs文件生成成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 生成不含通知机制的VO
        /// </summary>
        /// <param name="NameSpace">命名空间</param>
        /// <param name="name">文件名</param>
        /// <param name="listName">列表</param>
        public static void CreateVoNoINotifyPropertyChanged(string NameSpace,string name,List<TableVo> listName)
        {
            try
            {
                FileStream fs = new FileStream(pathVo1 + "/" + name + ".cs", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(CreateCSNoINotifyPropertyChanged(NameSpace, name, listName));
                sw.Flush();
                sw.Close();
                fs.Close();
                MessageBox.Show(name + ".cs文件生成成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 生成包含通知机制的VO
        /// </summary>
        /// <param name="NameSpace">命名空间</param>
        /// <param name="name">文件名</param>
        /// <param name="listName">列表</param>
        public static void CreateVoWithINotifyPropertyChanged(string NameSpace, string name, List<TableVo> listName)
        {
            try
            {
                FileStream fs = new FileStream(pathVo2 + "/" + name + ".cs", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(CreateCSWithINotifyPropertyChanged(NameSpace, name, listName));
                sw.Flush();
                sw.Close();
                fs.Close();
                MessageBox.Show(name + ".cs文件生成成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        /// <summary>
        /// 创建不含通知机制的CS文件
        /// </summary>
        /// <param name="NameSpace">命名空间</param>
        /// <param name="name">文件名</param>
        /// <param name="listName">列表</param>
        /// <returns></returns>
        private static string CreateCSNoINotifyPropertyChanged(string NameSpace,string name,List<TableVo> listName)
        {
            string content = "";
            content += "using System;\r\n";
            content += "using System.Collections.Generic;\r\n";
            content += "using System.Linq;\r\n";
            content += "using System.Text;\r\n";
            content += "\r\n";
            content += "namespace " + "" + NameSpace + "" + "\r\n";
            content += "{\r\n";
            content += "    public class " + "" + name + "" + "\r\n";
            content += "    {\r\n";
            foreach(TableVo s in listName)
            {
                content += "        private string " + s.Name.ToLower() + ";" + "\r\n";
                content += "        /// <summary>\r\n";
                content += "        /// " + s.Comments + "\r\n";
                content += "        /// </summary>\r\n";
                content += "        public string " + s.Name.Substring(0, 1) + s.Name.Substring(1, s.Name.Length-1).ToLower() + "\r\n";
                content += "        {\r\n";
                content += "            get\r\n";
                content += "            {\r\n";
                content += "                return " + s.Name.ToLower() + ";\r\n";
                content += "            }\r\n";
                content += "\r\n";
                content += "            set\r\n";
                content += "            {\r\n";
                content += "                " + s.Name.ToLower() + " = value;\r\n";
                content += "            }\r\n";
                content += "        }\r\n";
                content += "\r\n";
            }
            content += "    }\r\n";
            content += "}\r\n";
            return content;
        }
        /// <summary>
        /// 创建包含通知机制的CS文件
        /// </summary>
        /// <param name="NameSpace">命名空间</param>
        /// <param name="name">文件名</param>
        /// <param name="listName">列表</param>
        /// <returns></returns>
        private static string CreateCSWithINotifyPropertyChanged(string NameSpace, string name, List<TableVo> listName)
        {
            string content = "";
            content += "using System;\r\n";
            content += "using System.Collections.Generic;\r\n";
            content += "using System.Linq;\r\n";
            content += "using System.Text;\r\n";
            content += "\r\n";
            content += "namespace " + "" + NameSpace + "" + "\r\n";
            content += "{\r\n";
            content += "    public class " + "" + name + "" + ":ObjectNotifyPropertyChanged" + "\r\n";
            content += "    {\r\n";
            foreach (TableVo s in listName)
            {
                content += "        private string " + s.Name.ToLower() + ";" + "\r\n";
                content += "        /// <summary>\r\n";
                content += "        /// " + s.Comments + "\r\n";
                content += "        /// </summary>\r\n";
                content += "        public string " + s.Name.Substring(0, 1) + s.Name.Substring(1, s.Name.Length - 1).ToLower() + "\r\n";
                content += "        {\r\n";
                content += "            get\r\n";
                content += "            {\r\n";
                content += "                return " + s.Name.ToLower() + ";\r\n";
                content += "            }\r\n";
                content += "\r\n";
                content += "            set\r\n";
                content += "            {\r\n";
                content += "                " + s.Name.ToLower() + " = value;\r\n";
                content += "                RaisePropertyChanged(\"" + s.Name.Substring(0, 1) + s.Name.Substring(1, s.Name.Length - 1).ToLower() + "\");\r\n";
                content += "            }\r\n";
                content += "        }\r\n";
                content += "\r\n";
            }
            content += "    }\r\n";
            content += "}\r\n";
            return content;
        }
        /// <summary>
        /// 创建通知类文件
        /// </summary>
        /// <param name="NameSpace">命名空间</param>
        /// <returns></returns>
        private static string CreateRaisePropertyChanged(string NameSpace)
        {
            string content = "";
            content += "using System;\r\n";
            content += "using System.Collections.Generic;\r\n";
            content += "using System.Linq;\r\n";
            content += "using System.Text;\r\n";
            content += "using System.ComponentModel;\r\n";
            content += "\r\n";
            content += "namespace " + "" + NameSpace + "" + "\r\n";
            content += "{\r\n";
            content += "    public class ObjectNotifyPropertyChanged:INotifyPropertyChanged\r\n";
            content += "    {\r\n";
            content += "        public event PropertyChangedEventHandler PropertyChanged;\r\n";
            content += "        public void RaisePropertyChanged(string propertyName)\r\n";
            content += "        {\r\n";
            content += "            if (PropertyChanged != null)\r\n";
            content += "            {\r\n";
            content += "                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));\r\n";
            content += "            }\r\n";
            content += "        }\r\n";
            content += "    }\r\n";
            content += "}\r\n";
            return content;
        }
    }
}
