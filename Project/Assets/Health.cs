using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int count;
    [SerializeField] UnityEvent<int> onCountChanged;
    [SerializeField] UnityEvent onAllLivesLost;

    public int GetCount() => count;

    public void SetCount(int newCount)
    {
        count = newCount;
        onCountChanged.Invoke(newCount);
    }

    public void AddLife()
    {
        count++;
        onCountChanged.Invoke(count);
    }

    public void RemoveLife()
    {
        count--;
        if (count == 0) onAllLivesLost.Invoke();
        else onCountChanged.Invoke(count);
    }


}