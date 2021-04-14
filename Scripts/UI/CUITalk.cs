using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CUITalk : MonoBehaviour
{
    [SerializeField] private CUITalkScrollView ins_cUITalkScrollView;
    [SerializeField] private Button ins_btnNextChat;

    private int _nId = 0;

   public void OnClickNextChat()
    {
        ins_cUITalkScrollView.AddChatAt(_nId);
        _nId += 1;

        if(_nId ==16)
        {
            ins_btnNextChat.enabled = false;
        }
    }
}
