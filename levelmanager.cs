using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelmanager : MonoBehaviour {

public static levelmanager Instance { set; get; }



    private float Timer1;
    private float Timer2;
    private float Timer3;
    public GameObject Player;

    public bool SHOW_COLLIDER = true;//$$

    // Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 100;
    private const int INTITAL_SEGMENTS = 10;
    private const int INITIAL_TRANSITION_SEGMENT= 1;
    private const int MAX_SEGMENT_ON_SCREEN = 15;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int containiuousSegments;
    private int currentSpawnZ;
    //private int currentLevel;
    private int y1, y2, y3;


    //Tutorial
    public Animator Tutorial2;
    public Text Tutorial2Text;
    public string Tutorial2String;

    private AudioSource HintSound;




    //MonsterType
    private int MonsterType;

    //Level2 items
    public Animator TaptoattackMonster1;
    public bool Level2 ;
    public GameObject Monster;
    public GameObject MonsterPoint;
    private bool Monster1isDeead  ;

    //Level3 items
    public bool Level3 ;
    public GameObject Monster2;
    public Segment EmptyObject;
    private bool Monster2isDead ;

    private bool Monster3_Add =false;

    //[HideInInspector]
    public Segment ObstacleAdd01, ObstacleAdd02, ObstacleAdd03, ObstacleAdd04 ,ObstacleAdd05, ObstacleAdd06;
    public Segment Monster3_M, Monster3_L, Monster3_R;



    //List of pieces
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumpss = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    [HideInInspector]

    public List<Piece> pieces = new List<Piece>();//all the pieces in the pool


    //List of Segments
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransition = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();

    //GamePlay
   // private bool isMoveing = false;

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        //currentLevel = 0;
                
    }

    private void Start()
    {

        Timer1 = 0;
        Timer2 = 0;
        Timer3 = 0;

        for (int i = 0; i < INTITAL_SEGMENTS; i++)
        {
            if (i < INITIAL_TRANSITION_SEGMENT)
                SpawnTransition();
            else
                GenerateSegment();
        }

        HintSound = GameObject.Find("Hint").GetComponent<AudioSource>();
     




    }
    private void Update()
    {

        
        
        



        if (currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
        }

        if (amountOfActiveSegments >= MAX_SEGMENT_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawm();
            amountOfActiveSegments--;
        }      
      

    }
    private void LateUpdate()
    {
        

        if (!PlayerMove.Instance.isRunning)
            return;

        //Into Level2
        Timer1 += Time.deltaTime;
        if (Timer1 > 20f && Timer1<21f && !Level2)
        {
            GameLevel2();
            Timer3 = 0;
            
        }

        //Level2 is Overed.
           if(Level2 == true && !GameObject.FindGameObjectWithTag("Monster1"))
           {
            Monster1isDeead = true;
            Level2 = false;
            availableSegments.Clear();
            availableSegments.Add(EmptyObject);
           
           }

        if (Monster1isDeead == true && !Level3 )
        {
            Timer2 += Time.deltaTime;
            if (Timer2 > 10f)
            {
                Level3 = true;
                GameLevel4();               
               
            }
        }

        //Level3 is overd
        if (Level3 == true && !GameObject.FindGameObjectWithTag("Monster2")&&Monster3_Add==false)
        {
            CameraMotor.Instance.CamerChangeBack();
            Monster2isDead = true;
            Level3 = false;
            Monster1isDeead = false;
            Monster3_Add = true;
            availableSegments.Clear();
            availableSegments.Add(ObstacleAdd01);
            availableSegments.Add(ObstacleAdd02);
            availableSegments.Add(ObstacleAdd03);          
            availableSegments.Add(ObstacleAdd06);
            availableSegments.Add(Monster3_M);
            availableSegments.Add(Monster3_R);
            availableSegments.Add(Monster3_L);
            

        }
        
        if (Monster3_Add == true)
        {
            Timer3 += Time.deltaTime;
            if (Timer3 > 20)
            {
                Debug.Log("Monster3_Add");
                Monster3_Add = false;
                availableSegments.Remove(Monster3_M);
                availableSegments.Remove(Monster3_R);
                availableSegments.Remove(Monster3_L);
                availableSegments.Add(ObstacleAdd04);
                availableSegments.Add(ObstacleAdd05);

                Timer1 = 0;
                Timer2 = 0;

            }
           
        }





    }



    private void GenerateSegment()
    {
        SpawnSegment();

        if (Random.Range(0f,1f)<(containiuousSegments*0.25f))
        
        {
            //Spawn transiotion seg
            containiuousSegments=0;
            SpawnTransition();

        }
        else
        {
            containiuousSegments++;
        }
       
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beinY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();


    }
    private void SpawnTransition()
    {
        List<Segment> possibleTransition= availableTransition.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beinY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }

　　public Segment GetSegment(int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegID == id && x.transition == transition && !x.gameObject.activeSelf);

        if (s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransition[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegID = id;
            s.transition = transition;

            segments.Insert(0, s);
        }

        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;
        
    }
    public Piece GetPiece(PieceType pt, int visulIndex)
    {
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visulIndex && !x.gameObject.activeSelf);
        //activeSelf=active himself and the parent wont be active

        if (p == null)
        {
            GameObject go = null;
            if (pt == PieceType.ramp)
                go = ramps[visulIndex].gameObject;
            else if (pt == PieceType.longblock)
                go = longblocks[visulIndex].gameObject;
            else if (pt == PieceType.jump)
                go = jumpss[visulIndex].gameObject;
            else if (pt == PieceType.slide)
                go = slides[visulIndex].gameObject;

            go = Instantiate(go);//instantiate實例
            p = go.GetComponent<Piece>();
            pieces.Add(p);


        }
        return p;

    }



    //Level2 GameObject
 

    private void GameLevel2()
    {
        HintSound.Play();
        Tutorial2.SetTrigger("Show");       
        Tutorial2Text.text = Tutorial2String;

        TaptoattackMonster1.SetTrigger("show");
            Level2 = true;
            //Instatiate Monster 
            GameObject go = (GameObject)Instantiate(Monster);
            go.transform.parent = MonsterPoint.transform;
            go.transform.position = MonsterPoint.transform.position;
    }    
   
    private void GameLevel4()
    {
        TaptoattackMonster1.SetTrigger("show");      
        CameraMotor.Instance.CameraChange();
        GameObject go = (GameObject)Instantiate(Monster2);
        go.transform.parent = MonsterPoint.transform;
        go.transform.position = MonsterPoint.transform.position;
        
    }

 
}
