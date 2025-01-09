using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private LockTarget targetLocker;
    [SerializeField] private float buttonSpacing;
    public AMission mission;

    void Start()
    {
        targetLocker = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LockTarget>();

        
        List<WayPoint> wayPoints = mission.WayPoints;

        for (int i = 0; i < wayPoints.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonPanel.transform, false);
            button.transform.position += i * new Vector3(0, -buttonSpacing, 0);

            // Set button text
            button.GetComponentInChildren<TextMeshProUGUI>().text = wayPoints[i].name;

            GameObject target = wayPoints[i].target;
            button.GetComponent<Button>().onClick.AddListener(() => targetLocker.SetLock(target));
        }
    }
}
