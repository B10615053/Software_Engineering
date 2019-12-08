using SWE_Final_Project.Views.SubForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Managers {
    public enum SimulationType {
        NOT_SIMULATING,
        RUN_THROUGH,
        STEP_BY_STEP
    }

    public class SimulationManager {
        // the current simulation-type
        private static SimulationType mCurrentSimulationType = SimulationType.NOT_SIMULATING;
        public static SimulationType CurrentSimulationType { get => mCurrentSimulationType; /*set => mCurrentSimulationType = value;*/ }

        // start the simulation
        public static void startSimulation(SimulationType simulationType) {
            mCurrentSimulationType = simulationType;
            ModelManager.removeInfoPanel();
        }

        // judge if is simulating currently
        public static bool isSimulating()
            => mCurrentSimulationType != SimulationType.NOT_SIMULATING;

        // check if is simulating currently, if it's, show alert-dialog and return true
        public static bool checkSimulating() {
            if (isSimulating()) {
                new AlertForm("Simulating...", "This action is NOT allowed when doing the simulation.").ShowDialog();
                return true;
            }
            return false;
        }
    }
}
