using UnityEngine;

public enum ObjectType
{
    Red_Dominos,
    Green_Dominos,
};

public class GameRoot : MonoBehaviour
{
    public GameObject DominosRoot;
    public int Rows;
    public int Columns;
    public GameObject GridReferenceObject;
    public GameObject GridRoot;
    public bool ShowGrid;
    public GameObject FirstPersonPlayer;

    private RaycastHit m_HitObject;
    private ObjectType m_ObjectType = ObjectType.Green_Dominos;
    
    private GameObject m_GreenDomino;
    private GameObject m_RedDomino;
    private GameObject m_Ball;

    private GameObject m_Cell;
    private Grid       m_Grid;
    private Material   m_Black;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.Instance.SubscribeEventListener(EventId.KeyPad_1_Pressed, ProcessButtonPress);
        GameEventManager.Instance.SubscribeEventListener(EventId.KeyPad_2_Pressed, ProcessButtonPress);
        GameEventManager.Instance.SubscribeEventListener(EventId.Spacebar_Pressed, ProcessButtonPress);

        m_GreenDomino = Resources.Load<GameObject>("Prefabs/DominoCube_Green");
        m_RedDomino = Resources.Load<GameObject>("Prefabs/DominoCube_Red");
        m_Ball = Resources.Load<GameObject>("Prefabs/Ball");
        m_Cell = Resources.Load<GameObject>("Prefabs/Cell");

        m_Black = Resources.Load<Material>("Material/Black");

        GenerateGridData();
    }

    void GenerateGridData()
    {
        Transform referenceObjectTransform = GridReferenceObject.transform;
        m_Grid = new Grid(Rows, Columns, referenceObjectTransform.localScale, referenceObjectTransform.localPosition);
        m_Grid.Generate();

        if (ShowGrid)
        {
            bool useBlack ;
            bool startWithBlack = false;

            for (int i = 0; i < Rows; i++)
            {
                useBlack = startWithBlack;
                for (int j = 0; j < Columns; j++)
                {
                    Vector3 position = m_Grid.AllSquares[i, j].Position;
                    GameObject cellObject = GameObject.Instantiate(m_Cell);
                    cellObject.transform.parent = GridRoot.transform;

                    cellObject.transform.localPosition = position;
                    cellObject.transform.localScale = m_Grid.CellSize;
                    if (useBlack)
                    {
                        cellObject.GetComponent<Renderer>().material = m_Black;    
                    }
                    useBlack = !useBlack;
                }
                startWithBlack = !startWithBlack;
            }

            GridReferenceObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast(ray,out m_HitObject))
            {
                Rigidbody rigidbody = m_HitObject.rigidbody;
                rigidbody?.AddForce(ray.direction * 12, ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out m_HitObject))
            {
                if( m_HitObject.transform.gameObject.CompareTag("Ground"))
                {
                    Vector2 gridIndex = m_Grid.GetGridIndexFromPosition(m_HitObject.point);
                    Vector3 gridPosition = m_Grid.AllSquares[(int)gridIndex.x, (int)gridIndex.y].Position;
                    GameObject temp = null;
                    if (m_ObjectType == ObjectType.Green_Dominos)
                    {
                        temp = GameObject.Instantiate(m_GreenDomino);
                    }
                    else if (m_ObjectType == ObjectType.Red_Dominos)
                    {
                        temp = GameObject.Instantiate(m_RedDomino);
                    }

                    temp.transform.parent = DominosRoot.transform;
                    temp.transform.position = gridPosition;
                }
            }
        }
    }

    void ProcessButtonPress(object data)
    {
        if(data.ToString() == EventId.KeyPad_1_Pressed )
        {
            m_ObjectType = ObjectType.Green_Dominos;
        }
        else if( data.ToString() == EventId.KeyPad_2_Pressed)
        {
            m_ObjectType = ObjectType.Red_Dominos;
        }
        else if( data.ToString() == EventId.Spacebar_Pressed)
        {
            GameObject ballObject = GameObject.Instantiate(m_Ball);
            ballObject.transform.position = FirstPersonPlayer.transform.position;
            Rigidbody rigidbody = ballObject.GetComponent<Rigidbody>();
            rigidbody?.AddForce(FirstPersonPlayer.transform.forward * 4, ForceMode.Impulse);
        }
    }
}
