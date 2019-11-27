using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Managers {
    class SerializationManager {
        // serialize into a binary file stored in the disk (by the designated path)
        public static void serialize<T>(T obj, string path) {
            // create a file-stream as create mode
            FileStream fs = new FileStream(@path, FileMode.Create);

            // create a binary-formatter
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, obj);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        // de-serialize from a designated binary file
        public static T Deserialize<T>(string path) {
            T ret = default;

            // create a file-stream as open mode
            FileStream fs = new FileStream(@path, FileMode.Open);

            // create a binary-formatter
            BinaryFormatter bf = new BinaryFormatter();

            ret = (T) bf.Deserialize(fs);
            return ret;
        }
    }
}
