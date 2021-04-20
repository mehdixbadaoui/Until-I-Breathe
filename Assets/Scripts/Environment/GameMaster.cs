using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

public class GameMaster : MonoBehaviour
{
    //Position of the last checkpoint
    public Vector3 lastCheckPointPos;
    public Checkpoint lastCheckpoint;
    private static GameMaster instance;

    private GameObject uni;

    //LetterForUni 
    private List<string> letterList;
    private int indexForLetter;
    private string letterForUni; 


    // Breathing mecanic
    private Breathing_mechanic bm;

    // Falling platform 
    private FallingPlatform[] fallingPlatforms;

    // Falling platform 
    public GrapplingHook grapplin;

    //Controls
    public bool gamepad = true;

    //Train
    LaunchTrain LT;
    public TMP_Text DialoguBox;
    public Canvas canvas;



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


        fallingPlatforms = (FallingPlatform[])GameObject.FindObjectsOfType(typeof(FallingPlatform));

        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

        // Initialize the last checkpoint if she dies
        lastCheckPointPos = uni.transform.position;

        // Get uni Breathing Mecanic
        bm = uni.GetComponent<Breathing_mechanic>();

        // Get uni Breathing Mecanic
        grapplin = uni.GetComponent<GrapplingHook>();

        if (GameObject.FindGameObjectWithTag("CinematicBeginning"))
        {
            StartCoroutine(Cinematic(GameObject.FindGameObjectWithTag("CinematicBeginning").GetComponent<PlayableDirector>()));
        }




    }

    private void FixedUpdate()
    {
        //Debug.Log( GameObject.FindGameObjectWithTag("CinematicBeginning").GetComponent<PlayableDirector>().state );
    }

    private void OnLevelWasLoaded()
    {

        fallingPlatforms = (FallingPlatform[])GameObject.FindObjectsOfType(typeof(FallingPlatform));

        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

        // Initialize the last checkpoint if she dies
        lastCheckPointPos = uni.transform.position;
        lastCheckpoint = null;

        // Get uni Breathing Mecanic
        bm = uni.GetComponent<Breathing_mechanic>();

        // Get uni Breathing Mecanic
        grapplin = uni.GetComponent<GrapplingHook>();

        //Train
        LT = FindObjectOfType<LaunchTrain>();


        if (GameObject.FindGameObjectWithTag("CinematicBeginning"))
        {
            StartCoroutine(Cinematic(GameObject.FindGameObjectWithTag("CinematicBeginning").GetComponent<PlayableDirector>()));
        }
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

        //RESET PLATFORM POSITIONS
        if (fallingPlatforms != null)
        {
            for (int i = 0; i < fallingPlatforms.Length; ++i)
            {
                fallingPlatforms[i].Respawn();
            }

        }


        //RESET PLATFORM POSITIONS
        if (lastCheckpoint != null && lastCheckpoint.platformsToReset != null)
        {
            for (int i = 0; i < lastCheckpoint.platformsToReset.Length; ++i)
            {
                lastCheckpoint.platformsToReset[i].Respawn();
            }

        }

        //RESET TRAIN

        ResetCanstarts();
        
        //PullBox.can_pull = true;
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

    private IEnumerator Cinematic(PlayableDirector playable)
    {
        Movement.canMove = false;

        playable.Play();


        //Wait for the beginning of BreathingDead
        /*        yield return new WaitWhile(() => playable.state == PlayState.Playing );
                Debug.Log("While");

                yield return new WaitWhile(() => playable.state != PlayState.Playing );
                Debug.Log("End");
        */
        yield return new WaitForSeconds( (float)playable.duration );

        playable.Stop();


        Movement.canMove = true;
    }

    void ResetCanstarts()
    {
        LaunchTrain[] lts = FindObjectsOfType<LaunchTrain>();
        foreach (LaunchTrain lt in lts)
            lt.canstart = true;
    }

    //Each time we find a letter it will update the text of the uni's letter
    public string FindLetter()
    {
        if (indexForLetter == 0)
        {
            letterList[0] = "Je me souviens du temps où les humains n’avaient pas peur de perdre la chose la plus précieuse pour leur survie : l’oxygène. Aveuglés par leur essor économique, ils ont oublié la chose la plus précieuse qu’ils avaient. Mais cette tragédie n’a pas fait que des malheureux… Certains opportunistes ont su en profiter. Étant donné que l’éthique n’est plus au goût du jour, chacun fait bien ce qu’il veut.";
        }
        if (indexForLetter == 1)
        {
            letterList[1] = "Décadence ... une dégradation perpétuelle et immuable. Qu'importe l'horizon sur lequel mon regard se pose, tout ce qu'il y trouve est pourriture et putréfaction. Ces temps sont maudits, même mon génie ne peut rivaliser avec la stupidité et la débauche de ces asticots grouillants sur le sol. Être né à cette époque est très certainement mon plus grand échec.";
        }
        if (indexForLetter == 2)
        {
            letterList[2] = "Encore des émeutes, j'écraserai ces déchets rapidement et sans état d'âme.Ces imbéciles ne réalisent même pas à quel point je peux être généreux.Sans moi ils suffoqueraient dans leur propre pisse pleurant pour un peu d'aide.";
        }
        if (indexForLetter == 3)
        {
            
            letterList[3] = "Le conseil vient de m'apprendre que l'entreprise avait du mal à approvisionner les niveaux inférieurs. Ces incompétents ... Qu'importe, je me chargerai moi-même de la situation. Et si je n'arrive pas à trouver de solution, il suffira de couper l'approvisionnement pendant quelque temps afin de diminuer la demande."; 
        }
        if (indexForLetter == 4)
        {
            letterList[4] = "ElleestmorteElleestmorteElleestmorteElleestmorteElleestmorteElleestmorteElleestmorte ... Ma Céleste est morte... Comment suis je supposé continuer à vivre sans ma tendre épouse ?";
        }
        if (indexForLetter == 5)
        {
            letterList[5] = "Enfin mes efforts portent leurs fruits. Nous avons réussi à recréer le cœur et le foie de manière saine. Maintenant nous devons nous atteler à la conscience et aux poumons.";
        }
        if (indexForLetter == 6)
        {
            letterList[6] = "Jour 73 depuis la transplantation finale : Uni se porte bien mais quelque chose est différent ... Je n'arrive pas à mettre le doigt dessus mais j'en suis venu à une conclusion : ce n'est plus ma fille. C'est un échec. J'ai engendré un échec et je ne le supporte pas, je ne le permettrait pas. Elle ne peut rester, elle doit disparaître."; 
        }
        if (indexForLetter == 7)
        {
            letterList[7] = "Elle est revenue ... Elle est revenue me voir ! Oh mon dieu quel moment délectable ... Mais elle ne peut pas rester, je ne le permettrait pas. \r\n Troisième rencontre, chaque nouvelle session est encore plus délectable que la dernière.Même mes échecs sont bien au-delà de tout ce que l'humanité a pu accomplir."; 
        }
        if (indexForLetter == 8)
        {
            letterList[8] = "Ce robot va être un problème si tout doit se passer comme je l'entends, je vais devoir modifier quelques paramètres et l'empêcher de passer outre certains protocoles pour éviter toute mauvaise surprise. Même si je ne sais pas quelles informations ont été compromises, je ne peux pas me permettre une autre fuite de données. ";
        }


        for (int i = 0; i < 9; i++)
        {
            letterForUni += letterList[i] + "\r\n"; 
        }
        string letterforUnifinal = letterForUni;
        letterForUni = "";
        indexForLetter += 1;
        return letterforUnifinal; 
        
        //DeadRobotsText(letterForUni); 
        
        
        Debug.Log(indexForLetter);

        


    }
    //private void DeadRobotsText(string letterForUni)
    //{
    //    canvas.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    //    DialoguBox.text = letterForUni;
    //}

    public void ChooseMouse()
    {
        gamepad = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void ChooseGamepad()
    {
        gamepad = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    //Just For Testing
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
