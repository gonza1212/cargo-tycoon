using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    abstract class GameState
    {
        protected StatesManager sm;

        public abstract void LocateGui();

        public abstract void Update();

        public abstract void Draw();

        public abstract void DebugDraw();
    }
}
