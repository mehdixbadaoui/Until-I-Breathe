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


        platforms = (FallingPlatform[])GameObject.FindObjectsOfType(typeof(FallingPlatform));

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

        platforms = (FallingPlatform[])GameObject.FindObjectsOfType(typeof(FallingPlatform));

        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

        // Initialize the last checkpoint if she dies
        lastCheckPointPos = uni.transform.position;

        // Get uni Breathing Mecanic
        bm = uni.GetComponent<Breathing_mechanic>();

        // Get uni Breathing Mecanic
        grapplin = uni.GetComponent<GrapplingHook>();

        //Train
        LT = FindObjectOfType<LaunchTrain>();
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
        if (platforms != null)
        {
            for (int i = 0; i < platforms.Length; ++i)
            {
                platforms[i].Respawn();
            }

        }

        //RESET TRAIN
        //LT.canstart = true;
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


    //Each time we find a letter it will update the text of the uni's letter
    public void FindLetter()
    {
        if (indexForLetter == 0)
        {
            letterList[3] = "X X X X X X X ... Ma Céleste est morte ... Comment suis je supposé continuer à vivre sans ma tendre épouse ?";
        }
        if (indexForLetter == 1)
        {
            letterList[0] = "X ... une dégradation X et X. Qu'importe X sur lequel X X X, tout ce qu'il y X est X et putréfaction. Ces X sont X, même mon X X X X X X stupidité et X X de ces X grouillants X X X. Être né à cette époque et très X X X X X.";
            letterList[1] = "X des X, X X déchets X X X X d'âme. Ces X ne réalisent X X X X X X peux X X. Sans X X X dans leur propre pisse X X X X X.";
            letterList[2] = "X X vient X m'apprendre que X X X mal à approvisioner les X X. X X ... X, X X X X X X X X. X X X X X X X X X, X X X X X X X X X X X X.";
        }
        if (indexForLetter == 2)
        {
            letterList[0] = "Décadence ... une dégradation X et X. Qu'importe l'horizon sur lequel mon regard se X, tout ce qu'il y trouve est X et putréfaction. Ces temps sont maudits, même mon génie ne peut rivalsier avec la stupidité de ces vers grouillants. Être né à cette époque est très certainement X X X X."; 
            letterList[1] = "Encore des X, j'écraserai ces déchets X et sans états d'âme. Ces imbéciles ne réalisent pas à quel point je peux être X. Sans moi ils X dans leur propre pisse X X X X X.";
            letterList[2] = "Le conseil vient de m'apprendre que X avait du mal à approvisioner les niveaux inférieurs. Ces X ... Qu'importe, je me X X X X X X. Et si je n'arrive pas à trouver X X, il suffira de X X pendant quelques temps X X X X X.";
            letterList[4] = "Enfin X efforts X X X. Nous avons X à X X X X X X X manière X. Maintenant X X X atteler X X X X X X.";
            letterList[7] = "X X, chaque X X X X X X que la X. X X échecs X X X X X X X X l'humanité X X X.";
            letterList[8] = "X X X X X X si tout doit se passer X X X, X X X X quelques X X X de X X certains X X X toute X X. Même X X X X X X X ont été X, je ne peux X X X X X X X X."; 
        }
        if (indexForLetter == 3)
        {
            letterList[0] = "Décadence ... une dégradation perpétuelle et immuable. Qu'importe l'horizon sur lequel mon regard se pose, tout ce qu'il y trouve est pourriture et putréfaction. Ces temps sont maudits, même mon génie ne peut rivaliser avec la stupidité et la débauche de ces asticots grouillants sur le sol. Être né à cette époque est très certainement mon plus grand échec.";
            letterList[1] = "Encore des émeutes, j'écraserai ces déchets rapidement et sans état d'âme. Ces imbéciles ne réalisent même pas à quel point je peux être généreux. Sans moi ils suffoqueraient dans leur propre pisse pleurant pour un peu d'aide. ";
            letterList[2] = "Le conseil vient de m'apprendre que X avait du mal à approvisioner les niveaux inférieurs. Ces X ... Qu'importe, je me X X X X X X. Et si je n'arrive pas à trouver X X, il suffira de X X pendant quelques temps X X X X X.";
            letterList[4] = "Enfin X efforts X X X. Nous avons X à X X X X X X X manière X. Maintenant X X X atteler X X X X X X.";
            letterList[5] = "X X depuis la X X: X se X X mais X X X X... X X pas X X X X X X X X X à une X : X X X X X. C'est un X. X X X X et je ne le X X, X X X X X. X ne peut X, X doit X.";
            letterList[6] = "X est X ... X est X X X ! Oh mon dieu X X X... Mais X X peut X X, je ne le X X.";
            letterList[7] = "X X, chaque X X X X X X que la X. X X échecs X X X X X X X X l'humanité X X X. ";
            letterList[8] = "X X X X X X si tout doit se passer X X X, X X X X quelques X X X de X X certains X X X toute X X. Même X X X X X X X ont été X, je ne peux X X X X X X X X."; 
        }
        if (indexForLetter == 4)
        {
            letterList[2] = "Le conseil vient de m'apprendre que l'entreprise avait du mal à approvisioner les niveaux inférieurs. Ces incompétents ... Qu'importe, je me chargerai moi même de la situation. Et si je n'arrive pas à trouver de solution, il suffira de couper l'approvisionement pendant quelques temps afin de diminuer la demande. ";
            letterList[3] = "ElleestmorteElleestmorteElleestmorteElleestmorteElleestmorteElleestmorteElleestmorte ... Ma Céleste est morte... Comment suis je supposé continuer à vivre sans ma tendre épouse ?";
            letterList[4] = "Enfin mes efforts portent leurs fruits. Nous avons X à X X X X X X X manière X. Maintenant X X X atteler X X X X X X.";
            letterList[5] = "X X depuis la X X: X se porte X mais X chose est X... Je n'arrive pas à mettre le X X mais j'en suis X à une X : ce n'est X X X. C'est un X. J'ai X un X et je ne le X X, je ne le permettrais pas. X ne peut X, X doit X.";
            letterList[6] = "X est X... X est X X voir ! Oh mon dieu quel X X... Mais X X peut pas X, je ne le X X."; 
            letterList[7] = "X X, chaque nouvelle X est encore plus X que la X. Même mes échecs X X au delà X X ce que l'humanité a pu X.";
            letterList[8] = "X X va être un problème si tout doit se passer X je X, je vais devoir modifier quelques X et l'empêcher de passer X certains X pour éviter toute mauvaise X. Même si je ne sais pas quels X ont été X , je ne peux pas me permettre une autre X de X."; 

        }
        if (indexForLetter == 5)
        {
            letterList[4] = "Enfin mes efforts portent leurs fruits. Nous avons réussi à recréer le X X X X de manière saine. Maintenant nous devons nous atteler X X X X X X.";
            letterList[5] = "X X depuis la X X: X se porte X mais quelque chose est différent ... Je n'arrive pas à mettre le X X mais j'en suis X à une X : ce n'est X X X. C'est un X. J'ai X un X et je ne le X X, je ne le permettrais pas. X ne XX, X doit X.";
            letterList[6] = "X est X... X est X X voir ! Oh mon dieu quel moment délectable ... Mais X ne peut pas X, je ne le X X.";
            letterList[7] = "Troisième X, chaque nouvelle X est encore plus délectable que la dernière. Même mes échecs sont bien au delà de tout ce que l'humanité a pu accomplir.";
            letterList[8] = "Ce X va être un problème si tout doit se passer comme je l'entends, je vais devoir modifier quelques paramètres et l'empêcher de passer outre certains X pour éviter toute mauvaise surprise. Même si je ne sais pas quels X ont été X , je ne peux pas me permettre une autre X de X.";

        }
        if (indexForLetter == 6)
        {
            
            letterList[4] = "Enfin mes efforts portent leurs fruits. Nous avons réussi à recréer le coeur et le foie de manière saine. Maintenant nous devons nous atteler à la conscience et aux poumons.";
            letterList[5] = "Jour X depuis la X finale : X se porte bien mais quelque chose est différent ... Je n'arrive pas à mettre le doigt dessus mais j'en suis venu à une X : ce n'est plus ma X. C'est un échec. J'ai X un échec et je ne le supporte pas, je ne le permettrais pas. X ne peut rester, X doit X.";
        }
        if (indexForLetter == 7)
        {
            letterList[5] = "Jour 73 depuis la transplantation finale : X se porte bien mais quelque chose est différent ... Je n'arrive pas à mettre le doigt dessus mais j'en suis venu à une conclusion : ce n'est plus ma X. C'est un échec. J'ai engendré un échec et je ne le supporte pas, je ne le permettrais pas. Elle ne peut rester, elle doit disparaitre.";
            letterList[6] = "Elle est revenue ... Elle est revenue me voir ! Oh mon dieu quel moment délectable ... Mais elle ne peut pas rester, je ne le permettrais pas.";

            letterList[7] = "Troisième rencontre, chaque nouvelle session est encore plus délectable que la dernière. Même mes échecs sont bien au delà de tout ce que l'humanité a pu accomplir."; 
            letterList[8] = "Ce robot va être un problème si tout doit se passer comme je l'entends, je vais devoir modifier quelques paramètres et l'empêcher de passer outre certains protocoles pour éviter toute mauvaise surprise. Même si je ne sais pas quels informations ont été compromises, je ne peux pas me permettre une autre fuite de données.";
        }
        if (indexForLetter == 8)
        {
            letterList[5] = "Jour 73 depuis la transplantation finale : Uni se porte bien mais quelque chose est différent ... Je n'arrive pas à mettre le doigt dessus mais j'en suis venu à une conclusion : ce n'est plus ma fille. C'est un échec. J'ai engendré un échec et je ne le supporte pas, je ne le permettrais pas. Elle ne peut rester, elle doit disparaitre.";
            
        }


        for (int i = 0; i < 9; i++)
        {
            letterForUni += letterList[i] + "\r\n"; 
        }
        Debug.Log(letterForUni);
        DeadRobotsText(letterForUni); 
        letterForUni = ""; 
        indexForLetter += 1;
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
