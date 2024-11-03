using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public ChipLoader chipLoader; // Reference to the ChipLoader script
    public Transform[] pathPositions; // Array of waypoints for the chip path
    public Button rollButton;
    public Button resetButton;
    public Image diceImage;
    public Text resultText;
    public Sprite[] diceSprites; // Assign each dice face sprite in the Inspector

    private int lastRoll = 0;

    private void Start()
    {
        // Set the path positions for ChipLoader
        if (chipLoader != null)
        {
            chipLoader.SetPathPositions(pathPositions);
        }

        // Setup button listeners
        rollButton.onClick.AddListener(RollDice);
        resetButton.onClick.AddListener(ResetGame);
    }

    public void SetRollButtonInteractable(bool isInteractable)
    {
        rollButton.interactable = isInteractable;
    }

    public void SetResetButtonInteractable(bool isInteractable)
    {
        resetButton.interactable = isInteractable;
    }

    private void RollDice()
    {
        // Disable both the Roll and Reset buttons during the animation
        SetRollButtonInteractable(false);
        SetResetButtonInteractable(false);

        StartCoroutine(DiceRollAnimation());
    }

    private IEnumerator DiceRollAnimation()
    {
        float animationDuration = 1.5f; // Duration of the rolling animation
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            int randomIndex = UnityEngine.Random.Range(0, diceSprites.Length);
            diceImage.sprite = diceSprites[randomIndex];
            yield return new WaitForSeconds(0.1f); 
            elapsedTime += 0.1f;
        }

        lastRoll = UnityEngine.Random.Range(1, 7); // Roll a random number between 1 and 6
        resultText.text = lastRoll.ToString();
        UpdateDiceImage(lastRoll);

        // Move the chip based on the final result
        if (chipLoader != null)
        {
            chipLoader.MoveChip(lastRoll, this);
        }
    }

    private void ResetGame()
    {
        lastRoll = 0;
        resultText.text = "";
        if (diceSprites.Length > 0)
        {
            diceImage.sprite = diceSprites[0]; // Reset dice image to the first sprite
        }

        // Reset the chip position to the start
        if (chipLoader != null)
        {
            chipLoader.ResetChipPosition(); 
        }
    }

    private void UpdateDiceImage(int number)
    {
        if (number >= 1 && number <= diceSprites.Length)
        {
            diceImage.sprite = diceSprites[number - 1];
        }

        // Re-enable the Roll and Reset buttons after the final roll result is shown
        SetRollButtonInteractable(true);
        SetResetButtonInteractable(true);
    }
}
