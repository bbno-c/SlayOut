using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanelView : BaseView<IWeaponPanelView>, IWeaponPanelView
{
    protected override IWeaponPanelView View => this;
    
    public VerticalLayoutGroup VerticalLayoutGroup;
    public GameObject WeaponPanel;
    
    private Dictionary<WeaponInfo, GameObject> _weaponPanels;
    
    public void InitPanel(WeaponInfo weapon)
    {
        
    }
}
