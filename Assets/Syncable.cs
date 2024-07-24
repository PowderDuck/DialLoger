using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface Syncable<T>
{
    public void Sync(T value);
}
