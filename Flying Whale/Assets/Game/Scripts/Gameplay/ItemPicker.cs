using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EItemType
{
    one = 0,
    two = 1,
    three = 2,
    four = 3
}
public class ItemPicker : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item1"))
            Picked(0, other.gameObject);
        if (other.gameObject.CompareTag("Item2"))
            Picked(1, other.gameObject);
        if (other.gameObject.CompareTag("Item3"))
            Picked(2, other.gameObject);
        if (other.gameObject.CompareTag("Item4"))
            Picked(3, other.gameObject);

        if (other.gameObject.CompareTag("ItemSpecial"))
        {
            AudioManager.Instance.Play(AudioManager.Instance.m_AudioInfo.ItemSpecial);
            GameManager.Instance.PickedItemSpecial();
        }
    }

    void Picked(int _index, GameObject _obj)
    {
        GameManager.Instance.PickedItem(_index);
        Destroy(_obj);

        AudioManager.Instance.Play(AudioManager.Instance.m_AudioInfo.Item, false, 1 - Random.value * 0.5f);
    }
}
