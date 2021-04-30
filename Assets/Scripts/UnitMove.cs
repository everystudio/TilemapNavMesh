using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    public GameObject m_goTarget;

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



    void Update()
    {
        if(m_moveRoutePath != null)
        {
            if (Move())
            {
                m_moveRoutePath = null;
            }
        }
    }
}
