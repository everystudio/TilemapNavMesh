using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitMove : MonoBehaviour
{
    public GameObject m_goTarget;
    public Text m_txtShowCommand;

    [SerializeField]
    private int m_iCurrentIndex;
    private NavMeshPath m_moveRoutePath;
    public float m_fSpeed = 1.0f;

    public void OnClickButton()
    {
        m_moveRoutePath = new NavMeshPath();
        m_iCurrentIndex = 0;
        var result = NavMesh.CalculatePath(
            transform.position,
            m_goTarget.transform.position,
            NavMesh.AllAreas, m_moveRoutePath);
    }
    public bool Move()
    {
        var tragetPosition = m_moveRoutePath.corners[m_iCurrentIndex];
        if (Vector3.Distance(transform.position, tragetPosition) < 0.2f)
        {
            if(m_iCurrentIndex + 1 < m_moveRoutePath.corners.Length)
            {
                m_iCurrentIndex += 1;
            }
            else
            {
                return true;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, tragetPosition, m_fSpeed * Time.deltaTime);
        return false;
    }

    private void SearchTask()
    {
        if (m_txtShowCommand != null)
        {
            m_txtShowCommand.text = "Not Found Command";
        }

        RaycastHit2D hit2D = Physics2D.CircleCast(
            transform.position,
            1.0f, 
            Vector2.zero,0.0f,
            LayerMask.GetMask("Task"));
        //Debug.Log(hit2D);
        //Debug.Log(hit2D.collider);
        if (hit2D.collider != null)
        {
            UnitTask unitTask = hit2D.collider.GetComponent<UnitTask>();
            //Debug.Log(unitTask);
            if (unitTask != null)
            {
                if (m_txtShowCommand != null)
                {
                    m_txtShowCommand.text = unitTask.m_strTaskCommand;
                }
                Invoke(unitTask.m_strTaskCommand, 0.0f);
                //Debug.Log("find");
            }
        }
    }
    private void Red()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void Green()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }



    void Update()
    {
        if(m_moveRoutePath != null)
        {
            if (Move())
            {
                SearchTask();
                m_moveRoutePath = null;
            }
        }
    }
}
