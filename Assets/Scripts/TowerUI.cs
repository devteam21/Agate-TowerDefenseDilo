using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class TowerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    [SerializeField] private Image _towerIcon;

    private Tower _towerPrefab;
    private Tower _currentSpawnedTower;

    public void OnBeginDrag (PointerEventData eventData)
    {
        GameObject newTowerObj = Instantiate (_towerPrefab.gameObject);
        _currentSpawnedTower = newTowerObj.GetComponent<Tower> ();
        _currentSpawnedTower.ToggleOrderInLayer (true);
    }

     public void SetTowerPrefab (Tower tower)
    {
        _towerPrefab = tower;
        _towerIcon.sprite = tower.GetTowerHeadIcon ();
    }

     public void OnEndDrag (PointerEventData eventData)
    {
        if (_currentSpawnedTower.PlacePosition == null)
        {
            Destroy (_currentSpawnedTower.gameObject);
        }
        else
        {
            _currentSpawnedTower.LockPlacement ();
            _currentSpawnedTower.ToggleOrderInLayer (false);
            LevelManager.Instance.RegisterSpawnedTower (_currentSpawnedTower);
            _currentSpawnedTower = null;
        }
    }

    public void OnDrag (PointerEventData eventData)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint (mousePosition);

        _currentSpawnedTower.transform.position = targetPosition;
    }

   
}