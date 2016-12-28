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
        public static void LoadCharacterData(Reader reader)
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

                                if (vars.Length >= 13)
                                {
                                    try
                                    {
                                        bool Used = (vars[0] == "1") ? true : false;
                                        if (Used)
                                        {
                                            DataInfo.Object newModel = new DataInfo.Object();
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
        public static void LoadItems(Reader reader) //TODO: Add extra stats
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

                                if (vars.Length >= 118)
                                {
                                    try
                                    {
                                        bool Used = (vars[0] == "1") ? true : false;
                                        if (Used)
                                        {
                                            var newModel = new DataInfo.Item();
                                            newModel.ModelID = Int32.Parse(vars[1]);
                                            newModel.MediaName = vars[2];
                                            string SN = vars[5];
                                            if (Data.Translation.ContainsKey(SN))
                                                newModel.TranslationName = Data.Translation[SN];
                                            SN = vars[6];
                                            if (Data.Translation.ContainsKey(SN))
                                                newModel.Description = Data.Translation[SN];
                                            newModel.Classes.A = Int32.Parse(vars[7]);
                                            newModel.Classes.B = Int32.Parse(vars[8]);
                                            newModel.Classes.C = Int32.Parse(vars[9]);
                                            newModel.Classes.D = Int32.Parse(vars[10]);
                                            newModel.Classes.E = Int32.Parse(vars[11]);
                                            newModel.Classes.F = Int32.Parse(vars[12]);
                                            newModel.Type = Utility.GetItemType(newModel);
                                            newModel.MaxStack = Int32.Parse(vars[57]);

                                            newModel.Degree = Int32.Parse(vars[61]);
                                            newModel.Duration = Int32.Parse(vars[118]);
                                            Data.MediaItems.Add(newModel.ModelID, newModel);
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

                        dataFile = filesReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading Items." + ex.Message);
            }
        }
        public static void LoadSkills(Reader reader) //TODO: Add extra stats
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

                        Console.WriteLine(dataFile);

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

                                if (vars.Length >= 65)
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
                                            /*newModel.Classes.A = Int32.Parse(vars[7]);
                                            newModel.Classes.B = Int32.Parse(vars[8]);
                                            newModel.Classes.C = Int32.Parse(vars[9]);
                                            newModel.Classes.D = Int32.Parse(vars[10]);
                                            newModel.Classes.E = Int32.Parse(vars[11]);
                                            newModel.Classes.F = Int32.Parse(vars[12]);*/
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
        public static void LoadTeleportBuildings(Reader reader)
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
                                    DataInfo.Object newModel = new DataInfo.Object();
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
        public static void LoadMagicOptions(Reader reader)
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
        public static void LoadTranslation(Reader reader)
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
    }
}
