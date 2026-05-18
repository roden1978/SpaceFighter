using System;
using System.Collections.Generic;


public sealed class GunsController : Component
{
    private readonly IGunProvider _gunProvider;
    private readonly ILaserDispatcher _laserDispatcher;
    private readonly MouseEventSystem _mouseEventSystem;
    private readonly IShooteable _shooter;
    private GunBehaviour _currentGun;
    private GunBehaviour _defaultGun;

    private readonly Dictionary<LasersTypes, GunBehaviour> _gunStorage = [];

    public GunsController(IGunProvider gunProvider, ILaserDispatcher laserDispatcher, MouseEventSystem mouseEventSystem, IShooteable shooter)
    {
        _gunProvider = gunProvider;
        _laserDispatcher = laserDispatcher;
        _mouseEventSystem = mouseEventSystem;
        _shooter = shooter;
    }

    public override void Start()
    {
        _mouseEventSystem.ClickUpdate += OnMouseClick;
    }
   
    private void OnMouseClick(object sender, MouseEventArgs e)
    {
        if (false == _shooter.TryShoot(_gunProvider.Gun.GunData.Power)) 
        {
            if(_currentGun != _defaultGun)
                SwitchWeapon(LasersTypes.GreenLaser);
            
            return;
        }

        IEnumerable<Laser> lasers = _laserDispatcher.Shoot(_gunProvider.Gun);
        foreach (Laser laser in lasers)
        {
            laser.LaserData.Destination = new(laser.Position.X, Settings.ScreenHeight / 2);
        }
    }

    private GunBehaviour GetGun(LasersTypes lasersType)
    {
        if (_gunStorage.TryGetValue(lasersType, out GunBehaviour gun))
            return gun;

        throw new ArgumentNullException($"Laser type {lasersType} is not exist!");
    }

    public GunsController AddWeapon((LasersTypes type, GunBehaviour gun) weapon)
    {
        _gunStorage.Add(weapon.type, weapon.gun);
        return this;
    }

    public void SwitchWeapon(LasersTypes weapon)
    {
        GunBehaviour newGun = GetGun(weapon);

        if(weapon != LasersTypes.GreenLaser)
            if(newGun.GunData.Priority < _currentGun.GunData.Priority) return;
        
        _currentGun.gameObject.SetActive(false);
        newGun.gameObject.SetActive(true);
        InitialaizeCurrentGun(newGun);
    }

    public void InitialaizeCurrentGun(GunBehaviour currentGun)
    {
        _gunProvider.Gun = currentGun;
        _currentGun = currentGun;
    }

    public void SetDefaultGun(GunBehaviour defaultGun)
    {
        _defaultGun = defaultGun;
    }

    public override void Destroy()
    {
        _mouseEventSystem.ClickUpdate -= OnMouseClick;
    }

    public void ResetGun()
    {
        foreach(GunBehaviour item in _gunStorage.Values)
            item.gameObject.SetActive(false);

        SwitchWeapon(LasersTypes.GreenLaser);
    }

}