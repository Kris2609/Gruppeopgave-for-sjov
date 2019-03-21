using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public class Button : Component
    {
        private EventHandler onClickEventHandler;

        /// <summary>
        /// Creates a new button
        /// </summary>
        /// <param name="ev">The method to check on click</param>
        public Button(EventHandler ev)
        {
            onClickEventHandler = ev;
        }

        /// <summary>
        /// Setup the EventHandler for OnClickEvent on GameManager instance
        /// </summary>
        public void InitializeOnClickEvent()
        {
            GameManager.ManagerInstance.OnClickEvent += onClickEventHandler;
        }
        public override void Initialize()
        {
            InitializeOnClickEvent();
        }

        /// <summary>
        /// Removes the EventHandler for OnClickEvent on GameManager instance
        /// </summary>
        public void RemoveOnClickEvent()
        {
            GameManager.ManagerInstance.OnClickEvent -= onClickEventHandler;
        }
    }
}
