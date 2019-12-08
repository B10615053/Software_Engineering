using SWE_Final_Project.Models;
using SWE_Final_Project.Views.SubForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Managers {
    // the types of simulation
    public enum SimulationType {
        NOT_SIMULATING,
        RUN_THROUGH,
        STEP_BY_STEP
    }

    // the errors about simulate-ability
    public enum SimulateabilityError {
        // no error, it's simulatable
        SIMULATEABLE,

        // no START state
        NO_START,
        // no END state
        NO_END,
        // no START & END state
        NO_START_AND_END,
        // has but cannot reach any END state
        CANNOT_REACH_END
    }

    public class SimulationManager {
        // the current simulation-type
        private static SimulationType mCurrentSimulationType = SimulationType.NOT_SIMULATING;
        public static SimulationType CurrentSimulationType { get => mCurrentSimulationType; /*set => mCurrentSimulationType = value;*/ }

        // the different types of simulate-ability errors and their corresponding error messages
        private static Dictionary<SimulateabilityError, string> mSimulateabilityErrMsgs
            = new Dictionary<SimulateabilityError, string> {
                { SimulateabilityError.SIMULATEABLE, "No error." },
                { SimulateabilityError.CANNOT_REACH_END, "The END state is NOT reachable." },
                { SimulateabilityError.NO_END, "Please create at least 1 END state." },
                { SimulateabilityError.NO_START, "Please create a START state." },
                { SimulateabilityError.NO_START_AND_END, "Please create a START state and at least 1 END state." }
            };

        /* ===================================== */

        // start the simulation
        public static void startSimulation(SimulationType simulationType) {
            // check if the script is simulate-able
            SimulateabilityError err = checkScriptIsSimulateableOrNot();

            // yes, it is
            if (err == SimulateabilityError.SIMULATEABLE) {
                mCurrentSimulationType = simulationType;
                ModelManager.removeInfoPanel();
            }
            // no, it isn't
            else {
                new AlertForm(
                    "Your machine is NOT simulate-able.",
                    mSimulateabilityErrMsgs[err]
                ).ShowDialog();
            }
        }

        // check if it's a simulate-able state machine, e.g., if it has a START and an END or not
        public static SimulateabilityError checkScriptIsSimulateableOrNot() {
            // get the current working-on script-model
            ScriptModel scriptModel = ModelManager.getScriptModelByIndex();

            // first, check the completeness of this script
            if (scriptModel.Completeness == ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL)
                return SimulateabilityError.NO_START_AND_END;
            else if (scriptModel.Completeness == ScriptModelCompleteness.HAS_START_BUT_NO_END)
                return SimulateabilityError.NO_END;
            else if (scriptModel.Completeness == ScriptModelCompleteness.HAS_END_BUT_NO_START)
                return SimulateabilityError.NO_START;

            // second, check if it can reach any END from START
            List<StateModel> stateModelList = scriptModel.getCopiedStateList();
            if (isEndReachableFromStart(stateModelList) == false)
                return SimulateabilityError.CANNOT_REACH_END;

            // it's simulate-able
            return SimulateabilityError.SIMULATEABLE;
        }

        // check if any END state is reachable from START state at a certain (current working) script
        private static bool isEndReachableFromStart(List<StateModel> stateModels) {
            // get the START state
            StateModel startStateModel = stateModels.Find(it => it.StateType == StateType.START);

            // be used to do BFS to search END state from START state
            Queue<StateModel> q = new Queue<StateModel>();
            q.Enqueue(startStateModel);

            // be used to store the visited state-models
            HashSet<StateModel> s = new HashSet<StateModel>();
            s.Add(startStateModel);

            // do BFS
            while (q.Count > 0) {
                // get the front one
                StateModel front = q.Dequeue();

                // iterate all of its outgoing links
                foreach (LinkModel link in front.getAllOutgoingLinks()) {
                    // get one destination state-model
                    StateModel dst = link.DstStateModel;

                    // if it's an END, return true
                    if (dst.StateType == StateType.END)
                        return true;
                    // else, push it into the queue
                    else {
                        if (s.Contains(dst) == false) {
                            s.Add(dst);
                            q.Enqueue(dst);
                        }
                    }
                }
            }

            // cannot reach any END state
            return false;
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
