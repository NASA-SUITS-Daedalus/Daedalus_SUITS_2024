using UnityEngine;
using System.Collections;

public class SampleProcedure : MonoBehaviour
{
	// The "head" of the Task linked list
	public Task firstTask;

	// Use this for initialization
	void Start()
	{
		firstTask.makeActive();

    }

}