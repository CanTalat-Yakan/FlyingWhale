using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetRessource : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_tmpro;
    [SerializeField] EItemType m_itemType;


    void Update() { m_tmpro.text = GameManager.Instance.m_ItemCounter[(int)m_itemType].ToString(); }
}
