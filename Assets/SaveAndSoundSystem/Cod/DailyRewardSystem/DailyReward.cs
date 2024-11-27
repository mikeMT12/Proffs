using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;


namespace DailyRewardSystem
{


    [Serializable] public struct Reward
    {
        public int Amount;
    }


    public class DailyReward : MonoBehaviour
    {
        [Header("Main Menu UI")]
        [SerializeField] TextMeshProUGUI coinsText;

        [Space]
        [Header("Reward UI")]
        [SerializeField] GameObject rewardCanvas;
        [SerializeField] Button openButton;
        [SerializeField] Button closeButton;
        [SerializeField] Image rewardImage;
        [SerializeField] TextMeshProUGUI rewardAmountText;
        [SerializeField] Button claimButton;
        [SerializeField] GameObject rewardsNotification;
        [SerializeField] GameObject noMoreRewardsPanel;
        [SerializeField] TextMeshProUGUI timeToWaitText;

        /*        [Space]
                [Header("Rewards Images")]
                [SerializeField] DailyReward_Data dailyReward_data;*/

        [Space]
        [Header("Rewards Images")]
        [SerializeField] Sprite iconCoinsSprite;

        [Space]
        [Header("Rewards Database")]
        [SerializeField] DailyReward_Data dailyReward_data;
        [SerializeField] WorldData worldData;

        [Space]
		[Header ( "FX" )]
		[SerializeField] ParticleSystem fxCoins;

        [Space]
        [Header("Timing")]
        //wait 23 Hours to activate the next reward (it's better to use 23h instead of 24h)
        [SerializeField] double nextRewardDelay = 23f;
        //check if reward is available every 5 seconds
        [SerializeField] float checkForRewardDelay = 1f;

        

        //[SerializeField] private DailyReward_Data dailyReward_data;
        private int nextRewardIndex;
        private bool isRewardReady = false;
        private bool ferst_time = true;
        /*        public List<Reward> rewards;
                public int nextReward;*/

        private void Awake()
        {
            if (!Directory.Exists(Application.dataPath + "/Resources"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Resources");
            }
            if (!File.Exists(Application.dataPath + "/Resources/XMLWorldData.xml"))
            {
                File.Create(Application.dataPath + "/Resources/XMLWorldData.xml");
                
            }

            /*if (!Directory.Exists(Application.dataPath + "/Resources"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Resources");
            }*/
            if (!File.Exists(Application.dataPath + "/Resources/XMLDailyReward.xml"))
            {
                File.Create(Application.dataPath + "/Resources/XMLDailyReward.xml");
            }
        }
        void Start()
        {
            print(File.ReadAllText(Application.dataPath + "/Resources/XMLWorldData.xml", System.Text.Encoding.UTF8));
            if (File.ReadAllText(Application.dataPath + "/Resources/XMLWorldData.xml", System.Text.Encoding.UTF8) == "") 
            {
                WorldData.SaveInfo(0, false);
                dailyReward_data.rewards = new List<int> {
                10,
                20,
                30,
                40,
                50,
                60,
                70
                };
                dailyReward_data.SaveInfo(dailyReward_data.rewards, 0, DateTime.Now);
                ferst_time = false;
            }
                
            
            worldData.LoadInfo();
            
            //dailyReward_data.SaveInfo(rewards, nextReward, DateTime.Now);
           
            
            //dailyReward_data.SaveInfo(rewards0, 0, DateTime.Now);
            dailyReward_data.LoadInfo();
            
            

            

            //WorldData.SaveInfo(worldData.coins);
            
            Initialize();
           
            StopAllCoroutines();
            StartCoroutine(CheckForRewards());
        }

        private void Initialize() 
        {
            nextRewardIndex = dailyReward_data.nextReward;
            //Update Main menu ui
            UpdateCoinsTextUI();

            //Add click events
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(OnOpenButtonClick);

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClick);

            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(OnClaimButtonClick);

            //Check if the game is opened for the first time then set Reward_Claim_Datetime to the current datetime
           /* if (string.IsNullOrEmpty(PlayerPrefs.GetString("Reward_Claim_Datetime")))
                PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());*/
           if(dailyReward_data.rewardClaimDate.ToString() == null)
           {
                dailyReward_data.rewardClaimDate = DateTime.Now;
           }
        }
        IEnumerator CheckForRewards()
        {
            while (true)
            {
                if (!isRewardReady)
                {
                    DateTime currentDatetime = DateTime.Now;
                    DateTime rewardClaimDatetime = dailyReward_data.rewardClaimDate;
                    //get total Hours between this 2 dates
                    double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalHours;
                    double elapsedMin = (currentDatetime - rewardClaimDatetime).TotalMinutes;
                    Debug.Log(elapsedMin);
                    if (elapsedHours >= nextRewardDelay)
                        ActivateReward();
                    else
                        DesactivateReward(elapsedHours);
                }

                yield return new WaitForSeconds(checkForRewardDelay);
            }
        }

        void ActivateReward()
        {
            isRewardReady = true;

            noMoreRewardsPanel.SetActive(false);
            rewardsNotification.SetActive(true);

            //Update Reward UI
            int reward = dailyReward_data.GetReward(nextRewardIndex);

            //Icon
          
            rewardImage.sprite = iconCoinsSprite;
       

            //Amount
            rewardAmountText.text = string.Format("+{0}", reward);

        }

        void DesactivateReward(double elapsedHours)
        {
            isRewardReady = false;

            noMoreRewardsPanel.SetActive(true);
            //timeToWaitText.text = (nextRewardDelay - elapsedHours).ToString();
            rewardsNotification.SetActive(false);
        }
        // Update is called once per frame
        void UpdateCoinsTextUI()
        {
            coinsText.text = worldData.coins.ToString();
        }

        void OnOpenButtonClick()
        {
            rewardCanvas.SetActive(true);
            
        }

        void OnCloseButtonClick()
        {
            rewardCanvas.SetActive(false);
        }

        void OnClaimButtonClick()
        {
            int reward = dailyReward_data.GetReward(nextRewardIndex);
            //Debug.Log(reward.Type);
            //check reward type
            Debug.Log("<color=yellow>" + "Coins" + " Claimed : </color>+" + reward);
            worldData.coins += reward;
            fxCoins.Play();
            UpdateCoinsTextUI();


            //Save next reward index
            nextRewardIndex++;
            if (nextRewardIndex >= dailyReward_data.rewardsCount)
                nextRewardIndex = 0;

            dailyReward_data.nextReward = nextRewardIndex;
            //PlayerPrefs.SetInt("Next_Reward_Index", nextRewardIndex);

            //Save DateTime of the last Claim Click
            dailyReward_data.rewardClaimDate = DateTime.Now;
            //PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());
            DateTime currentDatetime = DateTime.Now;
            DateTime rewardClaimDatetime = dailyReward_data.rewardClaimDate;
            double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalMinutes;
            DesactivateReward(elapsedHours);
        }

        void OnApplicationQuit()
        {
            Debug.Log("Application ending after " + Time.time + " seconds");
            dailyReward_data.SaveInfo(dailyReward_data.rewards, dailyReward_data.nextReward, dailyReward_data.rewardClaimDate);
            WorldData.SaveInfo(worldData.coins, false);
        }

       


    }
}

