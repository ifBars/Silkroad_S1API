using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MelonLoader;
using MelonLoader.Utils;
using Newtonsoft.Json;
using S1API.Logging;
using S1API.Entities.NPCs;
using S1API.GameTime;
using S1API.Money;
using S1API.Entities;
using S1API.Console;
using UnityEngine;

#if (Il2Cpp)
using Console = Il2CppScheduleOne.Console;
#elif (Mono)
using Console = ScheduleOne.Console;
#endif

namespace Empire;

public class RewardManager
{
    BlackmarketBuyer buyer;
    // NEW: Store dealer reward info from the buyer's associated dealer
    public bool isRewardAvailable = false;

    public RewardManager(BlackmarketBuyer buyer)
    {
        this.buyer = buyer;
        isRewardAvailable = true;
        TimeManager.OnDayPass += SetRewardAvailable;
    }

    public void SetRewardAvailable()
    {
        isRewardAvailable = true;
    }
    public string GetRewardType()
    {
        if (buyer.Reward?.Args == null || buyer.Reward.Args.Count == 0)
            return "No special favors available from this contact right now.";

        string command = buyer.Reward.Args[0].ToLower();
        
        switch (command)
        {
            // Camera & UI Controls
            case "freecam": return "Contact can provide surveillance equipment";
            case "hideui": 
            case "showfps":
            case "hidefps": return "Contact offering to modify your HUD display";

            // Game State
            case "save": return "Contact can secure your current position";
            case "settime": return "Contact knows when to make things happen";
            case "settimescale": return "Contact can help time move differently";

            // Inventory & Money
            case "give":
            case "additemtoinventory": return "Contact has special items to offer";
            case "clearinventory": return "Contact can help you clean house";
            case "changecash": return "Contact offering some quick cash";
            case "changebalance": return "Contact can adjust your digital assets";
            case "addxp": return "Contact willing to share street knowledge";

            // Vehicle
            case "spawnvehicle": return "Contact has connections at the chop shop";

            // Player Abilities
            case "setmovespeed": return "Contact knows ways to enhance your mobility";
            case "setjumpforce": return "Contact has experimental movement tech";
            case "setstaminareserve": return "Contact offering stamina boosters";
            case "teleport": return "Contact knows some shortcuts through the city";

            // Property & Business
            case "setowned": return "Contact can arrange property ownership papers";
            case "packageproduct": return "Contact can help with product packaging";
            case "addemployee": return "Contact knows reliable workers";
            case "setdiscovered": return "Contact can reveal hidden locations";
            case "growplants": return "Contact knows ways to accelerate plant growth";

            // Law Enforcement
            case "raisewanted":
            case "lowerwanted":
            case "clearwanted": return "Contact has influence with law enforcement";
            case "setlawintensity": return "Contact can adjust police patrol intensity";

            // Health & Status
            case "sethealth": return "Contact knows a back-alley medic";
            case "setquality": return "Contact can improve product quality";

            // Relationships & Quests
            case "setvar":
            case "setqueststate":
            case "setquestentrystate": return "Contact can influence ongoing situations";
            case "setemotion": return "Contact knows how to affect people's moods";
            case "setrelationship": return "Contact can influence relationships";
            case "setunlocked": return "Contact can unlock new opportunities";

            // Environment
            case "cleartrash": return "Contact knows clean-up specialists";

            // System Controls
            case "bind":
            case "unbind":
            case "clearbinds": return "Contact offering to adjust your controls";
            case "disable":
            case "enable": return "Contact can toggle certain operations";
            case "endtutorial": return "Contact can end your training period";
            case "disablenpcasset": return "Contact can make certain people scarce";

            default:
                return $"Contact has an undefined favor to offer";
        }
        
    }
    public void GiveReward()
    {
        string rewardType = buyer.Reward?.Type ?? "";
        if (!isRewardAvailable)
        {
            MelonLogger.Error("Reward is not available right now.");
            return;
        }
        if (buyer.Reward == null || buyer.Reward.Args == null || buyer.Reward.Args.Count == 0 || rewardType == "")
        {
            MelonLogger.Error("No reward available from this contact.");
            return;
        }
        // Check if unlockRep is less than buyer Rep
        if (buyer.Reward?.unlockRep > 0 && buyer._DealerData.Reputation >= buyer.Reward.unlockRep)
        {
            MelonLogger.Error($"Insufficient reputation to claim reward. Required: {buyer.Reward.unlockRep}, Current: {buyer._DealerData.Reputation}");
            return;
        }
        // Deduct reputation cost from current reputation
        buyer._DealerData.Reputation -= buyer.Reward.unlockRep;
        // TODO - Switch to using s1api console once supported
        // TODO - Add support for other reward types
        if (rewardType.ToLower() == "console")
        {
            if (buyer.Reward.Args != null && buyer.Reward.Args.Count > 0)
            {
                string command = string.Join(" ", buyer.Reward.Args);
                Console.SubmitCommand(command);
                MelonLogger.Msg($"Executing console command: {command}");
            }
        }
        isRewardAvailable = false;
    }
}