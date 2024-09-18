using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using EventManager.Model;

public class SpawnController : MonoBehaviour
{
    private static SpawnController instance;

    public static SpawnController Instance => instance;
    public List<GameObject> List;

    [SerializeField]
    private bool m_isCloud;

    [SerializeField]
    private bool m_isBlock;

    private Vector3 m_firstPosition;

    private float m_offsetXMin;

    private float m_offsetXMax;

    private List<Vector3> m_originalPosList;

    public GameObject LastBlock;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_offsetXMin = 1.0f;
        m_offsetXMax = 5.0f;

        m_firstPosition = new Vector3(0, 0, 0);

        if (m_isCloud)
        {
            m_offsetXMin = 1.0f;
            m_offsetXMax = 10.0f;
        }

        if (m_isBlock)
        {
            m_offsetXMin = m_offsetXMax = 4;           
        }
 
        Spawn();
        LastBlock = List[List.Count - 1];
        Debug.Log($"LastBlock 1111 : {LastBlock.transform.localPosition} {LastBlock.name}");
        EventBus.Instance.StartListening(EventBusType.RESET_SPAWN_BLOCK, ResetSpawn);
        EventBus.Instance.StartListening(EventBusType.STOP_SCROLL, StopScroll);
        EventBus.Instance.StartListening(EventBusType.START_SCROLL, StartScroll);

    }

    void Spawn()
    {
        for (int i = 0; i < List.Count; i++)
        {
            var go = List[i];
            go.transform.localPosition = m_firstPosition + new Vector3(0, go.transform.localPosition.y, 0);
            m_firstPosition += new Vector3(Random.Range(m_offsetXMin, m_offsetXMax), 0, 0);
        }
    }

    private void ResetSpawn()
    {
        if (!m_isBlock) return;
        foreach(GameObject go in List)
        {
            go.transform.localPosition = Vector3.zero;
        }
        m_firstPosition = Vector3.zero;
        Spawn();
    }

    private void StartScroll()
    {
        foreach (GameObject go in List)
        {
            go.GetComponent<Scroll>().enabled = true;
        }
    }

    private void StopScroll()
    {
        foreach(GameObject go in List)
        {
            go.GetComponent<Scroll>().enabled = false;
        }
    }
}
