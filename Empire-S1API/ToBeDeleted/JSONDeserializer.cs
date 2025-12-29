//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Empire.NPC.Data;
//using MelonLoader;
//using MelonLoader.Utils;
//using Newtonsoft.Json;

//namespace Empire.NPC
//{
//    public static class JSONDeserializer
//    {
//        public static DealerData dealerData { get; set; } = new DealerData();
//        //two public dictionary to store the EffectsName and EffectsDollarMult; and QualitiesName and QualitiesDollarMult Lists
//        public static Dictionary<string, float> EffectsDollarMult { get; set; } = new Dictionary<string, float>();
//        public static Dictionary<string, float> QualitiesDollarMult { get; set; } = new Dictionary<string, float>();
//        public static List<float> RandomNumberRanges { get; set; } = new List<float>();
//        public static void Initialize()
//        {
//            // Load dealer data
//            string jsonPath = Path.Combine(MelonEnvironment.ModsDirectory, "Empire", "empire.json");
//            if (!File.Exists(jsonPath))
//            {
//                MelonLogger.Error("❌ empire.json file not found.");
//                return;
//            }

//            try
//            {
//                // Load dealer data if not already loaded (null or empty)
//                if (dealerData.Dealers == null || dealerData.Dealers.Count == 0)
//                {
//                    MelonLogger.Msg("Loading dealer data from empire.json...");
//                    string jsonContent = File.ReadAllText(jsonPath);
//                    MelonLogger.Msg("JSON Content read. Deserializing");
//                    try
//                    {
//                        // Deserialize the JSON content into DealerData object
//                        dealerData = JsonConvert.DeserializeObject<DealerData>(jsonContent);
//                    }
//                    catch (JsonReaderException ex)
//                    {
//                        MelonLogger.Error($"❌ Failed to parse empire.json: {ex.Message}");
//                    }
//                    catch (Exception ex)
//                    {
//                        MelonLogger.Error($"❌ Unexpected error during initialization: {ex}");
//                    }
//                    MelonLogger.Msg("JSON Content deserialized");
//                }

//                if (dealerData?.Dealers == null || dealerData.Dealers.Count == 0)
//                {
//                    MelonLogger.Error("❌ No dealers found in empire.json.");
//                    return;
//                }
//            }
//            catch (JsonReaderException ex)
//            {
//                MelonLogger.Error($"❌ Failed to parse empire.json: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                MelonLogger.Error($"❌ Unexpected error during initialization: {ex}");
//            }
            
//            // Load additional JSON files in the Empire folder
//            string empireFolder = Path.Combine(MelonEnvironment.ModsDirectory, "Empire");
//            try
//            {
//                var additionalFiles = Directory.GetFiles(empireFolder, "*.json")
//                    .Where(f => !f.Equals(jsonPath, StringComparison.OrdinalIgnoreCase));

//                MelonLogger.Msg($"Found {additionalFiles.Count()} additional JSON files to process.");

//				foreach (var file in additionalFiles)
//                {
//                    try
//                    {
//                        string additionalJson = File.ReadAllText(file);
//                        var additionalData = JsonConvert.DeserializeObject<DealerData>(additionalJson);
//                        if (additionalData?.Dealers != null && additionalData.Dealers.Count > 0)
//                        {
//                            // Only add dealers whose Name is not already present
//                            var existingNames = new HashSet<string>(dealerData.Dealers.Select(d => d.Name), StringComparer.OrdinalIgnoreCase);
//                            var newDealers = additionalData.Dealers
//                                .Where(d => !string.IsNullOrWhiteSpace(d.Name) && !existingNames.Contains(d.Name))
//                                .ToList();

//                            dealerData.Dealers.AddRange(newDealers);
//                            MelonLogger.Msg($"Loaded additional {newDealers.Count} dealers from {Path.GetFileName(file)}");
//                        }
//                    }
//                    catch (Exception exFile)
//                    {
//                        MelonLogger.Error($"❌ Error reading/de-serializing additional file {Path.GetFileName(file)}: {exFile.Message}");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MelonLogger.Error($"❌ Unexpected error while scanning additional JSON files: {ex}");
//            }

//            // Create two dictionaries from EffectsName and EffectsDollarMult; and QualityTypes and QualitiesDollarMult Lists
//            EffectsDollarMult = dealerData?.EffectsName?.Select((name, index) => new { name = name.Trim().ToLowerInvariant(), index })
//                .ToDictionary(x => x.name, x => dealerData?.EffectsDollarMult?[x.index] ?? 0f);
//            QualitiesDollarMult = (dealerData?.QualityTypes ?? new List<string>())
//                .Select((name, index) => new { name = name.Trim().ToLowerInvariant(), index })
//                .ToDictionary(x => x.name, x => dealerData?.QualitiesDollarMult?[x.index] ?? 0f);
//            // Log both in MelonLogger
//            MelonLogger.Msg($"Effects Dollar Mult: {string.Join(", ", EffectsDollarMult.Select(x => $"{x.Key}: {x.Value}"))}");
//            MelonLogger.Msg($"Qualities Dollar Mult: {string.Join(", ", QualitiesDollarMult.Select(x => $"{x.Key}: {x.Value}"))}");
//            RandomNumberRanges = dealerData?.RandomNumberRanges ?? new List<float>();
//        }
//    }
//}