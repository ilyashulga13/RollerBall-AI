using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class RollerAgent : Agent
{
    public Rigidbody rBody;
    public Transform target;
    public float speed = 10f;

    private float previousDistance;

    public override void OnEpisodeBegin()
    {
        rBody.linearVelocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;

        transform.position = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));

        previousDistance = Vector3.Distance(transform.position, target.position);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.position - transform.position);
        sensor.AddObservation(rBody.linearVelocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        rBody.AddForce(new Vector3(moveX, 0, moveZ) * speed);

        float currentDistance = Vector3.Distance(transform.position, target.position);

        if (currentDistance < previousDistance)
        {
            AddReward(0.1f);
        }
        else
        {
            AddReward(-0.1f);
        }

        previousDistance = currentDistance;

        if (transform.position.y < -2f)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            AddReward(5.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}