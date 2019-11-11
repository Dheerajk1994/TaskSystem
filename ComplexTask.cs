using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexTask : Task
{
    protected Queue<Task> taskPrqQueue;
    protected Vector2 mainTaskLocation;
    protected Task currentTask;
    protected bool isValid = false;

    public ComplexTask(string taskName, uint taskValue, GameObject entity, Vector2 mainTaskLocation) : base(taskName, entity)
    {
        this.mainTaskLocation = mainTaskLocation;
        taskPrqQueue = new Queue<Task>();
    }

    public override void Execute(uint workAmount)
    {
        if (isValid)
        {
            currentTask.Execute(workAmount);
        }
        else
        {
            Validate(workAmount);
        }
    }

    public override bool IsValidated()
    {
        return isValid;
    }

    public override void Validate(uint workAmount)
    {
        if(currentTask == null)
        {
            if(taskPrqQueue.Count > 0)
            {
                currentTask = taskPrqQueue.Peek();
                currentTask.TaskCompleted += PrereqCompleted;
                isValid = true;
            }
            else
            {
                OnFinish();
            }
        }
    }

    protected virtual void PrereqCompleted()
    {
        taskPrqQueue.Dequeue();
        if(taskPrqQueue.Count == 0)
        {
            Debug.Log("build task complete");
            OnFinish();
        }
        else
        {
            currentTask = taskPrqQueue.Peek();
        }
    }
}
