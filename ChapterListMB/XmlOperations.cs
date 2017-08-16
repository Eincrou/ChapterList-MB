using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChapterListMB
{
    internal static class XmlOperations
    {
        /// <summary>
        /// Saves provided ChapterList to XML format
        /// </summary>
        /// <param name="chapList"></param>
        internal static void SaveChapterListToXml(ChapterList chapList)
        {
            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("Chapterlist", new XAttribute("version", "1.0")
                )
            );
            foreach (var chapter in chapList)
            {
                var newElement = new XElement("Chapter",
                    new XAttribute("pos", chapter.Position),
                    new XAttribute("name", chapter.Title)
                );
                xmlDoc.Root.Add(newElement);
            }
            xmlDoc.Save(Track.XmlPath.LocalPath);
        }

        /// <summary>
        /// Reads ChapterList data into the provided ChapterList object
        /// </summary>
        /// <param name="chapList">Object to read XML chapters into</param>
        internal static void ReadChapterListFromXml(ChapterList chapList)
        {
            try
            {
                if (!System.IO.File.Exists(Track.XmlPath.LocalPath))
                {
                    throw new FileNotFoundException();
                }
                var chaptersListDoc = XDocument.Load(Track.XmlPath.LocalPath);
                if (chaptersListDoc.Root.Attribute("version").Value == "1.0") // 1.0 is original chapterlist XML format
                {
                    foreach (var xElement in chaptersListDoc.Descendants("Chapter"))
                    {
                        chapList.CreateNewChapter(xElement.Attribute("name").Value,
                            int.Parse(xElement.Attribute("pos").Value));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // ignored
            }
            catch (Exception)
            {
                // ignored
            }
        }

        internal static Dictionary<string, SkinElementColors> GetSkinColors(string skinFile)
        {
            try
            {
                if (!System.IO.File.Exists(skinFile))
                {
                    throw new FileNotFoundException("This skin file could not be located");
                }
                Dictionary<string,SkinElementColors> skinElementColors = new Dictionary<string, SkinElementColors>();
                XDocument skinDoc = XDocument.Load(skinFile);
                var test = skinDoc.Root.Element("colours");
                foreach (XElement xElement in skinDoc.Root.Element("colours").Elements())
                {
                    string foreground = xElement.Attribute("fg")?.Value;
                    string background = xElement.Attribute("bg")?.Value;
                    string background2 = xElement.Attribute("bg2")?.Value;
                    string border = xElement.Attribute("bdr")?.Value;
                    SkinElementColors elementColors = new SkinElementColors(xElement.Attribute("id").Value,
                        foreground, background, background2, border);
                    skinElementColors.Add(elementColors.ElementName, elementColors);
                }
                return skinElementColors;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
