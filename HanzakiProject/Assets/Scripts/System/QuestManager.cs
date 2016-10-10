﻿//QuestManager by Jordi

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public List<QuestClass> mainQuests = new List<QuestClass>();
    public List<QuestClass> sideQuests = new List<QuestClass>();

    public ProgressionManager _progression;
    public StatsManager _stats;

    void Awake()
    {
        _progression = GetComponent<ProgressionManager>();
        _stats = GetComponent<StatsManager>();
    }

    //Complete a main task and activate the next one
    public void CompleteMainQuest()
    {
        mainQuests[_progression.taskProgession].questState = QuestClass.QuestState.Completed;
        mainQuests[_progression.taskProgession + 1].questState = QuestClass.QuestState.Active;
        _progression.taskProgession++;

        //ui.updatemainquesttext
    }

    //Set a quest active
    public void ActivateSideQuest(int ID)
    {
        sideQuests[ID].questState = QuestClass.QuestState.Active;

        //ui.updatesidequesttext
    }

    //Complete a quest and get reward
    public void CompleteSideQuest(int ID)
    {
        sideQuests[ID].questState = QuestClass.QuestState.Completed;
        _stats.GetLife();
        //ui.updatesidequesttext
    }
}