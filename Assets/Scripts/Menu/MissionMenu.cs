using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionMenu : MonoBehaviour
{
    [SerializeField] private GameObject missionPanelPrefab;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private LockTarget targetLocker;
    void OnEnable()
    {
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < missionManager.activeMissions.Count; i++)
        {
            GameObject panel = Instantiate(missionPanelPrefab);
            panel.transform.SetParent(transform, false);
            panel.transform.position += i * new Vector3(420,0,0);

            GameObject target = missionManager.activeMissions[i].Target;
            panel.GetComponentInChildren<Button>().onClick.AddListener(() => targetLocker.SetLock(target));
            
        }
    }
}