using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject endTxt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;

    float time;
    float limit = 30.0f;
    public static gameManager I;
    private void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        int[] mes = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        mes = mes.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {

            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string meName = "me" + mes[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(meName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > limit)
        {
            GameEnd();
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardLeft = GameObject.Find("cards").transform.childCount;
            if (cardLeft  == 2)
            {
                GameEnd();
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }
    void GameEnd()
    {
        Time.timeScale = 0.0f;
        endTxt.SetActive(true);
    }
}
