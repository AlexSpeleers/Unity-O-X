using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private MovingLogic main;
    public CrossOrCircle? innerValue;
    public GameObject CrossOrCircleGO;
    public Button button;

    private void Awake()
    {
        main = GameObject.FindObjectOfType<MovingLogic>();
    }
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            main.PlayerMove(gameObject);
        });

        Debug.Log(main);
    }

    public void SpawnFigure(CrossOrCircle crossOrCircle)
    {
        CrossOrCircleGO = Instantiate(
            crossOrCircle == CrossOrCircle.Circle ? GameManager.instance.circlePrefab : GameManager.instance.crossPrefab,
            transform.position,
            Quaternion.identity);
        CrossOrCircleGO.transform.localScale *= 0.4f;
        CrossOrCircleGO.transform.SetParent(this.gameObject.transform);
        gameObject.GetComponent<Button>().interactable = false;
        innerValue = crossOrCircle;
        Debug.Log(crossOrCircle.ToString() + " is spawned");
    }
    public void OnDestroy()
    {
        button.onClick.RemoveListener(delegate { main.PlayerMove(gameObject); });
    }
}
