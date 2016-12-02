using UnityEngine;
using System.Collections;

public class GenericTimer  {

    float interval;
    float timeTrack = 0;
	

    public void Reset() { timeTrack = 0; }
    public bool Enabled { get; set; }
    public bool IncrementIfEnabled()
    {
        if (!Enabled) return false;
        timeTrack += Time.deltaTime;
        if (timeTrack > interval)
        {
            while (timeTrack > interval)
                timeTrack -= interval;
            return true;
        }
        return false;
    }

    public GenericTimer(float interval, bool enable)
    {
        this.interval = interval;
        Enabled = enable;
    }
}
