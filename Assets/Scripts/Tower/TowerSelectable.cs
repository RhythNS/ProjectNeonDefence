public class TowerSelectable : SimpleSelectable
{
    protected override void InnerSelect()
    {
        UpgradeManager.Instance.SelectedTower = GetComponent<Tower>();
    }

    protected override void InnerDeSelect()
    {
        UpgradeManager.Instance.SelectedTower = null;
    }
}
