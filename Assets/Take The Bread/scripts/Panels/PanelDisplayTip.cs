using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//also
public class PanelDisplayTip : MonoBehaviour {

    public GameObject gachaButton;
    //public GameObject ShopMenu;
    // Use this for initialization
    string tipContextType = "";
		Transform panel;
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	bool locker;
//	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
//	{
//		// Add event handler code here
//		switch (control.name) {
//		case "btnDisplayOK":
//			GameManager.getInstance ().playSfx ("click");
//			gameObject.GetComponent<dfPanel> ().IsVisible  = false;
//			GameData.getInstance().isLock = false;
//			Time.timeScale = 1;		
//			break;
//		}
//	}
		public void close(){
			ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 500, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
            if (gachaButton)
            {
            gachaButton.SetActive(true);
            } 
		}

	public void showMe(){
//		GetComponent<dfPanel> ().IsVisible = isVis;
		//show tip freely during level if once displayed.
				panel = transform.Find ("panel");

				initView ();

				GameData.getInstance().cLvShowedTip = true;
		
				ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo","oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));
		
				GameData.getInstance ().lockGame (true);

	}

    int SpinResult()
    {
        Debug.Log(GameData.totalItem);
        return
           Random.Range(0, GameData.totalItem);
    }

    void initView()
    {
        if (SceneManager.GetActiveScene().name == "ShopMenu")
        {
            panel.transform.Find("txtDisplayTipTitle").GetComponent<Text>().text = "Result";
            int result = SpinResult();
            panel.transform.Find("txtDisplayTipContent").GetComponent<Text>().text = result+" ";
            panel.transform.Find("resultImg").GetComponent<Image>().sprite = Resources.Load<Sprite>("hats/" + "item" + result);

            PlayerPrefs.SetInt("item_" + result.ToString(), 1);
            GameManager.getInstance().init();
            //unlock
            GameObject reward = GameObject.Find("item" + result);
            reward.transform.Find("lock").GetComponent<Image>().enabled = false;
            reward.transform.Find("Image").GetComponent<Image>().enabled = true;
            reward.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("hats/" + "item" + result);
            reward.transform.Find("Text").GetComponent<Text>().enabled = false;
        }
        else { 
        panel.transform.Find("txtDisplayTipTitle").GetComponent<Text>().text = Localization.Instance.GetString("tipTitle");
        panel.transform.Find("txtDisplayTipContent").GetComponent<Text>().text = Localization.Instance.GetString("level" + GameData.getInstance().cLevel + "tip");
        panel.transform.Find("btnClose").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnClose");
            }	
		}

		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}

		void OnShowCompleted(){
				GameData.getInstance ().lockGame (true);
		}

	


}
