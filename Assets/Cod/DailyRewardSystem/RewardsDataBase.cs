using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using DailyRewardSystem;
using System;

/*public class ParseValues { 
}*/

public class RewardsDataBase : MonoBehaviour
{
    private string filePath;
    [SerializeField] 

  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveInfo(List<Reward> rewards, int nextReward, DateTime rewardClaimDate)
    {
        XmlSerializer formatter = new XmlSerializer(typeof(DailyReward_Data));

        //List<int> rewards = new List<int>();
        var data = new DailyReward_Data()
        {
            nextReward = nextReward,
            rewardClaimDate = rewardClaimDate,
            rewards = rewards

        };

        //DailyReward_Data.nowReward;

        if (File.Exists(Application.dataPath + "/Level/Resources/XMLDailyReward.xml"))
        {
            File.WriteAllText(Application.dataPath + "/Level/Resources/XMLDailyReward.xml", "");
        }

        using (FileStream fs = new FileStream(Application.dataPath + "/Level/Resources/XMLDailyReward.xml", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, data);
        }


    }
    public void LoadInfo()
    {
        XmlSerializer formatter = new XmlSerializer(typeof(DailyReward_Data));
        //private string filePath = Application.dataPath + "/Level/Resources/XMLDailyReward.xml";
        using (FileStream fs = new FileStream(Application.dataPath + "/Level/Resources/XMLDailyReward.xml", FileMode.OpenOrCreate))
        {
            DailyReward_Data dailyReward_data = (DailyReward_Data)formatter.Deserialize(fs);
            /*rewards = dailyReward_data.rewards;
            nextReward = dailyReward_data.nextReward;
            rewardClaimDate = dailyReward_data.rewardClaimDate;*/

        }
    }

        public static int GetInfo(string key, int defaultValue)
    {
        return 1;
    }

    public static int SetInfo(string key, int defaultValue)
    {
        return 1;
    }
}
