using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] float moveTime;
    private RectTransform rt;
    private float height;
    private bool open;
    private bool moving;
    void Start() {
        rt = gameObject.GetComponent<RectTransform>();
        height = rt.rect.height;
        open = false;
        moving = false;
    }

    private void Update() {
        if (Input.GetKeyDown("space")) {
            if (!moving) {
                if (open) {
                    ClosePanel();
                } else {
                    OpenPanel();
                }
            }
        }
    }

    public void OpenPanel() {
        open = true;
        moving = true;
        StartCoroutine(MovePanelOpen(moveTime));

    }
    public void ClosePanel() {
        open = false;
        moving = true;
        StartCoroutine(MovePanelClose(moveTime));
    }

    private IEnumerator MovePanelOpen(float moveTime) {

        Vector2 startPos = rt.anchoredPosition;
        Vector2 endPos = new Vector2(rt.anchoredPosition.x, 0);
        float elapsedTime = 0;


        while (elapsedTime < moveTime) {
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moving = false;
    }

    private IEnumerator MovePanelClose(float moveTime) {

        Vector2 startPos = rt.anchoredPosition;
        Vector2 endPos = new Vector2(rt.anchoredPosition.x, -height);
        float elapsedTime = 0;


        while (elapsedTime < moveTime) {
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moving = false;
    }

    public bool IsOpen() {
        return open;
    }

    public float GetHeight() {
        return height;
    }
    
}
