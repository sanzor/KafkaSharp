using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LS.Server.Core {
    class IDGenerator {
        private static IDGenerator instance;
        public static IDGenerator Instance {
            get {
                lock (@lock) {
                    if (instance == null) {
                        instance = new IDGenerator();
                    }
                    return instance;
                }
            }
        }
        private HashSet<int> uniqueIDMap = new HashSet<int>();
        private readonly Random rand = new Random();
        private static readonly object @lock = new object();
        private int MAX_VALUE = 100_000;
        private const int MAX_ATTEMPTS = 3;
        public bool ReleaseID(string id) {
            lock (@lock) {
                 if(int.TryParse(id,out int result)) {
                    uniqueIDMap.Remove(result);
                    return true;
                }
                return false;
            }
        }
        public string GetUniqueConsumerGroupID() {
            //-- thread unsafe
           
            //--
            if (uniqueIDMap.Count > MAX_VALUE) {
                throw new NotSupportedException($"Consumer Group Count  Reached");
            }
            lock (@lock) {
                int attemptsToGenerateUniqueID = 0;
                while (true) {
                    if (attemptsToGenerateUniqueID++ > MAX_ATTEMPTS) {
                        return string.Empty;
                    }
                    int id = rand.Next(0, MAX_VALUE);
                    if(uniqueIDMap.TryGetValue(id,out int actualval)) {
                        continue;
                    }
                    uniqueIDMap.Add(id);
                    return id.ToString();
                }
            }
            
        }
        
    }
}
