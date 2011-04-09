using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MoodSwingCoreComponents;

namespace MoodSwingGUI
{
    public class MSTabbedPanel : MSGUIUnclickable
    {
        private MSRadioButtonGroup tabActivatorGroup;
        public MSRadioButtonGroup TabActivatorGroup { get { return tabActivatorGroup; } }
        
        private List<MSTab> tabs;

        public MSTab ActiveTab { get; set; }

        private MSPanel container;

        public MSTabbedPanel(MSPanel container)
            : base(container.BoundingRectangle, container.SpriteBatch, container.Game)
        {
            this.container = container;
            tabs = new List<MSTab>();
            tabActivatorGroup = new MSRadioButtonGroup(container);
        }

        public void AddTab(MSTab tab)
        {
            tabActivatorGroup.AddRadioButton(tab);
            tabs.Add(tab);
            tab.PanelGroup = this;
            if (tab.IsTicked)
            {
                if (ActiveTab != null)
                {
                    ActiveTab.TabPanel.Visible = false;
                }
                ActiveTab = tab;
                tab.TabPanel.Visible = true;
            }
            else
            {
                tab.TabPanel.Visible = false;
            }
        }
    }

    public class MSTab : MSRadioButton
    {
        public MSTabbedPanel PanelGroup { get; set; }
        private MSPanel tabPanel;
        public MSPanel TabPanel { get { return tabPanel; } }

        public MSTab(MSButton unticked, MSButton ticked, bool isTicked, MSPanel tabPanel)
            : base(unticked, ticked, isTicked)
        {
            this.tabPanel = tabPanel;
        }

        public override void UnLeftClick()
        {
            if (IsTicked)
                current.UnLeftClickNoAction();
            else
            {
                base.UnLeftClick();
                PanelGroup.ActiveTab.TabPanel.Visible = false;
                PanelGroup.ActiveTab = this;
                TabPanel.Visible = true;
            }
        }
    }
}
