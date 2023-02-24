using UnityEngine;
using UnityEngine.EventSystems;

namespace Vox
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] private byte live = 4;
        [SerializeField] private int liveTime;
        [SerializeField] private float clickDelay = 0.5f;

        private Camera myCamera;
        private Material material;
        private float clicked;
        private float clickTime;
        private float startTime;

        void Awake()
        {
            myCamera = Camera.main;
            material = mesh.material;
        }

        private void OnEnable()
        {
            material.color = Color.white;
            live = 4;
            startTime = Time.time;
            RandomParameters();
        }

        void Update()
        {
            if (!Manager.pause)
            {
                CheckLiveTime();
            }
        }

        void OnMouseDown()
        {
            //prevent click through UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            // Detecting double click
            clicked++;

            if (clicked == 1)
            {
                clickTime = Time.time;
            }
            else if (clicked > 1 && Time.time - clickTime < clickDelay)
            {
                //double click
                ReduceLive();
                clicked = 0;
                clickTime = 0;
            }
            else if (Time.time - clickTime > 1)
            {
                clicked = 0;
            }
        }

        private void RandomParameters()
        {
            transform.localPosition = myCamera.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
            transform.rotation = Random.rotation;
            liveTime = Random.Range(1, 4);
        }

        private void CheckLiveTime()
        {
            if (liveTime <= Time.time - startTime)
            {
                startTime = Time.time;
                Manager.RemoveAsteroid();
                gameObject.SetActive(false);
            }
        }

        private void ReduceLive()
        {
            live--;

            switch (live)
            {
                case 3:
                    material.color = Color.red;
                    break;

                case 2:
                    material.color = Color.green;
                    break;

                case 1:
                    material.color = Color.blue;
                    break;

                case 0:
                    Manager.AddPoints();
                    gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
        }
    }
}