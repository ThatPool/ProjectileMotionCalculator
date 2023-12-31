using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public static Demo instance;

    private void Awake()
    {
        instance = this;
    }

    public LineRenderer lineRenderer;

    public bool showLine = false;
    public bool useRigidbody = false;

    public int iterations;

    public Transform start, mid, end;

    public float step;

    public float timeToReachEnd = 0f;
    public float time = 0f;

    List<Vector3> positions = new List<Vector3>();

    public float theta, f, m, height;
    public const float g = 9.8f;

    [Range(0f, 1f)]
    public float range = 1.0f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ToggleLine();

        CalculateProjectileMotion();
    }

    public bool isSimulating = false;
    public bool isSimulationPaused = false;
    public void StartSimulation()
    {
        CancelSimulation();
        UIStuff.instance.DisableAllEditables();

        // Start the simulation
        isSimulating = true;
    }

    public void CancelSimulation()
    {
        // Stop the simulation
        isSimulating = false;
        isSimulationPaused = false;

        // Reset the simulation parameters
        time = 0;
        step = 0;

        // Reset the object's position to the starting position
        this.transform.position = start.position;

        // Update UI
        UIStuff.instance.UpdateNotEditables();

        UIStuff.instance.EnableAllEditables();

        UIStuff.instance.UpdateEditables();

        UIStuff.instance.ResetUI_Soft();
    }

    private void SimulateProjectileMotion()
    {
        if (time < timeToReachEnd)
        {
            // Increment time
            time += Time.deltaTime;

            // Calculate the interpolation factor based on current time
            float rr = time / timeToReachEnd;
            step = rr;

            // Calculate the current position based on the interpolation factor
            Vector3 newPosition = Vector3.Lerp(Vector3.Lerp(start.position, mid.position, rr), Vector3.Lerp(mid.position, end.position, rr), rr);

            // Move the object to the new position
            if (useRigidbody)
            {
                //rb.mass = m;
                rb.MovePosition(newPosition);
            }
            else
            {
                this.transform.position = newPosition;
            }

            UIStuff.instance.UpdateEditables();
        }
        else
        {
            // Stop the simulation
            isSimulating = false;

            // Reset the simulation parameters
            time = 0;
            step = 0;

            // Update UI
            UIStuff.instance.UpdateNotEditables();

            UIStuff.instance.EnableAllEditables();

            UIStuff.instance.ResetUI_Soft();
        }
    }

    void CalculateProjectileMotion()
    {
        float v0;

        // Initial velocity from Force and Mass
        v0 = getVelocity(f, m);

        // Convert angle to radians (same as in C++ code)
        float theta_radians = DegreeToRadians(theta);

        // Set initial position to (0, 0, 0) (same as in C++ code)
        start.position = new Vector3(0.0f, 0.0f, 0.0f);

        // Calculate time of flight (same as in C++ code)
        float v0y = v0 * Mathf.Sin(theta_radians);
        float time_of_flight = 2 * (v0y / g);

        // Calculate the final position (same as in C++ code)
        end.position = new Vector3(
            start.position.x + (float)(v0 * Mathf.Cos(theta_radians) * time_of_flight),
            start.position.y + (float)(v0 * Mathf.Sin(theta_radians) * time_of_flight - 0.5f * g * Mathf.Pow(time_of_flight, 2)),
            0.0f  // Assuming motion is only in the x-y plane
        );

        float max_height = getMaxHeight(v0, theta_radians); //start.position.y + Mathf.Pow(v0 * Mathf.Sin(temp_theta), 2) / (2.0f * g);

        height = max_height;

        // Calculate the midpoint coordinates (same as in C++ code)
        mid.position = new Vector3(
            (start.position.x + end.position.x) / 2.0f,
            (start.position.y + end.position.y) / 2.0f + max_height,
            (start.position.z + end.position.z) / 2.0f
        );

        timeToReachEnd = time_of_flight;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Global.EditMode = !Global.EditMode;
        }

        CalculateProjectileMotion();

        positions.Clear();

        if (range > 0.0f)
        {
            positions.Add(start.position);
        }
        for (float i = 1; i < iterations; i++)
        {
            float r = i / iterations;
            Vector3 pos = Vector3.Lerp(Vector3.Lerp(start.position, mid.position, r), Vector3.Lerp(mid.position, end.position, r), r);
            positions.Add(pos);
            if (r >= range)
                break;
        }
        if (range >= 1.0f)
        {
            positions.Add(end.position);
        }

        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        if (isSimulating)
        {
            if (!isSimulationPaused)
            {
                SimulateProjectileMotion();
            }
        }
        else
        {
            float rr = step; // / iterations;
            this.transform.position = Vector3.Lerp(Vector3.Lerp(start.position, mid.position, rr), Vector3.Lerp(mid.position, end.position, rr), rr);
        }
    }

    private void LateUpdate()
    {
        UIStuff.instance.UpdateNotEditables();
    }

    public void ToggleLine()
    {
        lineRenderer.gameObject.SetActive(showLine);
    }

    public void ToggleRidigbody()
    {
        Rigidbody comp;
        if (useRigidbody)
        {
            if(!TryGetComponent<Rigidbody>(out comp))
            {
                rb = this.gameObject.AddComponent<Rigidbody>();
                rb.mass = m;
            }
        }
        else
        {
            if (TryGetComponent<Rigidbody>(out comp))
            {
                Destroy(rb);
            }
        }
    }

    public float getMaxHeight(float velocity, float theta)
    {
        return start.position.y + Mathf.Pow(velocity * Mathf.Sin(theta), 2) / (2.0f * g);
    }

    float getVelocity(float force, float mass)
    {
        return force / mass;
    }

    float DegreeToRadians(float theta)
    {
        return theta * Mathf.PI / 180.0f;
    }
}