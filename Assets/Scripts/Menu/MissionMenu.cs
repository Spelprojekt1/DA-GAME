using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMenu : MonoBehaviour
{
    [SerializeField] private GameObject missionPanelPrefab;
    [SerializeField] private MissionManager missionManager;
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
            panel.transform.position += i * new Vector3(350,0,0);
        }
    }
}