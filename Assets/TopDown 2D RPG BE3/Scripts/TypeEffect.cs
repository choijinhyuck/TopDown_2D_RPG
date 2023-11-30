using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject endCursor;
    public bool isAnim;

    string targetMsg;
    Text msgText;
    AudioSource audioSource;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();

    }
    public void SetMsg(string msg)
    {
        if(isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
            return;
        }
        targetMsg = msg;
        EffectStart();
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        endCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);

        isAnim = true;

        Invoke("EffectIng", interval);
    }
    void EffectIng()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {

            audioSource.Play();

        }
        index++;
        Invoke("EffectIng", interval);
    }
    void EffectEnd()
    {
        isAnim = false;
        endCursor.SetActive(true);
    }
}
