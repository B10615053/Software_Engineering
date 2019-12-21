using SWE_Final_Project.Models;
using SWE_Final_Project.Views;
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

    // the statuses of links during a simulation
    public enum SimulatingLinkStatus {
        // already visited
        VISITED,
        // the 1-step reachable outgoing links
        AVAILABLE,
        // not visited 2- or more-step reachable outgoing links from the current state
        VISITABLE,
        // unreachable from the current state
        UNVISITABLE
    }

    public class SimulationManager {
        // the current simulation-type
        private static SimulationType mCurrentSimulationType = SimulationType.NOT_SIMULATING;
        public static SimulationType CurrentSimulationType { get => mCurrentSimulationType; /*set => mCurrentSimulationType = value;*/ }

        // the state-model list in the current working-on machine (script)
        private static List<StateModel> mAllStateModelList = null;

        // the link-model list in the current working-on machine (script)
        private static List<LinkModel> mAllLinkModelList = null;

        // the state-model stack used to store the simulation route
        private static Stack<StateModel> mRouteStatesStack = new Stack<StateModel>();

        // the state-views' statuses at the current moment
        private static Dictionary<SimulatingStateStatus, List<StateModel>>
            mCurrentStateViewsStatuses = null;

        // the colors of state-views when doing simulation
        private static Dictionary<SimulatingStateStatus, Color> mSimulatingStateColors
            = new Dictionary<SimulatingStateStatus, Color> {
                { SimulatingStateStatus.CURRENT, Color.Red },
                { SimulatingStateStatus.VISITED, Color.Orange },
                { SimulatingStateStatus.VISITABLE, Color.Black },
                { SimulatingStateStatus.UNVISITABLE, Color.LightGray }
            };

        // the link-views' statuses at the current moment
        private static Dictionary<SimulatingLinkStatus, List<LinkModel>>
            mCurrentLinkViewStatuses = null;

        // the colors of state-views when doing simulation
        private static Dictionary<SimulatingLinkStatus, Color> mSimulatingLinkColors
            = new Dictionary<SimulatingLinkStatus, Color> {
                { SimulatingLinkStatus.VISITED, Color.Orange },
                { SimulatingLinkStatus.AVAILABLE, Color.Red },
                { SimulatingLinkStatus.VISITABLE, Color.Black },
                { SimulatingLinkStatus.UNVISITABLE, Color.LightGray }
            };

        // the different types of simulate-ability errors and their corresponding error messages
        private static Dictionary<SimulateabilityError, string> mSimulateabilityErrMsgs
            = new Dictionary<SimulateabilityError, string> {
                { SimulateabilityError.SIMULATEABLE, "No error." },
                { SimulateabilityError.END_NOT_REACHABLE, "The END state is NOT reachable." },
                { SimulateabilityError.NO_END, "There\'s NO any END state on the script." },
                { SimulateabilityError.NO_START, "Please create a START state." },
                { SimulateabilityError.NO_START_AND_END, "Please create a START state and at least 1 END state." }
            };

        /* ===================================== */

        // start the simulation
        public static void startSimulation(SimulationType simulationType) {
            // local function: really start the simulation
            void reallyStartSimulation() {
                // clear the route
                mRouteStatesStack.Clear();

                // set the simulation type
                mCurrentSimulationType = simulationType;

                // remove the info-panel if exists
                ModelManager.removeInfoPanel();

                // start the simulation by classifying and re-rendering all the states
                StateModel startStateModel = mAllStateModelList.Find(it => it.StateType == StateType.START);
                stepOnNextState(null, startStateModel);
            }

            if (isSimulating())
                return;

            // save the script and get the state-model list
            try {
                // if there're some unsaved changes, help users save them
                if (ModelManager.getScriptModelByIndex().HaveUnsavedChanges)
                    Program.form.saveCertainScript();

                // get the state-model list
                mAllStateModelList = ModelManager.getScriptModelByIndex().getCopiedStateList();

                // get the link-model list
                mAllLinkModelList = new List<LinkModel>();
                mAllStateModelList.ForEach(stateModel => {
                    mAllLinkModelList.AddRange(stateModel.getConnectedLinks());
                });

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

        // stop the simulation
        public static void stopSimulation() {
            if (isSimulating() == false)
                return;

            // turn the simulation system into not-simulating status
            mCurrentSimulationType = SimulationType.NOT_SIMULATING;

            // clear state- and link- models stored in the simulation system
            mAllStateModelList.Clear(); mAllStateModelList = null;
            mAllLinkModelList.Clear(); mAllLinkModelList = null;

            // clear state- and link- views' statuses
            mCurrentStateViewsStatuses.Clear(); mCurrentStateViewsStatuses = null;
            mCurrentLinkViewStatuses.Clear(); mCurrentLinkViewStatuses = null;

            // clear the route
            mRouteStatesStack.Clear();

            // re-render
            Program.form.invalidateCanvasAtCurrentScript();
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
                foreach (LinkModel link in front.getConnectedLinks()) {
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

        // get the color of state-views when doing simulation
        public static Color getSimulatingStateColor(SimulatingStateStatus simulatingStateStatus)
            => mSimulatingStateColors[simulatingStateStatus];

        // get the color of link-views when doing simulation
        public static Color getSimulatingLinkColor(SimulatingLinkStatus simulatingLinkStatus)
            => mSimulatingLinkColors[simulatingLinkStatus];

        // step on the next state during the simulation
        public static void stepOnNextState(LinkModel walkedLinkModel, StateModel nextStateModel) {
            // push the next-state-model into the route stack
            mRouteStatesStack.Push(nextStateModel);

            // classify all states' statuses
            classifyAllStatesStatusesWhenOnNextStep(walkedLinkModel, nextStateModel);

            // re-color all the views
            recolorViews();
        }

        // back to a certain step
        public static void backToCertainStateByIdx(int routeIndex) {
            if (routeIndex >= mRouteStatesStack.Count - 1 || routeIndex < 0)
                return;

            while (mRouteStatesStack.Count > routeIndex + 1) {
                StateModel dst = mRouteStatesStack.Pop();
                StateModel src = mRouteStatesStack.Peek();
                LinkModel link = src.getConnectedLinks().Find(it => it.DstStateModel.Id == dst.Id);

                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Clear();
                mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED].Remove(dst);
                mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITED].Remove(link);

                classifyAllStatesStatusesWhenOnNextStep(null, src);
            }

            // re-color all the views
            recolorViews();
        }

        // back to a certain step
        public static void backToCerainState(StateModel stateModel) {
            if (mRouteStatesStack == null ||
                    stateModel == null ||
                    mRouteStatesStack.Contains(stateModel) == false)
                return;

            while (mRouteStatesStack.Peek().Equals(stateModel) == false) {
                StateModel dst = mRouteStatesStack.Pop();
                StateModel src = mRouteStatesStack.Peek();
                LinkModel link = src.getConnectedLinks().Find(it => it.DstStateModel.Id == dst.Id);

                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Clear();
                mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED].Remove(dst);
                mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITED].Remove(link);

                classifyAllStatesStatusesWhenOnNextStep(null, src);
            }

            // re-color all the views
            recolorViews();
        }

        // step on the next state during the simulation,
        // classify all states into CURRENT, VISITED, VISITABLE, and UNVISITABLE
        private static void classifyAllStatesStatusesWhenOnNextStep(LinkModel walkedLinkModel, StateModel walkedOverStateModel) {
            #region the initialization of every step at the first
            // the first step during the simulation (START state)
            if (mCurrentStateViewsStatuses == null) {
                mCurrentStateViewsStatuses = new Dictionary<SimulatingStateStatus, List<StateModel>>();

                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.CURRENT, new List<StateModel>());
                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.VISITED, new List<StateModel>());
                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.VISITABLE, new List<StateModel>());
                mCurrentStateViewsStatuses.Add(SimulatingStateStatus.UNVISITABLE, new List<StateModel>());

                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Add(walkedOverStateModel);
            }
            // the subsequent simulation after being at the START state
            else {
                if (mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Count > 0) {
                    mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED].Add(
                        mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].First()
                    );
                }

                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Clear();
                mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].Add(walkedOverStateModel);

                mCurrentStateViewsStatuses[SimulatingStateStatus.VISITABLE].Clear();
                mCurrentStateViewsStatuses[SimulatingStateStatus.UNVISITABLE].Clear();
            }

            // the first step during the simulation (START state)
            if (mCurrentLinkViewStatuses == null) {
                mCurrentLinkViewStatuses = new Dictionary<SimulatingLinkStatus, List<LinkModel>>();

                mCurrentLinkViewStatuses.Add(SimulatingLinkStatus.VISITED, new List<LinkModel>());
                mCurrentLinkViewStatuses.Add(SimulatingLinkStatus.AVAILABLE, new List<LinkModel>());
                mCurrentLinkViewStatuses.Add(SimulatingLinkStatus.VISITABLE, new List<LinkModel>());
                mCurrentLinkViewStatuses.Add(SimulatingLinkStatus.UNVISITABLE, new List<LinkModel>());
            }
            // the subsequent simulation after being at the START state
            else {
                if (!(walkedLinkModel is null))
                    mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITED].Add(walkedLinkModel);

                mCurrentLinkViewStatuses[SimulatingLinkStatus.AVAILABLE].Clear();
                mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITABLE].Clear();
                mCurrentLinkViewStatuses[SimulatingLinkStatus.UNVISITABLE].Clear();
            }

            // simply add all of outgoing links of the current walked-over state-model
            mCurrentLinkViewStatuses[SimulatingLinkStatus.AVAILABLE].AddRange(
                walkedOverStateModel.getConnectedLinks()
            );
            #endregion

            // be used to do BFS to search END state from START state
            Queue<StateModel> q = new Queue<StateModel>();
            q.Enqueue(walkedOverStateModel);

            // be used to store the visited state-models
            HashSet<StateModel> s = new HashSet<StateModel>();
            s.Add(walkedOverStateModel);

            // be used to store the visited link-models
            HashSet<LinkModel> visitedLinks = new HashSet<LinkModel>();

            // do BFS
            while (q.Count > 0) {
                // get the front one
                StateModel front = q.Dequeue();

                // iterate all of its outgoing links
                foreach (LinkModel link in front.getConnectedLinks()) {
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

            // a state is unvisit-able if and only if
            // it is NOT in the 's' set after the BFS algorithm neither in visited
            mAllStateModelList.ForEach(it => {
                if (s.Contains(it) == false && !mCurrentStateViewsStatuses[SimulatingStateStatus.VISITED].Contains(it))
                    mCurrentStateViewsStatuses[SimulatingStateStatus.UNVISITABLE].Add(it);
            });

            // decide the rest links are visit-able or un-visit-able
            mAllLinkModelList.ForEach(it => {
                if (visitedLinks.Contains(it)
                        && !mCurrentLinkViewStatuses[SimulatingLinkStatus.AVAILABLE].Contains(it)
                        && !mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITED].Contains(it))
                    mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITABLE].Add(it);
                if (visitedLinks.Contains(it) == false && !mCurrentLinkViewStatuses[SimulatingLinkStatus.VISITED].Contains(it))
                    mCurrentLinkViewStatuses[SimulatingLinkStatus.UNVISITABLE].Add(it);
            });
        }

        public static int getRouteLength() => mRouteStatesStack.Count;

        public static bool isCertainStateInRoute(StateModel stateModel) {
            if (isSimulating() == false ||
                    mAllStateModelList == null ||
                    mAllStateModelList.Count == 0 ||
                    mCurrentStateViewsStatuses == null ||
                    mRouteStatesStack == null)
                return false;
            return mRouteStatesStack.Contains(stateModel);
        }

        public static bool isCertainLinkAvailableCurrently(LinkModel linkModel) {
            if (isSimulating() == false ||
                    mAllLinkModelList == null ||
                    mAllLinkModelList.Count == 0 ||
                    mCurrentLinkViewStatuses == null ||
                    mCurrentLinkViewStatuses[SimulatingLinkStatus.AVAILABLE] == null ||
                    mCurrentLinkViewStatuses[SimulatingLinkStatus.AVAILABLE].Count == 0)
                return false;
            return mCurrentLinkViewStatuses[SimulatingLinkStatus.AVAILABLE].Exists(it => it.Id == linkModel.Id);
        }

        private static void recolorViews() {
            // re-color the views
            foreach (var stateStatusPair in mCurrentStateViewsStatuses) {
                stateStatusPair.Value.ForEach(stateModel => {
                    StateView stateView = Program.form.getCertainInstanceStateViewById(stateModel.Id);
                    if (!(stateView is null))
                        stateView.CurrentSimulatingStatus = stateStatusPair.Key;
                });
            }
            // re-color the START view
            StateModel startStateModel = mCurrentStateViewsStatuses[SimulatingStateStatus.CURRENT].First();
            StateView startStateView = Program.form.getCertainInstanceStateViewById(startStateModel.Id);
            if (!(startStateView is null))
                startStateView.CurrentSimulatingStatus = SimulatingStateStatus.CURRENT;

            // re-color the links
            foreach (var linkStatusPair in mCurrentLinkViewStatuses) {
                linkStatusPair.Value.ForEach(linkModel => {
                    LinkView linkView = Program.form.getCertainInstanceLinkViewById(linkModel.Id);
                    linkView.CurrentSimulatingStatus = linkStatusPair.Key;
                });
            }
        }
    }
}
