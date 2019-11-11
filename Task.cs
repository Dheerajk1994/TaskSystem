using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public string taskName { get; private set; }
    public bool isDone { get; protected set; }
    public GameObject entity;
    
    public event Action TaskCompleted = delegate { };

    public Task(string taskName, GameObject entity)
    {
        this.taskName = taskName;
        this.entity = entity;
    }

    public abstract bool IsValidated();
    public abstract void Validate(uint workAmount);
    public abstract void Execute(uint workAmount);

    protected virtual void OnFinish()
    {
        isDone = true;
        TaskCompleted();
    }
}
