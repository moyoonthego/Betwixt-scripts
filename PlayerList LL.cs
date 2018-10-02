using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Node
{
    public Node next;
    public string name;
    public string score;
}

public class LinkedList {
    public Node head = null;

    public void Save()
    {
        int counter = 0;
        Node current = head;
        while (current != null)
        {
            counter++;
            //Convert to Json
            string jsonData = JsonUtility.ToJson(current);
            //Save Json string
            string keepname = string.Format("HighScores{0}", counter);
            PlayerPrefs.SetString(keepname, jsonData);
            PlayerPrefs.Save();
            current = current.next;
        }
    }

    public Node Load()
    {
        Node firstnode = null;
        int count = 0;
        List<Node> scores = new List<Node>();
        bool abort = false;
        string strscore;
        string jsonData;
        string dne = "Non existent";
        bool compare = false;
        while (abort == false)
        {
            count++;
            strscore = string.Format("HighScores{0}", count);
            //Load saved Json
            jsonData = PlayerPrefs.GetString(strscore, dne);
            compare = jsonData.Equals(dne);
            //Convert to Class
            if (compare)
            {
                abort = true;
            } else
            {
                Node loadedhead = JsonUtility.FromJson<Node>(jsonData);
                scores.Add(loadedhead);
            }

        }
        count = 0;
        if (scores.Count != 0)
        {
            for(int i = 0; i < scores.Count; i++)
            {
                count++;
                if (count != scores.Count) {
                    scores[i].next = scores[count];
                } else
                {
                    scores[i].next = null;
                }
            }
            firstnode = scores[0];
        }
        return firstnode;
    }

    public void Add(string name, string score)
    {
        head = Load();
        Node toAdd = new Node();
        toAdd.name = name;
        toAdd.score = score;
        Node current = head;
        Node prev = null;
        //empty
        if (current == null)
        {
            head = toAdd;
        }
        //new head
        else if (int.Parse(toAdd.score) > int.Parse(current.score))
        {
            head = toAdd;
            head.next = current;
        }
        else
        {
            while ((current != null) && (int.Parse(toAdd.score) <= int.Parse(current.score)))
            {
                prev = current;
                current = current.next;
            }
            if (prev != null) {
                prev.next = toAdd;
            }
            toAdd.next = current;
        }
        MaintainOrder();
        Save();
    }

    public void MaintainOrder()
    {
        //essentially removing more than 15 entries for high score!
        int counter = 0;
        Node current = head;
        while (current != null)
         {
             counter++;
             if (counter >= 15)
             {
                current.next = null;
             }
             current = current.next;
         }
    }
}

public class PlayerListLL : MonoBehaviour {

    public static LinkedList players = new LinkedList();
    public static string scorepage;

    public static void Start()
    {
        scorepage = "";
    }

    public static string printAll()
    {
        Node playerhead = players.head;
        while (playerhead != null)
        {
            scorepage += string.Format("Player {0} \t \t Score: {1}\n", playerhead.name, playerhead.score);
            playerhead = playerhead.next;
        }
        return scorepage;
    }

}
