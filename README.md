# Unity ML-Agents: RollerBall AI (Reinforcement Learning)

This project demonstrates training an AI agent using Unity ML-Agents and PyTorch. The agent (a sphere) learns to autonomously find the shortest path to a target (a cube) by applying physics and utilizing the PPO (Proximal Policy Optimization) algorithm.

## Technologies Used

- Unity 6 (or your specific version, e.g., Unity 2022.3)
- ML-Agents Release 21 (ml-agents 1.1.0)
- Python 3.10
- PyTorch

## Environment Setup

**Observations: 6**
- 3 values: Direction to the target (vector).
- 3 values: Current linear velocity of the sphere.

**Actions: 2 (Continuous)**
- Force applied along the X and Z axes.

**Rewards:**
- `+0.1` — Agent moves closer to the target.
- `-0.1` — Agent moves away from the target.
- `+5.0` — Agent touches the target cube (success).
- `-1.0` — Agent falls off the platform.

## Project Structure

- `Assets/Scripts/RollerAgent.cs` — The agent's control script.
- `results/Run1/RollerBall.onnx` — The trained neural network model (ready for inference in Unity).
- `results/Run1/RollerBall/` — Training checkpoints and TensorBoard logs.

## How to Train

1. Open the scene in the Unity Editor.
2. Ensure the `Behavior Type` on the Agent is set to `Default`.
3. Run the following command in your terminal:

4. py -3.10 -m mlagents.trainers.learn --run-id=Run1 --time-scale=10

5. Press Play in the Unity Editor to start training.
  
## How to Run Inference (Test the Model)

1. In the Agent's Inspector, change `Behavior Type` to `Inference Only`.
2. Drag and drop the `RollerBall.onnx` file into the `Model` field of the `Behavior Parameters` component.
3. Press Play — the sphere will now autonomously navigate to the target.
