using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{

    public Quest quest;
    public GameObject player;
    public GameObject questWindow;

    private Text uiQuestTitleText;
    private Text uiQuestDescription;
    private Text uiQuestObjectives;
    private Text uiQuestRewardExperience;
    private Text uiQuestRewardGold;


    public void Start()
    {
        //hook up ui text elements
    }

    public void OpenQuestDialog()
    {
        uiQuestTitleText.text = quest.QuestTitle;
        uiQuestDescription.text = quest.QuestDescription;
        uiQuestObjectives.text = quest.QuestObjectives.ToString();
        uiQuestRewardExperience.text = quest.ExperienceReward.ToString();
        uiQuestRewardGold.text = quest.GoldReward.ToString() ;


        questWindow.SetActive(true);


    }

  
}
