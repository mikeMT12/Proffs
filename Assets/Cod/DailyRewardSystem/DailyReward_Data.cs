using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;
using DailyRewardSystem;

[System.Serializable]
public class DailyReward_Data
{
    public  List<int> rewards;
    public  int nextReward;
    public  DateTime rewardClaimDate;


    /*public static int nowReward
    {
        get { return _nowReward; }
        set { SaveInfo(); }
    }

    public static List<int> rewardCount
    {
        get { return _rewardCount; }

    }

    public static DateTime rewardClaimDate
    {
        get { return _rewardClaimDate; }

    }*/


    public int rewardsCount
    {
        get { return rewards.Count; }
    }

    public int GetReward(int index)
    {
        return rewards[index];
    }


    private string filePath;
 

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
  

    public void SaveInfo(List<int> rewards, int nextReward, DateTime rewardClaimDate)
    {
        XmlSerializer formatter = new XmlSerializer(typeof(DailyReward_Data));
        filePath = Application.dataPath + "/Resources/XMLDailyReward.xml";
        //List<int> rewards = new List<int>();
        var data = new DailyReward_Data()
        {
            nextReward = nextReward,
            rewardClaimDate = rewardClaimDate,
            rewards = rewards

        };

        //DailyReward_Data.nowReward;

        if (File.Exists(filePath))
        {
            Debug.Log(File.Exists(filePath));
            File.WriteAllText(filePath, "");
        }

        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, data);
        }


    }
    public void LoadInfo()
    {
        XmlSerializer formatter = new XmlSerializer(typeof(DailyReward_Data));
        filePath = Application.dataPath + "/Resources/XMLDailyReward.xml";
        
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            DailyReward_Data dailyReward_data = (DailyReward_Data)formatter.Deserialize(fs);
            rewards = dailyReward_data.rewards;
            nextReward = dailyReward_data.nextReward;
            rewardClaimDate = dailyReward_data.rewardClaimDate;

        }
    }

}



