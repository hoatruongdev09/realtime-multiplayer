using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorizeOnUnitSpawned;
    public static event Action<Unit> AuthorizeOnUnitDespawned;

    public UnityEvent onSelect = new UnityEvent();
    public UnityEvent onDeselect = new UnityEvent();

    public virtual void ReceiveCommand(BaseCommandData commandData)
    {

    }

    public void Select()
    {
        onSelect?.Invoke();
    }
    public void Deselect()
    {
        onDeselect?.Invoke();
    }

    #region  Server
    public override void OnStartServer()
    {
        base.OnStartServer();
        ServerOnUnitSpawned?.Invoke(this);
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        ServerOnUnitDespawned?.Invoke(this);
    }
    #endregion

    #region  Client
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isClientOnly || !hasAuthority) { return; }
        AuthorizeOnUnitSpawned?.Invoke(this);
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        if (!isClientOnly || !hasAuthority) { return; }
        AuthorizeOnUnitDespawned?.Invoke(this);
    }
    #endregion
}


