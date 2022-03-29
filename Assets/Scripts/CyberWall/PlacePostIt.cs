using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlacePostIt : MonoBehaviour
{
    GameObject m_Cyberwall;
    bool notesPlaced = false;

    public GameObject m_PostitPrefab;

    public List<Material> materialList;

    public List<String> m_Notes;

    public List<String> m_NotesDeleted;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("cyberwall") && notesPlaced == false)
        {
            m_Cyberwall = GameObject.FindGameObjectWithTag("cyberwall");
            PlaceNotes();
            Debug.Log("Found Wall");
        }
    }

    public void PlaceNotes()
    {
        m_Notes = GetComponentInParent<LoadTweets>().LoadedTweets;

        int i = 0;
        foreach (string note in m_Notes)
        {

            GameObject spawnedNote = Instantiate(m_PostitPrefab);
            spawnedNote.transform.SetParent(m_Cyberwall.transform);
            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-1.4f, 1.4f), UnityEngine.Random.Range(0.2f, 1.8f), 0.05f);

            Collider[] hitColliders = Physics.OverlapBox(m_Cyberwall.transform.TransformPoint(spawnPos), new Vector3(0.1f,0.1f,0.01f), Quaternion.identity);
            Debug.Log(hitColliders.Length);

            if (hitColliders.Length > 0)
            {
                spawnPos.z += 0.01f*(hitColliders.Length);
                Debug.Log(hitColliders.Length);
            }
            spawnedNote.transform.localPosition = spawnPos;
            spawnedNote.GetComponentInChildren<Renderer>().material = materialList[i % materialList.Count];
            spawnedNote.GetComponentInChildren<Text>().text = note;
            i++;
        }
        Debug.Log("Notes placed");
        notesPlaced = true;
    }
}
