using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float m_offsetMin;

    private float m_offsetMax;

    [SerializeField]
    private float m_scrollSpeed;

    [SerializeField]
    private bool m_isBlock;

    private bool IsInvisible
    {
        get
        {
            return transform.position.x < -CameraBound.SharedInstance.Width - 3;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(m_isBlock)
        {
            m_offsetMin = 4;
            m_offsetMax = 6;
        }
        else
        {
            m_offsetMin = m_offsetMax = 6;
        }
        
    }

    private void Update()
    {
        UpdatePosition();

        if (IsInvisible)
        {
            ResetPosition();
        }
    }

    private void UpdatePosition()
    {
        var velocityX = -m_scrollSpeed * Time.deltaTime;

        transform.position += new Vector3(velocityX, 0, 0);
    }

    private void ResetPosition()
    {
        var lastBlock = SpawnController.Instance.LastBlock.transform.localPosition;

        transform.localPosition = new Vector3(lastBlock.x + Random.Range(m_offsetMin, m_offsetMax), transform.localPosition.y, 0);

        SpawnController.Instance.LastBlock = gameObject;
    }
}
