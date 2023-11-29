using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이곳에 처음 왔구나?:1" });
        talkData[2000] = new string[] { "1:3", "2:2", "3:0" };
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용한 흔적이 있는 책상이다." });

        talkData.Add(10 + 1000, new string[] { "어서 와:0", "이 마을에 놀라운 전설이 있다는데:1", "오른쪽 호수 쪽에 루도가 알려줄거야.:0" });
        talkData.Add(11 + 2000, new string[] { "어서 가:0", "이 마을에 놀라운 전설이 없다는데:1", "왼쪽 호수 쪽에 루나는 모를거야. 내 동전이나 내놔.:0" });

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1", "저런!:3" });
        talkData.Add(20 + 2000, new string[] { "내 동전:2", "가져다 줘:3" });

        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });
        talkData.Add(21 + 2000, new string[] { "찾아줘서 고마워.:1" });


        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
                
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }

        }

        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
