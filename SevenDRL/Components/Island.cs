using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL.Components
{
    public class Island : Component
    {
        private bool enemy;

        /// <summary>
        /// Constructor for Island
        /// </summary>
        /// <param name="enemy">Bool value</param>
        public Island(bool enemy)
        {
            this.enemy = enemy;
            GameManager.ManagerInstance.OnClickEvent += IslandClick;
        }

        /// <summary>
        /// Method for event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IslandClick(object sender, EventArgs e)
        {
            if (enemy == true)
            {
                
            }
            else
            {

            }
        }
    }
}
