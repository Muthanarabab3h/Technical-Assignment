# Tamaten Assignment - Ludo Game

This Unity project demonstrates a simple Ludo game scene as part of the Tamaten assignment. It includes interactive UI, animations, and Addressables integration.

## Features
- **Roll Dice Button**: Simulates a dice roll with an animation and displays the result.
- **Reset Button**: Resets the chip to its starting position.
- **Chip Movement**: Moves the chip based on the dice roll result.
- **Addressables**: Loads dice and chip images through Unity's Addressables system.

## Setup Instructions
1. Clone this repository.
2. Open the project in **Unity (Version X.Y.Z)**.
3. Set up Addressables:
   - Ensure that the Addressables system is enabled and configured.
   - Rebuild Addressables if necessary (Window > Asset Management > Addressables > Groups > Build > New Build).
4. Run the game in Unity Editor or build for Android/iOS.

## Usage Instructions
- **Roll the Dice**: Click the "Roll" button to roll the die and move the chip based on the result.
- **Reset**: Click the "Reset" button to reset the chip position.
  
## Platform Compatibility
- Compatible with Android/iOS. Tested in Unity's Simulator.

## Additional Notes
- **API Integration (Optional)**: [If applicable] This project uses an external API for random number generation. You may need an internet connection for API-based dice rolls.
- **Code Organization**: Scripts are organized for readability and maintainability, with separate classes for game logic and UI handling.
