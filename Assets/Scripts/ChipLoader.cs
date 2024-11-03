using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class ChipLoader : MonoBehaviour
{
    public AssetReference chipPrefabReference; // Addressable reference to the chip 
    public Transform spawnPoint; 
    private Transform[] pathPositions; 

    private GameObject loadedChip; 
    private int currentPathIndex = 0; // Tracks the chip position on the path
    private bool isMoving = false; 
    public float moveDuration = 0.5f; // Duration of movement between waypoints

    private void Start()
    {
        LoadChip();
    }

    private void LoadChip()
    {
        chipPrefabReference.LoadAssetAsync<GameObject>().Completed += OnChipLoaded;
    }

    private void OnChipLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            // Instantiate the loaded chip GameObject at the spawn point
            loadedChip = Instantiate(obj.Result, spawnPoint.position, Quaternion.identity);
            Debug.Log("Chip loaded successfully");
        }
        else
        {
            Debug.LogError("Failed to load the chip");
        }
    }

    // Method to set the path positions from the GameManager
    public void SetPathPositions(Transform[] waypoints)
    {
        pathPositions = waypoints;
        currentPathIndex = 0; 
    }

    public void MoveChip(int steps, GameManager gameManager)
    {
        if (isMoving || loadedChip == null || pathPositions == null) return;

        // Disable both the Roll and Reset buttons in the Inspector
        gameManager.SetRollButtonInteractable(false);
        gameManager.SetResetButtonInteractable(false);

        StartCoroutine(MoveChipCoroutine(steps, gameManager));
    }

    private IEnumerator MoveChipCoroutine(int steps, GameManager gameManager)
    {
        isMoving = true;

        for (int i = 0; i < steps; i++)
        {
            if (currentPathIndex < pathPositions.Length - 1)
            {
                currentPathIndex++;
                Vector3 nextPosition = pathPositions[currentPathIndex].position;
                yield return StartCoroutine(SmoothMove(nextPosition, moveDuration)); // Use moveDuration for smooth movement
            }
            else
            {
                Debug.Log("You won!"); 
                break;
            }
        }

        isMoving = false;

        // Re-enable both the Roll and Reset buttons after moving
        gameManager.SetRollButtonInteractable(true);
        gameManager.SetResetButtonInteractable(true);
    }

    private IEnumerator SmoothMove(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = loadedChip.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            loadedChip.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        loadedChip.transform.position = targetPosition; // Ensure final position is set
    }

    // Method to reset the chip position immediately to the start
    public void ResetChipPosition()
    {
        if (loadedChip != null && pathPositions.Length > 0)
        {
            currentPathIndex = 0;
            loadedChip.transform.position = pathPositions[0].position; // Move immediately to the first waypoint
        }
    }

   
    private void OnDestroy()
    {
        if (loadedChip != null)
        {
            Addressables.ReleaseInstance(loadedChip);
        }
    }
}
