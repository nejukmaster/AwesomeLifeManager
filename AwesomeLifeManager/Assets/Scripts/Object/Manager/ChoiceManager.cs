using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : Manager
{
    public static ChoiceManager instance;

    public Dictionary<string, Choice> choices = new Dictionary<string, Choice>();
    public Dictionary<string, System.Action<MonoBehaviour>> choices_rewards = new Dictionary<string, System.Action<MonoBehaviour>>();
    public override void Init()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
    }

    void mapping()
    {
        List<Dictionary<string, object>> choice_data = CSVReader.Read("DataSheet/Choice");
        choices = new Dictionary<string, Choice>();
        for (int i = 0; i < choice_data.Count; i++)
        {
            choices[choice_data[i]["code"].ToString()] = new Choice(choice_data[i]["main text"].ToString(),
                                                                    choice_data[i]["output message"].ToString(),
                                                                    choices_rewards[choice_data[i]["code"].ToString()]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
