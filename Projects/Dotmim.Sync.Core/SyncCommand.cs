﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Dotmim.Sync
{
    public class SyncCommand 
    {
        public DbCommand DbCommand { get; set; }

        public bool IsPrepared { get; set; }


        public SyncCommand(DbCommand dbCommand)
        {
            DbCommand = dbCommand;
            IsPrepared = false;
        }
    }
}
