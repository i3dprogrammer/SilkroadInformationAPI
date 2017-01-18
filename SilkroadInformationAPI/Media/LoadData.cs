using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK2Reader;
using PK2Reader.Framework;
using PK2Reader.EntrySet;

namespace SilkroadInformationAPI.Media
{
    class LoadData
    {
        private static Reader reader;

        public static void InitializeReader(string MediaPath, string blowfish = "169841")
        {
            if (!System.IO.File.Exists(MediaPath))
            {
                throw new System.IO.FileNotFoundException("Couldn't find the media path, please check it.");
            }

            try
            {
                reader = new Reader(MediaPath, blowfish);
            }
            catch
            {
                throw new Exception("Error during loading the media, please check the pk2 blowfish.");
            }
        }

        public static bool IsInitialized()
        {
            return reader is object;
        }

        public static void LoadCharacterData()
        {
            try
            {
                using(System.IO.StringReader filesReader = new System.IO.StringReader(reader.GetFileText("characterdata.txt")))
                {
                    string dataFile = filesReader.ReadLine();

                    while (dataFile != null)
                    {
                        if (dataFile == "")
                            continue;

                        //Console.WriteLine(dataFile);

                        using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText(dataFile.ToLower())))
                        {
                            string line = streamReader.ReadLine();
                            
                            while (line != null)
                            {
                                if (line == "" || line.Contains('\t') == false)
                                {
                                    line = streamReader.ReadLine();
                                    continue;
                                }

                                string[] vars = line.Split('\t');

                                if (vars.Length >= 13)
                                {
                                    try
                                    {
                                        bool Used = (vars[0] == "1") ? true : false;
                                        if (Used)
                                        {
                                            DataInfo.MediaModel newModel = new DataInfo.MediaModel();
                                            newModel.ModelID = Int32.Parse(vars[1]);
                                            newModel.MediaName = vars[2];
                                            string SN = vars[5];
                                            if(Data.Translation.ContainsKey(SN))
                                                newModel.TranslationName = Data.Translation[SN];
                                            newModel.Classes.A = Int32.Parse(vars[7]);
                                            newModel.Classes.B = Int32.Parse(vars[8]);
                                            newModel.Classes.C = Int32.Parse(vars[9]);
                                            newModel.Classes.D = Int32.Parse(vars[10]);
                                            newModel.Classes.E = Int32.Parse(vars[11]);
                                            newModel.Classes.F = Int32.Parse(vars[12]);
                                            newModel.Type = Utility.GetModelType(newModel);
                                            Data.MediaModels.Add(newModel.ModelID, newModel);
                                        }
                                    }
                                    catch (Exception ex) {
                                        //Console.WriteLine(ex.StackTrace);
                                    }
                                }

                                line = streamReader.ReadLine();
                            }
                        }

                        dataFile = filesReader.ReadLine();
                    }
                }
            } catch(Exception ex)
            {
                throw new Exception("Error loading models. " + ex.Message);
            } 
        }
        public static void LoadItems() //TODO: Add extra attrs, Add Item Models to MediaModels
        {
            try
            {
                using (System.IO.StringReader filesReader = new System.IO.StringReader(reader.GetFileText("itemdata.txt")))
                {
                    string dataFile = filesReader.ReadLine();

                    while (dataFile != null)
                    {
                        if (dataFile == "")
                            continue;

                        //Console.WriteLine(dataFile);

                        using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText(dataFile.ToLower())))
                        {
                            string line = streamReader.ReadLine();

                            while (line != null)
                            {
                                if (line == "" || line.Contains('\t') == false)
                                {
                                    line = streamReader.ReadLine();
                                    continue;
                                }

                                string[] vars = line.Split('\t');

                                if (vars.Length >= 118)
                                {
                                    try
                                    {
                                        bool Used = (vars[0] == "1") ? true : false;
                                        if (Used)
                                        {
                                            var newItem = new DataInfo.Item();
                                            var newModel = new DataInfo.MediaModel();
                                            newItem.ModelID = newModel.ModelID = Int32.Parse(vars[1]);
                                            newItem.MediaName = newModel.MediaName = vars[2];
                                            string SN = vars[5];
                                            if (Data.Translation.ContainsKey(SN))
                                                newItem.TranslationName = newModel.TranslationName = Data.Translation[SN];
                                            SN = vars[6];
                                            if (Data.Translation.ContainsKey(SN))
                                                newItem.Description = newModel.TranslationName = Data.Translation[SN];
                                            newItem.Classes.A = newModel.Classes.A = Int32.Parse(vars[7]);
                                            newItem.Classes.B = newModel.Classes.B = Int32.Parse(vars[8]);
                                            newItem.Classes.C = newModel.Classes.C = Int32.Parse(vars[9]);
                                            newItem.Classes.D = newModel.Classes.D = Int32.Parse(vars[10]);
                                            newItem.Classes.E = newModel.Classes.E = Int32.Parse(vars[11]);
                                            newItem.Classes.F = newModel.Classes.F = Int32.Parse(vars[12]);
                                            newItem.Type = Utility.GetItemType(newItem);
                                            newModel.Type = ModelType.Unknown;
                                            newItem.MaxStack = Int32.Parse(vars[57]);

                                            newItem.Degree = Int32.Parse(vars[61]);
                                            newItem.Duration = Int32.Parse(vars[118]);

                                            Data.MediaItems.Add(newItem.ModelID, newItem);
                                            Data.MediaModels.Add(newModel.ModelID, newModel);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //Console.WriteLine(ex.Message + ex.StackTrace);
                                    }
                                }

                                line = streamReader.ReadLine();
                            }
                        }

                        dataFile = filesReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading Items." + ex.Message);
            }
        }
        public static void LoadSkills() //TODO: Add extra attrs
        {
            try
            {
                using (System.IO.StringReader filesReader = new System.IO.StringReader(reader.GetFileText("skilldataenc.txt")))
                {
                    string dataFile = filesReader.ReadLine();

                    while (dataFile != null)
                    {
                        if (dataFile == "")
                            continue;

                        //Console.WriteLine(dataFile);

                        using (System.IO.TextReader streamReader = new System.IO.StringReader(Utility.DecryptSkills(reader.GetFileBytes(dataFile.ToLower()))))
                        {
                            string line = streamReader.ReadLine();
                            while (line != null)
                            {
                                if (line == "" || line.Contains('\t') == false)
                                {
                                    line = streamReader.ReadLine();
                                    continue;
                                }

                                string[] vars = line.Split('\t');

                                if (vars.Length >= 70)
                                {
                                    try
                                    {
                                        bool Used = (vars[0] == "1") ? true : false;
                                        if (Used)
                                        {
                                            var newModel = new DataInfo.Skill();
                                            newModel.ModelID = Int32.Parse(vars[1]);
                                            newModel.MediaName = vars[3];
                                            string SN = vars[62];
                                            if (Data.Translation.ContainsKey(SN))
                                                newModel.TranslationName = Data.Translation[SN];
                                            SN = vars[64];
                                            if (Data.Translation.ContainsKey(SN))
                                                newModel.Description = Data.Translation[SN];
                                            newModel.Params = vars[69];
                                            Data.MediaSkills.Add(newModel.ModelID, newModel);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message + " " + ex.StackTrace);
                                    }
                                }

                                line = streamReader.ReadLine();
                            }
                        }

                        dataFile = filesReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading skills." + ex.Message);
            }
        }
        public static void LoadTeleportBuildings()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("teleportbuilding.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 13)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    DataInfo.MediaModel newModel = new DataInfo.MediaModel();
                                    newModel.ModelID = Int32.Parse(vars[1]);
                                    newModel.MediaName = vars[2];
                                    string SN = vars[5];
                                    if (Data.Translation.ContainsKey(SN))
                                        newModel.TranslationName = Data.Translation[SN];
                                    newModel.Classes.A = Int32.Parse(vars[7]);
                                    newModel.Classes.B = Int32.Parse(vars[8]);
                                    newModel.Classes.C = Int32.Parse(vars[9]);
                                    newModel.Classes.D = Int32.Parse(vars[10]);
                                    newModel.Classes.E = Int32.Parse(vars[11]);
                                    newModel.Classes.F = Int32.Parse(vars[12]);
                                    newModel.Type = Utility.GetModelType(newModel);
                                    Data.MediaModels.Add(newModel.ModelID, newModel);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.StackTrace);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
             }
            catch (Exception ex)
            {
                throw new Exception("Error loading teleport buildings." + ex.Message);
            }
        }
        public static void LoadMagicOptions()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("magicoption.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 13)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    int ID = Int32.Parse(vars[1]);
                                    string MediaName = vars[2];
                                    Data.MediaBlues.Add(ID, MediaName);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.StackTrace);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading magic options." + ex.Message);
            }
        }
        public static void LoadTranslation()
        {
            try
            {
                using (System.IO.StringReader filesReader = new System.IO.StringReader(reader.GetFileText("textdataname.txt")))
                {
                    string dataFile = filesReader.ReadLine();

                    while (dataFile != null)
                    {
                        if (dataFile == "")
                            continue;

                        Console.WriteLine(dataFile);

                        using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText(dataFile.ToLower())))
                        {
                            string line = streamReader.ReadLine();

                            while (line != null)
                            {
                                if (line == "" || line.Contains('\t') == false)
                                {
                                    line = streamReader.ReadLine();
                                    continue;
                                }

                                string[] vars = line.Split('\t');

                                if (vars.Length >= 10)
                                {
                                    try
                                    {
                                        bool Used = (vars[0] == "1") ? true : false;
                                        if (Used)
                                        {
                                            string SN = vars[1];
                                            string text1 = vars[8];
                                            string text2 = vars[9];
                                            Data.Translation.Add(SN, (text1 == "" || text1 == "0") ? text2 : text1);
                                        }
                                    }
                                    catch
                                    {
                                        //Console.WriteLine(ex.StackTrace);
                                    }
                                }

                                line = streamReader.ReadLine();
                            }
                        }

                        dataFile = filesReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading translation files." + ex.Message);
            }
        }

        public static void LoadLevelData()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("leveldata.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 2)
                        {
                            try
                            {
                                int level = Int32.Parse(vars[0]);
                                ulong maxExp = ulong.Parse(vars[1]);
                                Data.MaxEXP.Add(level, maxExp);
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.StackTrace);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading level data." + ex.Message);
            }
        }

        public static void LoadShops()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refshop.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');
                        if (vars.Length >= 5)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    int ID = Int32.Parse(vars[2]);
                                    string StoreName = vars[3];
                                    var shop = new DataInfo.Shops.Shop(StoreName);
                                    shop.ShopGroups.AddRange(Data.refmappingshopwithtab[StoreName]);
                                    shop.StoreGroupName = Data.refmappingshopgroup[StoreName];
                                    shop.NPCName = Data.refshopgroup[shop.StoreGroupName];
                                    Data.MediaShops.Add(shop);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading NPC Shops." + ex.Message);
            }
        }
        public static void LoadRefMappingShopWithTab()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refmappingshopwithtab.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 3)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    string StoreName = vars[2];
                                    string GroupName = vars[3];
                                    var shopgroup = new DataInfo.Shops.ShopGroup(GroupName);
                                    shopgroup.GroupTabs.AddRange(Data.refshoptab[GroupName]);
                                    if (!Data.refmappingshopwithtab.ContainsKey(StoreName))
                                        Data.refmappingshopwithtab.Add(StoreName, new List<DataInfo.Shops.ShopGroup> { shopgroup });
                                    else
                                        Data.refmappingshopwithtab[StoreName].Add(shopgroup);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping shops groups to stores." + ex.Message);
            }
        }
        public static void LoadRefShopTabs()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refshoptab.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 5)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    int ID = Int32.Parse(vars[2]);
                                    string TabName = vars[3];
                                    string GroupName = vars[4];
                                    var shoptab = new DataInfo.Shops.ShopTab(TabName);
                                    shoptab.TabItems.AddRange(Data.refshopgoods[TabName]);
                                    if (!Data.refshoptab.ContainsKey(GroupName))
                                        Data.refshoptab.Add(GroupName, new List<DataInfo.Shops.ShopTab> { shoptab });
                                    else
                                        Data.refshoptab[GroupName].Add(shoptab);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping shop tabs to groups." + ex.Message);
            }
        }
        public static void LoadRefShopGoods()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refshopgoods.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 5)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    string TabName = vars[2];
                                    string PackageName = vars[3];
                                    int PackagePosition = Int32.Parse(vars[4]);
                                    var item = new DataInfo.Shops.ShopItemPackage(PackageName, PackagePosition);
                                    item.ItemMediaName = Data.refscrapofpackageitem[PackageName];
                                    if (!Data.refshopgoods.ContainsKey(TabName))
                                        Data.refshopgoods.Add(TabName, new List<DataInfo.Shops.ShopItemPackage> { item });
                                    else
                                        Data.refshopgoods[TabName].Add(item);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading shop tabs goods." + ex.Message);
            }
        }
        public static void LoadRefScrapOfPackageItem()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refscrapofpackageitem.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 5)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    string PackageName = vars[2];
                                    string ItemName = vars[3];
                                    Data.refscrapofpackageitem.Add(PackageName, ItemName);
                                    //Console.WriteLine(PackageName + " : " + ItemName + " : " + Data.refscrapofpackageitem.Count);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping package items to items." + ex.Message);
            }
        }
        public static void LoadRefShopGroup()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refshopgroup.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }

                        string[] vars = line.Split('\t');

                        if (vars.Length >= 5)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    string GroupName = vars[3];
                                    string NPCName = vars[4];
                                    Data.refshopgroup.Add(GroupName, NPCName);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping package items to items." + ex.Message);
            }
        }
        public static void LoadRefMappingShopGroup()
        {
            try
            {
                using (System.IO.TextReader streamReader = new System.IO.StringReader(reader.GetFileText("refmappingshopgroup.txt")))
                {
                    string line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line == "" || line.Contains('\t') == false)
                        {
                            line = streamReader.ReadLine();
                            continue;
                        }
                        string[] vars = line.Split('\t');

                        if (vars.Length >= 4)
                        {
                            try
                            {
                                bool Used = (vars[0] == "1") ? true : false;
                                if (Used)
                                {
                                    string GroupName = vars[2];
                                    string StoreName = vars[3];
                                    Data.refmappingshopgroup.Add(StoreName, GroupName);
                                    //Console.WriteLine(PackageName + " : " + ItemName + " : " + Data.refscrapofpackageitem.Count);
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                            }
                        }

                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping package items to items." + ex.Message);
            }
        }
    }
}
