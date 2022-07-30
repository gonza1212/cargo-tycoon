using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class StatesManager
    {
        private GameState currentState;
        public StatesManager()
        {
            StartCurrentState();
        }

        private void StartCurrentState()
        {
            /*
            if (Loader.checkSaveFile())
            {
                currentState = new MainState(this, new Enterprise("AmazOrc"));
            }
            else
            {
                currentState = new MainState(this, new Enterprise("AmazOrc"));
            } */
            currentState = new GarageState(this, new Company());
        }

        public void Update()
        {
            currentState.Update();
        }

        public void Draw()
        {
            // draw current state
            currentState.Draw();
        }
    }
}
