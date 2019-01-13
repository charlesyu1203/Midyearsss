using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touch : MonoBehaviour {
    private ScoreTracker score;
    [SerializeField] private AudioClip audio;
    private AudioSource m_AudioSource;
    private void Start()
    {
        score = GetComponent<ScoreTracker>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Maze");
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            m_AudioSource.clip = audio;
            m_AudioSource.Play();
            collision.transform.gameObject.SetActive(false);
            score.addScore();
        }
    }
}
