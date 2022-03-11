using JetBrains.Annotations;
using UnityEngine;

public class NightclubMenu : MonoBehaviour
{
    public GameObject hireEmployeeMenu;
    
    [Header("Submenus")]
    public GameObject inventoryMenu;
    public GameObject cleaningMenu;
    public GameObject assetsMenu;
    public GameObject staffMenu;
    [CanBeNull] private GameObject _currentOpenMenu;
    
    public void OpenInventoryMenu() => _openMenu(inventoryMenu);
    public void OpenCleaningMenu() => _openMenu(cleaningMenu);
    public void OpenEditAssets() => _openMenu(assetsMenu);
    public void OpenStaffMenu() => _openMenu(staffMenu);

    public void OpenHireEmployeeMenu()
    {
        hireEmployeeMenu.SetActive(true);
    }
    
    private void _openMenu(GameObject menu)
    {
        if (_currentOpenMenu != null)
        {
            _currentOpenMenu.SetActive(false);
        }
        
        if (_currentOpenMenu == menu)
        {
            _currentOpenMenu = null;
            return;
        }
        
        menu.SetActive(true);
        _currentOpenMenu = menu;
    }
}
