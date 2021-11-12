using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HudQuestManager : MonoBehaviour
{
    public static HudQuestManager Instance;

    [SerializeField]
    private     GameObject          questGo;
    [SerializeField]
    private     TextMeshProUGUI     titleText;
    [SerializeField]
    private     TextMeshProUGUI     descriptionText;
    [SerializeField]
    private     Button              acceptButton;
    [SerializeField]
    private     Button              refuseButton;
    [SerializeField]
    private     Button              completeButton;

    private     QuestScriptable quest;
    
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);  
    }

    public void Close()
    {
        questGo.SetActive(false);
    }

    public void Open(QuestScriptable _quest,bool complete=false)
    {
        quest = _quest;
        titleText.text = quest.Name;

        if(complete)
        {
            descriptionText.text = "Congratulations you completed this mission!!!\nCoins: +"+_quest.BonusCoinComplete+"\nFriends Points: +"+_quest.BonusFriendPointComplete;
        }
        else
            descriptionText.text = quest.Description;

        completeButton.gameObject.SetActive(complete);
        questGo.SetActive(true);
    }

    public void AcceptQuest()
    {
        GameManager.Instance.SetQuestStep(quest,0);
        Close();
    }

    public void RefusetQuest()
    {
        //GameManager.Instance.SetQuestStep(quest,-1);
        Close();
    }
}
