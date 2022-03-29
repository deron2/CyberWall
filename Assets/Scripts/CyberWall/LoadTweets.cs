using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Twity.DataModels.Responses;
using Twity.DataModels.Core;

using System.Text.RegularExpressions;


public class LoadTweets : MonoBehaviour
{
    // Start is called before the first frame update

    public List<string> LoadedTweets;
    Regex hashtagRegex = new Regex(@"#(\w+)");
    Regex mentionRegex = new Regex(@"@(\w+)");
    Regex rtRegex = new Regex(@"RT");

    Regex urlRegex = new Regex("(https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9]+\\.[^\\s]{2,}|www\\.[a-zA-Z0-9]+\\.[^\\s]{2,})");

    public string tweetSearchQuery = "#SOSHK";
    public int maximumTweets = 30;

    void Start()
    {
        Twity.Oauth.consumerKey       = "cTuO7Ok5EMJZmx0zNGap19A4j";
        Twity.Oauth.consumerSecret    = "aCsGe55b1PVzKwMnJXCwGmlALTTf6x5H4ozYrQ2jMcYkg5NZSu";
        Twity.Oauth.accessToken       = "160994010-QqbyadLKNaqCAai40RNCMTxl2LrcHYPVEi3Ti4Mh";
        Twity.Oauth.accessTokenSecret = "MINoF6jUR3j0WWdzVQxPusPPdO7UfGLBkkyQNqOlG0vuC";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters ["q"] = tweetSearchQuery;
        parameters ["count"] = maximumTweets.ToString();;
        StartCoroutine (Twity.Client.Get ("search/tweets", parameters, Callback));
    }

    void Callback(bool success, string response) {

        if (success) {
            SearchTweetsResponse Response = JsonUtility.FromJson<SearchTweetsResponse> (response);
            foreach (Tweet tweet in Response.statuses) {
                string tweetText = tweet.text;
                if (rtRegex.Match(tweetText).Length == 0)
                {
                    tweetText = mentionRegex.Replace(tweetText,"");
                    tweetText = urlRegex.Replace(tweetText,"");
                    LoadedTweets.Add(tweetText);
                }
            }
        }             

        else {
            Debug.Log (response);
        }
        this.GetComponentInParent<PlacePostIt>().PlaceNotes();
    }                             

    // Update is called once per frame
    void Update()
    {
        
    }
}
