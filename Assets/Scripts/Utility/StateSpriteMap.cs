using System;
using System.Collections.Generic;
using UnityEngine;

public class StateSpriteMap : MonoBehaviour
{
    [Serializable]
    public class StateSprite
    {
        public TargetState state;
        public Sprite sprite;
    }

    public StateSprite[] entries;

    public Dictionary<TargetState, Sprite> map;

    private void Awake()
    {
        map = new Dictionary<TargetState, Sprite>();
        foreach(StateSprite i in entries)
        {
            map.Add(i.state, i.sprite);
        }
    }
}
