using LS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LS.Server.Core {
    class SafeSettings {

        private ReaderWriterLockSlim @lock = new ReaderWriterLockSlim();
        private Settings settings;
        private Settings Settings {
            get {
                try {
                    @lock.EnterReadLock();
                    return this.settings;
                } finally {
                    @lock.ExitReadLock();

                }
            }
            set {
                try {
                    @lock.EnterWriteLock();
                    this.settings = value;
                } finally {
                    @lock.ExitWriteLock();
                }
            }
        }

    }       
}

