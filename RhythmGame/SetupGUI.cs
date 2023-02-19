using GXPEngine;

public partial class Setup : Game
{
    public void LoadGUI(string key)
    {
        #region Switch by key
        GameObject GUI = null;

        switch (key)
        {
            case "Menu": Menu(); 
                break;
            default:
                break;
        }
        #endregion

        #region GUI Setups
        void Menu()
        {
            GUI = new Sprite("Menu");
        }
        #endregion

        #region Assigning

        #endregion
    }
}