using System.Collections;
using UnityEngine;
using TMPro;

public class HudPlayerPanel : MonoBehaviour
{
    public static HudPlayerPanel Instance;
    [SerializeField]
    protected Animator          mainPainelAnim;
    [SerializeField]
    protected TextMeshProUGUI   coinsText;
    [SerializeField]
    protected GameObject        friendsGo;
    [SerializeField]
    protected TextMeshProUGUI   friendsText;

    WaitForSeconds  waitClose = new WaitForSeconds(3f);

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);  
    }

    private void Start() 
    {
        GameManager gm = GameManager.Instance;
        coinsText.text = ""+gm.PlayerCoins;
        friendsText.text = "x"+gm.PlayerFriendsPoints;

        friendsGo.SetActive(gm.PlayerFriendsPoints>0);
    }


    public void UpdateCoins(int value)
    {
        coinsText.text = ""+value;
        OpenPanel();
    }

    public void UpdateFriendsPoints(int value)
    {
        friendsText.text = "x"+value;

        if(value>0 && !friendsGo.activeSelf)
            friendsGo.SetActive(true);

        OpenPanel();
    }

    public void OpenPanel(bool activeClose=true)
    {
        StopCoroutine(IClosePanel());

        mainPainelAnim.SetBool("Open",true);

        if(activeClose)
            ClosePanel();
    }

    void ClosePanel(bool coroutine=true)
    {
        if(coroutine)
            StartCoroutine(IClosePanel());
        else
        {
            StopCoroutine(IClosePanel());
            mainPainelAnim.SetBool("Open",false);
        }
    }

    IEnumerator IClosePanel()
    {
        yield return waitClose;

        mainPainelAnim.SetBool("Open",false);
    }
}
