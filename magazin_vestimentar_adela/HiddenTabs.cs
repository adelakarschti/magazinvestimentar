namespace magazin_vestimentar_adela
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class HiddenTabs : TabControl
    {
        public HiddenTabs()
        {
            // Hide the tab names
            this.Appearance = TabAppearance.FlatButtons;
            this.ItemSize = new Size(0, 1); // Hide the tabs
            this.SizeMode = TabSizeMode.Fixed;
        }

        protected override void WndProc(ref Message m)
        {
            // Prevent the tab control from adjusting the size for the tab names
            if (m.Msg == 0x1328) // TCM_ADJUSTRECT
            {
                return;
            }
            base.WndProc(ref m);
        }
    }
}