using Cinemachine;
using UnityEngine;

namespace KittyFarm
{
    public class CinemachineConfinerInitializer : MonoBehaviour
    {
        private CinemachineConfiner2D confiner;

        private void Awake()
        {
            confiner = GetComponent<CinemachineConfiner2D>();
        }

        private void OnEnable()
        {
            GameManager.MapChanged += OnMapChanged;
        }

        private void OnDisable()
        {
            GameManager.MapChanged -= OnMapChanged;
        }

        private void OnMapChanged()
        {
            var gridCollider = FindObjectOfType<Grid>().GetComponent<PolygonCollider2D>();
            confiner.m_BoundingShape2D = gridCollider;
        }
    }
}