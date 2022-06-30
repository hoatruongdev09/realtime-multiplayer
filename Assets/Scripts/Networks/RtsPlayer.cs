using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class RtsPlayer : NetworkBehaviour
{
    private List<Unit> units = new List<Unit>();

    #region  Server

    public override void OnStartServer()
    {
        base.OnStartServer();
        Unit.ServerOnUnitSpawned += OnServerUnitSpawned;
        Unit.ServerOnUnitDespawned += OnServerUnitDespawned;

    }



    public override void OnStopServer()
    {
        base.OnStopServer();
        Unit.ServerOnUnitSpawned -= OnServerUnitSpawned;
        Unit.ServerOnUnitDespawned -= OnServerUnitDespawned;
    }

    #endregion

    #region  Client

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isClientOnly) { return; }
        Unit.AuthorizeOnUnitSpawned += OnAuthorizeUnitSpawned;
        Unit.AuthorizeOnUnitDespawned += OnAuthorizeUnitDespawned;
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        if (!isClientOnly) { return; }
        Unit.AuthorizeOnUnitSpawned -= OnAuthorizeUnitSpawned;
        Unit.AuthorizeOnUnitDespawned -= OnAuthorizeUnitDespawned;
    }

    #endregion

    private void OnServerUnitSpawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) { return; }
        units.Add(unit);
    }

    private void OnServerUnitDespawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) { return; }
        units.Remove(unit);
    }
    private void OnAuthorizeUnitSpawned(Unit unit)
    {
        if (!hasAuthority) { return; }
        units.Add(unit);
    }

    private void OnAuthorizeUnitDespawned(Unit unit)
    {
        if (!hasAuthority) { return; }
        units.Remove(unit);
    }
    public List<Unit> GetUnits()
    {
        return units;
    }
}
