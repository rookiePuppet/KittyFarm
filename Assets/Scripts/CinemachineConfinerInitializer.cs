using Cinemachine;
using KittyFarm.Service;
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
            SceneLoader.MapLoaded += OnMapLoaded;
        }

        private void OnDisable()
        {
            SceneLoader.MapLoaded -= OnMapLoaded;
        }

        private void OnMapLoaded()
        {
            var gridCollider = FindObjectOfType<Grid>().GetComponent<PolygonCollider2D>();
            confiner.m_BoundingShape2D = gridCollider;
        }
    }
}