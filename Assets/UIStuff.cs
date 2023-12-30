using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStuff : MonoBehaviour
{
    public static UIStuff instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Editable Sliders")]
    [SerializeField]
    private Slider angleSlider;
    [SerializeField]
    private Slider rangeSlider;
    [SerializeField]
    private Slider stepSlider;

    [Header("Editable Inputs")]
    [SerializeField]
    private TMP_InputField iterationsInput;
    [SerializeField]
    private TMP_InputField rangeInput;
    [SerializeField] 
    private TMP_InputField timeInput;
    [SerializeField]
    private TMP_InputField forceInput;
    [SerializeField]
    private TMP_InputField massInput;
    [SerializeField]
    private TMP_InputField angleInput;
    [SerializeField]
    private TMP_InputField stepInput;
    [SerializeField]
    private Toggle showTrajectoryToggle;
    [SerializeField]
    private Toggle toggleRigidbody;

    [Header("Not Editable Texts")]
    [SerializeField]
    private TMP_Text currentPos;
    [SerializeField]
    private TMP_Text initPos;
    [SerializeField]
    private TMP_Text midPos;
    [SerializeField]
    private TMP_Text finalPos;
    [SerializeField]
    private TMP_Text maxHeight;
    [SerializeField]
    private TMP_Text totalTime;

    [Header("Action Buttons")]
    [SerializeField]
    private Button startSimulateButton;
    [SerializeField]
    private Button cancelSimulateButton;

    // Start is called before the first frame update
    void Start()
    {
        angleSlider.value = Demo.instance.theta;
        angleInput.text = angleSlider.value.ToString();
        angleSlider.onValueChanged.AddListener(delegate { AngleSliderValueChange(); });
        angleInput.onValueChanged.AddListener(delegate { AngleInputValueChange(); });

        rangeSlider.value = Demo.instance.range;
        rangeInput.text = rangeSlider.value.ToString();
        rangeSlider.onValueChanged.AddListener(delegate { RangeSliderValueChange(); });
        rangeInput.onValueChanged.AddListener(delegate { RangeInputValueChange(); });

        stepSlider.value = Demo.instance.step;
        stepInput.text = stepSlider.value.ToString();
        stepSlider.onValueChanged.AddListener(delegate { StepSliderValueChange(); });
        stepInput.onValueChanged.AddListener(delegate { StepInputValueChange(); });

        iterationsInput.text = Demo.instance.iterations.ToString();
        iterationsInput.onEndEdit.AddListener(delegate { IterationsInputEndEdit(); });

        timeInput.text = Demo.instance.time.ToString();
        timeInput.onEndEdit.AddListener(delegate { TimeInputEndEdit(); });
        
        forceInput.text = Demo.instance.f.ToString();
        forceInput.onEndEdit.AddListener(delegate { ForceInputEndEdit(); });

        massInput.text = Demo.instance.m.ToString();
        massInput.onEndEdit.AddListener(delegate { MassInputEndEdit(); });

        showTrajectoryToggle.onValueChanged.AddListener(delegate { Demo.instance.showLine = showTrajectoryToggle.isOn; Demo.instance.ToggleLine(); });

        toggleRigidbody.onValueChanged.AddListener(delegate { Demo.instance.useRigidbody = toggleRigidbody.isOn; Demo.instance.ToggleRidigbody(); });

        startSimulateButton.onClick.AddListener(delegate { Demo.instance.StartSimulation(); });
        cancelSimulateButton.onClick.AddListener(delegate { Demo.instance.CancelSimulation(); });
    }

    public void DisableAllEditables()
    {
        angleSlider.interactable = false;
        stepSlider.interactable = false;
        iterationsInput.interactable = false;
        timeInput.interactable = false;
        forceInput.interactable = false;
        massInput.interactable = false;
        angleInput.interactable = false;
        stepInput.interactable = false;
        startSimulateButton.interactable = false;
    }
    public void EnableAllEditables()
    {
        angleSlider.interactable = true;
        stepSlider.interactable = true;
        iterationsInput.interactable = true;
        timeInput.interactable = true;
        forceInput.interactable = true;
        massInput.interactable = true;
        angleInput.interactable = true;
        stepInput.interactable = true;
        startSimulateButton.interactable = true;
    }

    public void UpdateNotEditables()
    {
        currentPos.text = "Current Position: " + Demo.instance.gameObject.transform.position.ToString();

        initPos.text = "Init Position: " + Demo.instance.start.position.ToString();
        midPos.text = "Mid Position: " + Demo.instance.mid.position.ToString();
        finalPos.text = "Final Position: " + Demo.instance.end.position.ToString();

        maxHeight.text = "Max Height: " + Demo.instance.height.ToString();

        totalTime.text = "Total Time: " + Demo.instance.timeToReachEnd.ToString();
    }

    void IterationsInputEndEdit()
    {
        int value;
        if (int.TryParse(iterationsInput.text, out value))
        {
            if (value < 2)
            {
                value = 2;
            }
            Demo.instance.iterations = value;
        }
        iterationsInput.text = Demo.instance.iterations.ToString();
    }

    void TimeInputEndEdit()
    {
        int value;
        if (int.TryParse(timeInput.text, out value))
        {
            if (value < 0)
            {
                value = 0;
            }
            Demo.instance.time = value;
        }
        timeInput.text = Demo.instance.time.ToString();
    }

    void ForceInputEndEdit()
    {
        float value;
        if (float.TryParse(forceInput.text, out value))
        {
            if (value < 0)
            {
                value = 0;
            }
            Demo.instance.f = value;
        }
        forceInput.text = Demo.instance.f.ToString();
    }

    void MassInputEndEdit()
    {
        float value;
        if (float.TryParse(massInput.text, out value))
        {
            if (value < 0)
            {
                value = 0;
            }
            Demo.instance.m = value;
        }
        massInput.text = Demo.instance.m.ToString();
    }

    void StepSliderValueChange()
    {
        stepInput.text = stepSlider.value.ToString();
        Demo.instance.step = stepSlider.value;
    }
    void StepInputValueChange()
    {
        float value;
        if(float.TryParse(stepInput.text, out value)) {
            if(value < stepSlider.minValue)
            {
                value = stepSlider.minValue;
            }
            if (value > stepSlider.maxValue)
            {
                value = stepSlider.maxValue;
            }
            stepSlider.value = value;
            Demo.instance.step = stepSlider.value;
        }
        else
        {
            stepInput.text = stepSlider.value.ToString();
        }
    }

    void AngleSliderValueChange()
    {
        angleInput.text = angleSlider.value.ToString();
        Demo.instance.theta = angleSlider.value;
    }
    void AngleInputValueChange()
    {
        float value;
        if (float.TryParse(angleInput.text, out value))
        {
            if (value < angleSlider.minValue)
            {
                value = angleSlider.minValue;
            }
            if (value > angleSlider.maxValue)
            {
                value = angleSlider.maxValue;
            }
            angleSlider.value = value;
            Demo.instance.theta = angleSlider.value;
        }
        else
        {
            angleInput.text = angleSlider.value.ToString();
        }
    }


    void RangeSliderValueChange()
    {
        rangeInput.text = rangeSlider.value.ToString();
        Demo.instance.range = rangeSlider.value;
    }
    void RangeInputValueChange()
    {
        float value;
        if (float.TryParse(rangeInput.text, out value))
        {
            if (value < rangeSlider.minValue)
            {
                value = rangeSlider.minValue;
            }
            if (value > rangeSlider.maxValue)
            {
                value = rangeSlider.maxValue;
            }
            rangeSlider.value = value;
            Demo.instance.range = rangeSlider.value;
        }
        else
        {
            rangeInput.text = rangeSlider.value.ToString();
        }
    }

}
