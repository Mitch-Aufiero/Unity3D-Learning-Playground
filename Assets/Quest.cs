using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public string QuestTitle;
    public string QuestDescription;
    public string[] QuestPrerequisites;

    public string[] QuestObjectives;

    public int ExperienceReward;
    public int GoldReward;
    public string[] QuestItemRewards;


}
