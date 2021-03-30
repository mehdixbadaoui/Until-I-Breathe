using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    //Position of the last checkpoint
    private Vector3 lastCheckPointPos;
    private static GameMaster instance;

    private GameObject uni;

    //LetterForUni 
    private List<string> letterList;
    private int indexForLetter;
    private string letterForUni; 


    // Breathing mecanic
    private Breathing_mechanic bm;

    // Falling platform 
    public FallingPlatform[] platforms;

    // Falling platform 
    public GrapplingHook grapplin;
    //Controls
    public bool gamepad = true;


    // Set and get of the last checkoint position
    public Vector3 LastCheckPointPos
    {
        get { return lastCheckPointPos; }   // get method
        set { lastCheckPointPos = value; }  // set method
    }
    

    void Awake()
    {
        //Initialization for the Uni's letter
        InitializationLetter();

        //To check if the gameMaster doesn't exist for the moment
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }


        platforms = (FallingPlatform[])GameObject.FindObjectsOfType(typeof(FallingPlatform));

        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

        // Initialize the last checkpoint if she dies
        lastCheckPointPos = uni.transform.position;

        // Get uni Breathing Mecanic
        bm = uni.GetComponent<Breathing_mechanic>();

        // Get uni Breathing Mecanic
        grapplin = uni.GetComponent<GrapplingHook>();



    }

    private void OnLevelWasLoaded()
    {

        platforms = (FallingPlatform[])GameObject.FindObjectsOfType(typeof(FallingPlatform));

        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

        // Initialize the last checkpoint if she dies
        lastCheckPointPos = uni.transform.position;

        // Get uni Breathing Mecanic
        bm = uni.GetComponent<Breathing_mechanic>();

        // Get uni Breathing Mecanic
        grapplin = uni.GetComponent<GrapplingHook>();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }


    public void Resume()
    {
        Time.timeScale = 1;
    }

    // If Uni die
    public void Die()
    {

        if (grapplin.isGrappling)
        {
            grapplin.CutRope();
        }

        uni.transform.position = lastCheckPointPos;
        uni.transform.rotation = Quaternion.Euler(0, 0, 0);
        uni.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;


        bm.breath = 100;

        if (platforms != null)
        {
            for (int i = 0; i < platforms.Length; ++i)
            {
                platforms[i].Respawn();
            }

        }
    }


    void InitializationLetter()
    {
        //Initialization of a list (length = 9 because we have to find 9 letters) 
        letterList = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            letterList.Add("x");
        }
        indexForLetter = 0;
    }

    //Each time we find a letter it will update the text of the uni's letter
    public void FindLetter()
    {
        if (indexForLetter == 0)
        {
            letterList[3] = "X ... My Celeste is dead. How am I supposed to go on without my dear wife.";
        }
        if (indexForLetter == 1)
        {
            letterList[0] = "X... X is eveywhere. As far as the eye can X there's only X and X. Those X are cursed, even my X can’t compete with the stupidity and the X of those wiggling X. Being born at this time is certainly my X X.";
            letterList[1] = "X X, I’ll X XX X and X, X can’t even X how X I X to X. X X X X in XX X X XX X help.";
            letterList[2] = "X told me the X had X X the X X. X... Anyway I'll X the X X , and if I can't X a X we'll X the X for a X.";
        }
        if (indexForLetter == 2)
        {
            letterList[1] = "X X, I’ll X these X swiftly and promptly, X can’t even realise how X I am to them. without me they’d X in their X X crying their X for help.";
            letterList[2] = "X told me the X had X X the X X. Incompetents ... Anyway I'll X the X X , and if I can't X a X we'll X the X for a X.";
            letterList[4] = "X my X are showing… We X to X the X, and X X , now we need to deal with the X and the X.";
            letterList[7] = "X X, X X X X X. I X I couldn’t X, even X X are X X that’s been X in X. "; 
        }
        if (indexForLetter == 3)
        {
            letterList[0] = "Decadence ... decadence is eveywhere. As far as the eye can X there's only decay and putrefaction. Those X are cursed, even my X can’t compete with the stupidity and the debauchery of those wiggling X. Being born at this time is certainly my biggest X.";
            letterList[1] = "X again, I’ll X these X swiftly and promptly, X can’t even realise how good I am to them. without me they’d suffocate in their X X crying their X for help. ";
            letterList[2] = "X told me the X had trouble X the deeper X. Incompetents ... Anyway I'll X the matter X , and if I can't X a X we'll X the X for a X.";
            letterList[4] = "X my X are showing… We X to X the X, and X successfully, now we need to deal with the X and the X.";
            letterList[6] = "X X… X X back for X! Oh sweet mercy it was delightful … but X can’t X, I X X it.";
            letterList[7] = "X X, X X X X X. I knew I couldn’t X, even my X are X X that’s been X in X. ";
            letterList[8] = "This X will be a X if I want everything to go as X, I’ll X a few things around to avoid any bad X and X some X. I can't allow any more X X."; 
        }
        if (indexForLetter == 4)
        {
            letterList[0] = "Decadence ... decadence is eveywhere. As far as the eye can see there's only decay and putrefaction. Those times are cursed, even my genius can’t compete with the stupidity and the debauchery of those wiggling grubs. Being born at this time is certainly my biggest failure.";
            letterList[1] = "Riots again, I’ll X these scum swiftly and promptly, X can’t even realise how good I am to them. without me they’d suffocate in their own X crying their mother for help.";
            letterList[2] = "X told me the X had trouble supplying the deeper X. Incompetents ... Anyway I'll handle the matter X , and if I can't find a X we'll X the X for a X. ";
            letterList[3] = "She's dead She's dead She's dead She's dead She's dead She's dead She's dead... My Celeste is dead. How am I supposed to go on without my dear wife.";
            letterList[4] = "Finally my X are showing… We managed to X the X, and X successfully, now we need to deal with the X and the X.";
            letterList[7] = "X X, each X X X X. I knew I couldn’t X, even my X are X X that’s been X in X.";
            letterList[8] = "This X will be a X if I want everything to go as X, I’ll X a few things around to avoid any bad surprises and overide some X. I can't allow any more X X."; 

        }
        if (indexForLetter == 5)
        {
            letterList[1] = "Riots again, I’ll crush these scum swiftly and promptly, fools can’t even realise how good I am to them. without me they’d suffocate in their own piss crying their mother for help.";
            letterList[2] = "Council told me the X had trouble supplying the deeper X. Incompetents ... Anyway I'll handle the matter personally, and if I can't find a X we'll cut the X for a week.";
            letterList[4] = "Finally my efforts are showing… We managed to recreate the X, and X successfully, now we need to deal with the X and the X.";
            letterList[6] = "X X… X X back for me ! Oh sweet mercy it was delightful … but X can’t X, I won’t X it.";
            letterList[7] = "X X, each X X more X. I knew I couldn’t X, even my X are X X that’s been X in X.";
            letterList[8] = "This X will be a X if I want everything to go as X, I’ll tweak a few things around to avoid any bad surprises and overide some X. I can't allow any more X X.";
        }
        if (indexForLetter == 6)
        {
            letterList[2] = "Council told me the company had trouble supplying the deeper levels. Incompetents ... Anyway I'll handle the matter personally, and if I can't find a solution we'll cut the pipelines for a week.";
            letterList[4] = "Finally my efforts are showing… We managed to recreate the heart, and liver successfully, now we need to deal with the consciousness and the lungs.";
            letterList[5] = "X since X : X is well but X... I can't gather what it is but I came to X : X X X. That's a X. I X a X and I X X X, I won't X .X X X X and Xmust X.";
            letterList[6] = "X came … X came back for me ! Oh sweet mercy it was delightful … but X can’t X, I won’t allow it.";
            letterList[7] = "X X, each X X more X. I knew I couldn’t X, even my X are beyond X that’s been X in X.";
            letterList[8] = "This X will be a pain if I want everything to go as X, I’ll tweak a few things around to avoid any bad surprises and overide some protocols. I can't allow any more X X.";
        }
        if (indexForLetter == 7)
        {
            letterList[5] = "Day 73 since X : X is well but something is missing... I can't gather what it is but I came to a conclusion : she's not X. That's a failure. I created a failure and I can't stand it, I won't allow it. She can't X and she must X.";
            letterList[8] = "This robot will be a pain if I want everything to go as planned, I’ll tweak a few things around to avoid any bad surprises and overide some protocols. I can't allow any more data leak.";
        }
        if (indexForLetter == 8)
        {
            letterList[5] = "Day 73 since the final transplant : Uni is well but something is missing... I can't gather what it is but I came to a conclusion : she's not my daughter anymore. That's a failure. I created a failure and I can't stand it, I won't allow it. She can't stay anymore and she must disappear.";
            letterList[6] = "She came … She came back for me ! Oh sweet mercy it was delightful … but she can’t stay, I won’t allow it.";
            letterList[7] = "Third encounter, each time brings more joy. I knew I couldn’t fail, even my failures are beyond everything that’s been achieved in human history.";
            letterList[8] = "This robot will be a pain if I want everything to go as planned, I’ll tweak a few things around to avoid any bad surprises and overide some protocols. I can't allow any more data leak.";
        }


        for (int i = 0; i < 9; i++)
        {
            letterForUni += letterList[i] + "\r\n"; 
        }
        Debug.Log(letterForUni);
        letterForUni = ""; 
        indexForLetter += 1;
        Debug.Log(indexForLetter);

        


    }

    public void ChooseMouse()
    {
        gamepad = false;
    }

    public void ChooseGamepad()
    {
        gamepad = true;
    }

    //Just For Testing
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
