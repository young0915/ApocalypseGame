using UnityEngine;
using UnityEngine.EventSystems;

// 문 클릭시 발동되는 이벤트.
public class CDoorAction : MonoBehaviour
{

    private EventTrigger _EventTrigger;

    public void InstallCity()
    {
        _EventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry MouseClick = new EventTrigger.Entry();
        MouseClick.eventID = EventTriggerType.PointerClick;
        MouseClick.callback.AddListener((data) => OnClickBunker());

        _EventTrigger.triggers.Add(MouseClick);

    }

    private void OnClickBunker()
    {
        CSceneManager.Inst.OnSceneMovement(CDataManager.m_strBunkerScene);
        CCameraManager.Inst.SetCameraView(EmCameraType.Bunker);
        CWheatherManager.Inst.SetTurnOnOrOff(false);
    }



    public void InstallBunker()
    {
        _EventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry MouseOutClick = new EventTrigger.Entry();

        MouseOutClick.eventID = EventTriggerType.PointerClick;
        MouseOutClick.callback.AddListener((data) => OnClickOutSideCity());

        _EventTrigger.triggers.Add(MouseOutClick);
    }

  
    private void OnClickOutSideCity()
    {
       StartCoroutine(CUIManager.Inst.CorWarning(EmWarningType.SceneChange));
    }

}
