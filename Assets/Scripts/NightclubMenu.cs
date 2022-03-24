using JetBrains.Annotations;
using UnityEngine;

public class NightclubMenu : MonoBehaviour
{
    public GameObject hireEmployeeMenu;
    public GameObject changeTrackMenu;
    public GameObject assetStore;
    
    [Header("Submenus")]
    public GameObject inventoryMenu;
    public GameObject audioMenu;
    public GameObject assetsMenu;
    public GameObject staffMenu;
    [CanBeNull] private GameObject _currentOpenMenu;
    
    public void OpenInventoryMenu() => _openMenu(inventoryMenu);
    public void OpenAudioMenu() => _openMenu(audioMenu);
    public void OpenEditAssets() => _openMenu(assetsMenu);
    public void OpenStaffMenu() => _openMenu(staffMenu);

    public void OpenHireEmployeeMenu()
    {
        _closeAllMenus();
        hireEmployeeMenu.SetActive(true);
    }

    public void OpenChangeTrackMenu()
    {
        _closeAllMenus();
        changeTrackMenu.SetActive(true);
    }

    public void OpenAssetStore()
    {
        _closeAllMenus();
        assetStore.SetActive(true);
    }

    private void _closeAllMenus()
    {
        hireEmployeeMenu.SetActive(false);
        changeTrackMenu.SetActive(false);
        assetStore.SetActive(false);
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
