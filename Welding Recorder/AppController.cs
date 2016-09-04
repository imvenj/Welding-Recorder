using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Welding_Recorder
{
    public class AppController
    {
        private static AppController shared;
        private AppController() { }
        public static AppController Shared
        {
            get
            {
                if (shared == null)
                {
                    shared = new AppController();
                }
                return shared;
            }
        }

        public Template CurrentTemplate { get; set; }
    }
}
