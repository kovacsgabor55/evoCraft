using EvoCraft.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace EvoCraft.Core
{
    /// <summary>
    /// Loads the maps from the predefined way of .txt files.
    /// </summary>
    public class MapLoader
    {
        static string path = Directory.GetCurrentDirectory();

        static MapLoader()
        {
            path += "\\Maps\\";
        }

        /// <summary>
        /// Betölt egy pályát a fájlból. Ha bármilyen hiba történik és nem sikerül a betöltés,
        /// akkor egy MapLoadingFailedException-t dob.
        /// </summary>
        /// <param name="fileNameAndPath">A fájl teljes elérési útvonala</param>
        /// <returns>A betöltött pálya</returns>
        public static Map LoadFromFile(string fileNameAndPath)
        {
            bool didMapLoadingSucceed = true;
            bool mapDataStarted = false;
            bool mapDataStartedLock = true;
            int gold = 0;
            int wood = 0;
            int food = 0;
            int width = 30;
            int height = 20;
            string title = "";
            string author = "";
            Map loadedMap = null;

            string propertyValue = "";
            string propertyName = "";
            int currentLineInMapData = 0;
            int currentCharInMapData = 0;
            try
            {
                using (var reader = new StreamReader(fileNameAndPath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!mapDataStarted)
                        {
                            if (line.Length > 0 && !line.Contains("#"))
                            {
                                if (line.Contains(":"))
                                {
                                    propertyName = line.Substring(0, line.IndexOf(":"));
                                    if (!line.EndsWith(":"))
                                    {
                                        propertyValue = line.Substring(line.IndexOf(":") + 2);
                                    }
                                    if (propertyName.Contains("Map"))
                                    {
                                        mapDataStarted = true;
                                    }
                                    else if (propertyName.Contains("Gold"))
                                    {
                                        gold = int.Parse(propertyValue);
                                    }
                                    else if (propertyName.Contains("Wood"))
                                    {
                                        wood = int.Parse(propertyValue);
                                    }
                                    else if (propertyName.Contains("Food"))
                                    {
                                        food = int.Parse(propertyValue);
                                    }
                                    else if (propertyName.Contains("Width"))
                                    {
                                        width = int.Parse(propertyValue);
                                    }
                                    else if (propertyName.Contains("Height"))
                                    {
                                        height = int.Parse(propertyValue);
                                    }
                                    else if (propertyName.Contains("Title"))
                                    {
                                        title = propertyValue;
                                    }
                                    else if (propertyName.Contains("Author"))
                                    {
                                        author = propertyValue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            currentCharInMapData = 0;
                            // Itt dől el, hogy milyen karakterre mit tölt be
                            foreach (char c in line)
                            {
                                switch (c)
                                {
                                    case 'O': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Rock()); break;
                                    case 'X': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Wall(0,false)); break;
                                    case 'H': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new MainHall(0, false)); break;
                                    case 'B': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Barracks(0, false)); break;
                                    case 'N': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new MedicalTent(0, false)); break;
                                    case 'E': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Tower(0, false)); break;
                                    case 'W': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Worker(0)); break;
                                    case 'S': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Soldier(0)); break;
                                    case 'D': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Doctor(0)); break;
                                    case 'P': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Hero(0)); break;
                                    case 'G': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new GunMan(0)); break;
                                    case 'T': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Tree()); break;
                                    case 'M': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Mine(500)); break;
                                    case 'F': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Farm()); break;
                                    case '1': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Sloth()); break;
                                    case '2': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Llama()); break;
                                    case '3': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Chupacabra()); break;
                                    case '4': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Boss()); break;
                                    case '5': loadedMap.GetCellAt(currentLineInMapData, currentCharInMapData).MapObjects.Add(new Rolls()); break;
                                }
                                currentCharInMapData++;
                            }
                            currentLineInMapData++;
                        }


                        if (!didMapLoadingSucceed)
                        {
                            break;
                        }

                        if (mapDataStarted && mapDataStartedLock)
                        {   
                            loadedMap = new Map(title, author, width, height, gold, food, wood);
                            mapDataStartedLock = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new MapLoadingFailedException("Map loading failed.", e);
            }
            
            

            return loadedMap;
        }

        /// <summary>
        /// A pályák mappájából minden pálya betöltése.
        /// </summary>
        /// <returns></returns>
        public static List<Map> LoadAll()
        {
            List<Map> maps = new List<Map>();

            string[] fileEntries = Directory.GetFiles(path);
            foreach( string fileName in fileEntries)
            {
                try
                {
                    Map map = LoadFromFile(fileName);
                    if (map != null)
                    {
                        maps.Add(map);
                    }
                }
                catch (MapLoadingFailedException)
                {
                    Console.WriteLine("Error reading map: " + fileName);
                }
            }
            

            return maps;
        }

        /// <summary>
        /// Betölt egy pályát úgy, hogy csak a fájlnevet kell beírni. Teszteléshez volt használva.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Map LoadFromFileWithEasyFileName(string fileName)
        {
            return LoadFromFile(path + fileName);
        }

    }
}
