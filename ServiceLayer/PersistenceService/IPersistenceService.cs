using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.PersistenceService
{
    public interface IPersistenceService
    {
        void Export(string filename);
        void Import(string filename);
    }
}
