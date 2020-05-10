using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A timer
/// </summary>
public abstract class Timer : MonoBehaviour
{
	#region Fields

	float totalSeconds = 0;

	float elapsedSeconds = 0;
	bool running = false;

	int previousCountdownValue;

	bool started = false;

	#endregion

	#region Properties

	public float Duration
    {
		set
        {
			if (!running)
            {
				totalSeconds = value;
			}
		}
	}

	public bool Finished
    {
		get { return started && !running; } 
	}

	public bool Running
    {
		get { return running; }
	}

	#endregion

	protected abstract void InvokeFinishEvent();

	#region Public methods

	void Update()
    {
		// update timer
		if (running)
        {
			elapsedSeconds += Time.deltaTime;

			int newCountdownValue = GetCurrentCountdownValue();
			if (newCountdownValue != previousCountdownValue)
            {
				previousCountdownValue = newCountdownValue;
			}

			if (elapsedSeconds >= totalSeconds)
            {
				running = false;
				InvokeFinishEvent();
			}
		}
	}


    public void Run()
    {
		// only run with valid duration
		if (totalSeconds > 0)
        {
			started = true;
			running = true;
			elapsedSeconds = 0;

			previousCountdownValue = GetCurrentCountdownValue();
		}
	}


	public void Stop()
    {
		started = false;
		running = false;
	}

	#endregion


	#region Private methods

	int GetCurrentCountdownValue()
    {
		return (int)Mathf.Ceil(totalSeconds - elapsedSeconds);
	}

	#endregion
}
