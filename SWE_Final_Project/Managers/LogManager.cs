using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Managers {
    public enum LogType {
        SIMULATION_START_OR_END,
        DURING_SIMULATION
    }

    public class LogManager {
        // the dictionary used to store classified log texts
        private static Dictionary<LogType, List<string>> mLogTextDict = new Dictionary<LogType, List<string>> {
            { LogType.SIMULATION_START_OR_END, new List<string>() },
            { LogType.DURING_SIMULATION, new List<string>() }
        };

        // do logging w/ a single text
        public static void Log(LogType logType, string text) {
            List<string> texts = new List<string> { text };
            doLogging(logType, texts);
        }

        // do logging w/ a list of texts
        public static void Log(LogType logType, List<string> texts) {
            doLogging(logType, texts);
        }

        // really do logging
        private static void doLogging(LogType logType, List<string> texts) {
            if (texts is null || texts.Count() == 0)
                return;

            // get the logout rich-text-box
            RichTextBox rtxtBx = Program.form.getCmdLogoutBox();

            // add to the list of the corresponding type
            mLogTextDict[logType].AddRange(texts);

            // select the color determined by the log-type
            rtxtBx.SelectionColor = getLogColor(logType);

            // write all of 1the texts into the rich-text-box
            foreach (string text in texts)
                rtxtBx.AppendText(text + "\r\n");

            // let the rich-text-box automatically scroll to end
            rtxtBx.ScrollToCaret();
        }

        // select the log color differentiated by the log-type
        private static Color getLogColor(LogType logType) {
            switch (logType) {
                case LogType.SIMULATION_START_OR_END:
                    return Color.AliceBlue;
                case LogType.DURING_SIMULATION:
                    return Color.White;
            }
            return Color.White;
        }
    }
}
