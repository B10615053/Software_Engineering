using SWE_Final_Project.Models;
using SWE_Final_Project.Views.States;
using SWE_Final_Project.Views.SubForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        END_NOT_REACHABLE
    }

    // the statuses of states during a simulation
    public enum SimulatingStateStatus {
        // current state
        CURRENT,
        // already visited
        VISITED,
        // visit-able (reachable) from the current state
        VISITABLE,
        // unvisit-able (unreachable) from the current state
        UNVISITABLE
    }

    public class SimulationManager {
        // the current simulation-type
        private static SimulationType mCurrentSimulationType = SimulationType.NOT_SIMULATING;
        public static SimulationType CurrentSimulationType { get => mCurrentSimulationType; /*set => mCurrentSimulationType = value;*/ }

        // the state-model list in the current working-on machine (script)
        private static List<StateModel> mAllStateModelList = null;

        // the current state-views' statuses
        private static Dictionary<SimulatingStateStatus, List<StateModel>>
            mCurrentStateViewsStatuses = null;

        // the different types of simulate-ability errors and their corresponding error messages
        private static Dictionary<SimulateabilityError, string> mSimulateabilityErrMsgs
            = new Dictionary<SimulateabilityError, string> {
                { SimulateabilityError.SIMULATEABLE, "No error." },
                { SimulateabilityError.END_NOT_REACHABLE, "The END state is NOT reachable." },
                { SimulateabilityError.NO_END, "There\'s NO any END state on the script." },
                { SimulateabilityError.NO_START, "Please create a START state." },
                { SimulateabilityError.NO_START_AND_END, "Please create a START state and at least 1 END state." }
            };

        // the colors of state-views when doing simulation
        private static Dictionary<SimulatingStateStatus, Color> mSimulatingStateColors
            = new Dictionary<SimulatingStateStatus, Color> {
                { SimulatingStateStatus.CURRENT, Color.Red },
                { SimulatingStateStatus.VISITED, Color.Orange },
                { SimulatingStateStatus.VISITABLE, Color.Black },
                { SimulatingStateStatus.UNVISITABLE, Color.LightGray }
            };

        /* ===================================== */

        // start the simulation
        public static void startSimulation(SimulationType simulationType) {
            // local function: really start the simulation
            void reallyStartSimulation() {
                // set the simulation type
                mCurrentSimulationType = simulationType;

                // remove the info-panel if exists
                ModelManager.removeInfoPanel();

                // start the simulation by classifying and re-rendering all the states
                StateModel startStateModel = mAllStateModelList.Find(it => it.StateType == StateType.START);
                stepOnNextState(startStateModel);
            }

            // save the script and get the state-model list
            try {
                // if there're some unsaved changes, help users save them
                if (ModelManager.getScriptModelByIndex().HaveUnsavedChanges)
                    Program.form.saveCertainScript();

                // get the state-model list
                mAllStateModelList = ModelManager.getScriptModelByIndex().getCopiedStateList();
                if (mAllStateModelList == null) throw new Exception();
            } catch (Exception) {
                new AlertForm("Alert", "Something is wrong, I can feel it.").ShowDialog();
                return;
            }

            // check if the script is simulate-able
            SimulateabilityError err = checkScriptIsSimulateableOrNot();

            // yes, it is simulate-able
            if (err == SimulateabilityError.SIMULATEABLE)
                reallyStartSimulation();
            // no, it is not simulate-able
            else {
                // err = no END or the END state is not reachable
                if (err == SimulateabilityError.END_NOT_REACHABLE ||
                        err == SimulateabilityError.NO_END) {
                    // let users to decide if they want to boost up the simulation anyways or not
                    DialogResult dialogResult = new AlertForm(
                        mSimulateabilityErrMsgs[err],
                        "Do you still want to start the simulation?",
                        false,
                        true,
                        true
                    ).ShowDialog();

                    // yes, they still want to
                    if (dialogResult == DialogResult.Yes)
                        reallyStartSimulation();
                }
                // err = no START or no START & END
                else {
                    new AlertForm(
                        "Your machine is NOT simulate-able.",
                        mSimulateabilityErrMsgs[err]
                    ).ShowDialog();
                }
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
            if (isEndReachableFromStart(mAllStateModelList) == false)
                return SimulateabilityError.END_NOT_REACHABLE;

            // it's simulate-able
            return SimulateabilityError.SIMULATEABLE;
        }

        // check if any END state is reachable from START state at a certain (current working) script
        private static bool isEndReachableFromStart(List<StateModel> stateModels) {
            if (stateModels == null)
                return false;

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
                new AlertForm(
                    "Simulating...",
                    "This action is NOT allowed when the simulation is running.\r\n\r\n" +
                        "Please cancel the simulation before doing this action."
                ).ShowDialog();

                return true;
            }
            return false;
        }

        // get the state-model that is currently staying during the simulation
        public static StateModel getCurrentStayingStateModel() {
            if (mCurrentStateViewsStatuses is null ||
                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT] is null ||
                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Count == 0)
                return null;
            return mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].First();
        }

        // get the color of state-view when doing simulation
        public static Color getSimulatingStateColor(SimulatingStateStatus simulatingStateStatus)
            => mSimulatingStateColors[simulatingStateStatus];

        // step on the next state during the simulation
        public static void stepOnNextState(StateModel nextStateModel) {
            // classify all states' statuses
            classifyAllStatesStatusesWhenOnNextStep(nextStateModel);

            // re-render the views
            foreach (var stateStatusPair in mCurrentStateViewsStatuses) {
                stateStatusPair.Value.ForEach(stateModel => {
                    StateView stateView = Program.form.getCertainInstanceStateViewById(stateModel.Id);
                    stateView.CurrentSimulatingStatus = stateStatusPair.Key;
                });
            }
            // re-render the START view
            StateModel startStateModel = mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].First();
            StateView startStateView = Program.form.getCertainInstanceStateViewById(startStateModel.Id);
            startStateView.CurrentSimulatingStatus = SimulatingStateStatus.CURRENT;

        }

        // step on the next state during the simulation,
        // classify all states into CURRENT, VISITED, VISITABLE, and UNVISITABLE
        private static void classifyAllStatesStatusesWhenOnNextStep(StateModel currentSrc) {
            // the first step during the simulation (START state)
            if (mCurrentStateViewsStatuses == null) {
                mCurrentStateViewsStatuses = new Dictionary<SimulatingStateStatus, List<StateModel>>();

                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.CURRENT, new List<StateModel>());
                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.VISITED, new List<StateModel>());
                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.VISITABLE, new List<StateModel>());
                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.UNVISITABLE, new List<StateModel>());

                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Add(currentSrc);
            }
            // the subsequent simulation after being at the START state
            else {
                mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED].Add(
                    mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].First()
                );

                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Clear();
                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Add(currentSrc);

                mCurrentStateViewsStatuses[SimulatingStateStatus.VISITABLE].Clear();
                mCurrentStateViewsStatuses[SimulatingStateStatus.UNVISITABLE].Clear();
            }

            // be used to do BFS to search END state from START state
            Queue<StateModel> q = new Queue<StateModel>();
            q.Enqueue(currentSrc);

            // be used to store the visited state-models
            HashSet<StateModel> s = new HashSet<StateModel>();
            s.Add(currentSrc);

            // be used to store the visited link-models
            HashSet<LinkModel> visitedLinks = new HashSet<LinkModel>();

            // do BFS
            while (q.Count > 0) {
                // get the front one
                StateModel front = q.Dequeue();

                // iterate all of its outgoing links
                foreach (LinkModel link in front.getAllOutgoingLinks()) {
                    // get one destination state-model
                    StateModel dst = link.DstStateModel;

                    // add this link into visisted-links set
                    visitedLinks.Add(link);

                    // no visited in this BFS algorithm
                    if (s.Contains(dst) == false) {
                        s.Add(dst);
                        q.Enqueue(dst);

                        // if the dst is not in CURRENT neither in VISITED,
                        // then it shall be in VISITABLE
                        if (!mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT]
                                .Exists(sm => sm.Id == dst.Id) &&
                            !mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED]
                                .Exists(sm => sm.Id == dst.Id))
                            mCurrentStateViewsStatuses[SimulatingStateStatus.VISITABLE].Add(dst);
                    }
                }
            }

            // A state is unvisit-able if and only if it is NOT in the 's' set after the BFS algorithm
            mAllStateModelList.ForEach(it => {
                if (s.Contains(it) == false && !mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED].Contains(it))
                    mCurrentStateViewsStatuses[SimulatingStateStatus.UNVISITABLE].Add(it);
            });
        }
    }
}
