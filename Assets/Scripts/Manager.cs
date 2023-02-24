using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Vox
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private GameObject popUp;
        [SerializeField] private TextMeshProUGUI pointText;
        [SerializeField] private List<GameObject> asteroidsList;
        [SerializeField] private byte minAsteroids = 5;
        [SerializeField] private byte maxAsteroids = 10;

        public static bool pause;

        private static byte maxPoints = 15;
        private static byte points;
        private static int asteroidsCount;
        private static bool updatePoints;

        private void Start()
        {
            SetStartingAsteroids();
        }

        void Update()
        {
            if (updatePoints)
            {
                pointText.text = "Punkty: " + points;
                updatePoints = false;
            }

            if (pause)
            {
                popUp.SetActive(true);
            }
            else if (asteroidsCount < minAsteroids)
            {
                int asteroidsAmount = Random.Range(minAsteroids, maxAsteroids+1);
                
                asteroidsCount = asteroidsAmount;

                for (int j = 0; j < asteroidsList.Count; j++)
                {
                    if (asteroidsAmount == 0) break;

                    if (!asteroidsList[j].activeInHierarchy)
                    {
                        asteroidsList[j].SetActive(true);
                    }

                    asteroidsAmount--;
                }
            }
        }

        static public void AddPoints()
        {
            points++;
            asteroidsCount--;
            pause = points == maxPoints;
            updatePoints = true;
        }

        static public void RemoveAsteroid()
        {
            asteroidsCount--;
        }

        public void ResetGame()
        {
            pause = false;
            updatePoints = true;
            points = 0;
            asteroidsCount = minAsteroids;

            SetStartingAsteroids();
            popUp.SetActive(false);
        }

        private void SetStartingAsteroids()
        {
            for (int i = 0; i < maxAsteroids; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i < minAsteroids);
            }
        }
    }
}
