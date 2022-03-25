using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStatReader : MonoBehaviour
{
    public TextAsset defaultStats;
    // Start is called before the first frame update
    [System.Serializable]
    public class Character
    {
        public string name;
        public int[] statTypes;
    }
    [System.Serializable]
    public class DefaultStatList
    {
        public Character[] DefaultStats;
    }

    public DefaultStatList defaultStatsList= new DefaultStatList();
    // Update is called once per frame
    void Start()
    {
        defaultStatsList = JsonUtility.FromJson<DefaultStatList>(defaultStats.text);
    }
}
