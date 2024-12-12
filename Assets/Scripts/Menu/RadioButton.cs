
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioButton : MonoBehaviour
{
    [SerializeField] private List<string> options;
    [SerializeField] private float startHeight;
    [SerializeField] private float verticalSpacing;
    [SerializeField] private float margin;
    [SerializeField] private float hotizontalSpacing;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Image connectionPrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject selectionBackground;
    [SerializeField] private TextMeshProUGUI selectionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptions()
    {
        selectionBackground.SetActive(false);

        for (int i = 0; i < options.Count; i++)
        {
            GameObject option = Instantiate(optionPrefab);
            option.transform.SetParent(container.transform);
            option.transform.localScale = Vector3.one;
            option.transform.localPosition = new Vector3(margin + hotizontalSpacing, startHeight - i * verticalSpacing, 0);
            int index = i;
            option.GetComponent<Button>().onClick.AddListener(() => SelectOption(index));

            // Set the text of the option
            option.GetComponentInChildren<TextMeshProUGUI>().text = options[i];
        }

        // Vertical connection
        Image connection = Instantiate(connectionPrefab);
        connection.transform.SetParent(container.transform);
        connection.transform.localScale = Vector3.one;
        connection.transform.localPosition = new Vector3(margin, startHeight - 0.5f * verticalSpacing * (options.Count -1), 0);
        connection.rectTransform.sizeDelta = new Vector2(4, (options.Count - 1) * verticalSpacing + 2);

        // Horizontal connection
        Image connection2 = Instantiate(connectionPrefab);
        connection2.transform.SetParent(container.transform);
        connection2.transform.localScale = Vector3.one;
        connection2.transform.localPosition = new Vector3(margin/2, 0, 0);
        connection2.rectTransform.sizeDelta = new Vector2(margin, 4);

    }

    void SelectOption(int index)
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        selectionBackground.SetActive(true);
        selectionText.text = options[index];
    }
}
