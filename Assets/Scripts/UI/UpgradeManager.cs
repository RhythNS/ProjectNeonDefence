using Doozy.Engine.UI;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public UIView UIView { get; private set; }
    public Tower SelectedTower
    {
        get => selectedTower;
        set
        {
            selectedTower = value;
            if (value == null)
                UIView.Hide();
            else
                UIView.Show();
        }
    }

    private Tower selectedTower;

    private void Awake()
    {
        Instance = this;
        UIView = GetComponent<UIView>();
        UIView.Hide(true);
    }

    private void Update()
    {
        if (SelectedTower != null)
        {
            if (!SelectedTower)
                SelectedTower = null;
        }
    }

    public void Upgrade()
    {
        if (SelectedTower != null)
            SelectedTower.Upgrade();
    }

    public void Sell()
    {
        if (SelectedTower != null)
            World.Instance.SellTurret(selectedTower);
    }
}
