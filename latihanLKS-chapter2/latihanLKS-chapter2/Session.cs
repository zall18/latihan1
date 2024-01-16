using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latihanLKS_chapter2
{
    static class Session
    {
        public static int id_user = 0;
        public static string nama_user = null;

        public static void SessionStart(int id, string nama)
        {
           id_user = id;
            nama_user = nama;
        }
    }
}
