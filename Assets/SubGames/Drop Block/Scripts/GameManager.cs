using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.DropBlock
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("ColorPlate")]
        [SerializeField] private ColorPlate dropBlockColors;
        [Header("Spawn")]
        [SerializeField] private float yAxisOffest;
        [SerializeField] private Transform spawnerPos;
        [Space]
        [SerializeField] private List<GameObject> blockList;

        [HideInInspector] public bool IsPlay;

        private int index = 0;
        private int currentColorIndex = -1;

        private GameObject go = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            StartGame();
        }

        private void Update()
        {
            if (IsPlay)
            {
                if (Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;

                    if (touches[0].phase == TouchPhase.Began)
                    {
                        OnTap();
                    }
                }

                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnTap();
                }
            }
        }

        private void StartGame()
        {
            IsPlay = true;
            go = blockList[index];
            go.GetComponent<SpriteRenderer>().color = GetColor();
            go.GetComponent<Rigidbody2D>().gravityScale = 0f;
            go.transform.position = spawnerPos.position;
            go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            go.SetActive(true);
        }

        public void OnTap()
        {
            IsPlay = false;

            index++;
            if (index >= blockList.Count) index = 0;

            spawnerPos.position +=  new Vector3(0,yAxisOffest);

            go.GetComponent<BlockScript>().enabled = false;
            go.GetComponent<Rigidbody2D>().gravityScale = 1f;
            Debug.Log("1");

            StartCoroutine(Delay(() =>
            {
                Debug.Log("2");
                go = blockList[index];
                go.GetComponent<SpriteRenderer>().color = GetColor();
                go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                go.GetComponent<Rigidbody2D>().gravityScale = 0f;
                go.transform.position = spawnerPos.position;
                go.transform.rotation = Quaternion.identity;
                go.GetComponent<BlockScript>().enabled = true;

                go.SetActive(true);

                IsPlay = true;
            }));

           

            
        }

        private Color GetColor()
        {
            if(currentColorIndex == -1)
            {
                currentColorIndex = UnityEngine.Random.Range(0, dropBlockColors.colors.Count);
            }
            else
            {
                int _colorTempIndex = UnityEngine.Random.Range(0, dropBlockColors.colors.Count);

                while(_colorTempIndex == currentColorIndex)
                {
                    _colorTempIndex = UnityEngine.Random.Range(0, dropBlockColors.colors.Count);
                }
            }

            return dropBlockColors.colors[currentColorIndex];
        }

        IEnumerator Delay( Action action, float delayDuration = 0.5f)
        {
            yield return new WaitForSeconds(delayDuration);
            action?.Invoke();
        }
        
    }
}