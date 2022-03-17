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
        hireEmployeeMenu.SetActive(true);
    }

    public void OpenChangeTrackMenu()
    {
        changeTrackMenu.SetActive(true);
    }

    public void OpenAssetStore()
    {
        assetStore.SetActive(true);
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
