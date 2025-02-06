using System;
using UnityEngine;

public class NoteItemButton : MonoBehaviour
{
    public Action<NoteItemButton> onButtonClick;

    public void OnClick()
    {
        SoundManager.PlaySound(SoundType.Click);
        onButtonClick?.Invoke(this);
    }
}
