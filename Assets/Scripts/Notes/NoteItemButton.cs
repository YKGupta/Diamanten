using System;
using UnityEngine;

public class NoteItemButton : MonoBehaviour
{
    public Action<NoteItemButton> onButtonClick;

    public void OnClick()
    {
        onButtonClick?.Invoke(this);
    }
}
