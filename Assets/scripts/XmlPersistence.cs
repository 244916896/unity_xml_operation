/********************************************************************************
** Auth:zhanghl
** Date:2017/3/10
** FileName:XmlPersistence
** Desc:键值对的形式在本地保存读取内容
** Ver:V1.0.0
*********************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;

namespace mm
{
    /**
    *  @addtogroup Unity
    *  @{
    */
    /// <summary>
    /// 键值对的形式在本地保存读取内容
    /// </summary>
    public class XmlPersistence
    {
        private XmlPersistence()
        {

        }
        private static string PathRoot = Application.persistentDataPath + "//fileData//";

        /// <summary>
        /// 通过键读取指定文件中此键所对应的内容
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="fileName">文件名（含后缀名），默认可不填</param>
        /// <returns>查询返回""</returns>
        public static string RetrieveXml(string key, string fileName = "mmFile.xml", string filePath = "")
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                PathRoot = filePath;
            }
            if (File.Exists(PathRoot + fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(PathRoot + fileName);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.GetAttribute("code") == key)
                    {
                        return xe.InnerText;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 删除指定的键
        /// </summary>
        /// <param name="key">指定的键</param>
        /// <param name="fileName">文件名（含后缀名），默认可不填</param>
        public static void DeleteXml(string key, string fileName = "mmFile.xml", string filePath = "")
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                PathRoot = filePath;
            }
            if (File.Exists(PathRoot + fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(PathRoot + fileName);
                XmlNode root = xmlDoc.SelectSingleNode("root");
                XmlNodeList nodeList = root.ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.GetAttribute("code") == key)
                    {
                        root.RemoveChild(xe);
                        break;
                    }
                }
                xmlDoc.Save(PathRoot + fileName);
            }
        }
        /// <summary>
        /// 将指定的键修改为指定的值(存在键值对是更新，不存在时添加)
        /// </summary>
        /// <param name="key">指定键</param>
        /// <param name="val">新值</param>
        /// <param name="fileName">文件名（含后缀名），默认可不填</param>
        public static void UpdateXml(string key, string val, string fileName = "mmFile.xml", string filePath = "")
        {
            if (val == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(filePath))
            {
                PathRoot = filePath;
            }
            if (!Directory.Exists(PathRoot))
            {
                Directory.CreateDirectory(PathRoot);
            }
            if (!File.Exists(PathRoot + fileName))
            {
                //创建XML文档实例
                XmlDocument xmlDoc = new XmlDocument();
                //创建root节点，也就是最上一层节点
                XmlElement root = xmlDoc.CreateElement("root");
                XmlElement attri = xmlDoc.CreateElement("code");
                attri.SetAttribute("code", key);
                attri.InnerText = val;
                root.AppendChild(attri);
                xmlDoc.AppendChild(root);
                //把XML文件保存至本地
                xmlDoc.Save(PathRoot + fileName);
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(PathRoot + fileName);
                XmlNode root = xmlDoc.SelectSingleNode("root");
                XmlNodeList nodeList = root.ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.GetAttribute("code") == key)
                    {
                        xe.InnerText = val;
                        xmlDoc.Save(PathRoot + fileName);
                        return;
                    }
                }
                XmlElement attri = xmlDoc.CreateElement("code");
                attri.SetAttribute("code", key);
                attri.InnerText = val;
                root.AppendChild(attri);
                xmlDoc.Save(PathRoot + fileName);
            }
        }
        /// <summary>
        /// 读取指定xml文件到字典中
        /// </summary>
        /// <param name="fileName">文件名（含后缀名），默认可不填</param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXmlToDic(string fileName = "mmFile.xml", string filePath = "")
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                PathRoot = filePath;
            }
            Dictionary<string, string> _tmp = new Dictionary<string, string>();
            if (File.Exists(PathRoot + fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(PathRoot + fileName);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    _tmp.Add(xe.GetAttribute("code"), xe.InnerText);
                }
            }
            return _tmp;
        }
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="fileName">文件名（含后缀名）</param>
        public static void DeleteFile(string fileName = "mmFile.xml", string filePath = "")
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                PathRoot = filePath;
            }
            if (File.Exists(PathRoot + fileName))
            {
                File.Delete(PathRoot + fileName);
            }
        }
    }
    /**
    *  @}
    */
}