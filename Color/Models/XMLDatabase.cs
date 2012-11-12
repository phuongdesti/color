using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Color.Models
{
    public class MainStructure
    {
        [XmlIgnore()]
        public Object Lock = new Object();
        public int a { get; set; }
        public string name { get; set; }
        public List<ColorInfo> oColor = new List<ColorInfo>();

        public void Save(string reason)
        {
            Database.SaveAmfiDB("", "", this, reason);
        }
        public MainStructure Load()
        {

            return Database.LoadAmfiDB("", "");
        }
        public object Clone()
        {
            MainStructure o = new MainStructure();
            o.a = this.a;
            o.name = this.name;
            return o;

        }

    }
    public class Database
    {
        private static System.Collections.Concurrent.ConcurrentDictionary<string, MainStructure> AmfiBorgDBConCurrent = new System.Collections.Concurrent.ConcurrentDictionary<string, MainStructure>();
        public MainStructure CopyTenderDB(string portalid)
        {
            MainStructure tenderdb;
            MainStructure tenderdbclone;
            AmfiBorgDBConCurrent.TryGetValue(portalid, out tenderdb);
            if (tenderdb == null) return null;
            lock (tenderdb.Lock) { tenderdbclone = (MainStructure)tenderdb.Clone(); }
            tenderdb = null;
            return tenderdbclone;

        }

        public static bool UpdateAmfiDB(string portalid, MainStructure tenderdb)
        {
            MainStructure tenderdbclone;
            tenderdbclone = (MainStructure)tenderdb.Clone();
            AmfiBorgDBConCurrent.AddOrUpdate(portalid, tenderdbclone, (key, oldValue) => tenderdbclone);
            return true;
        }

        public bool ClearAmfiDB(string portalid)
        {
            MainStructure oldtenderdb;
            AmfiBorgDBConCurrent.TryRemove(portalid, out oldtenderdb);
            return true;
        }



        public static MainStructure LoadAmfiDB(string partnerid, string portalid)
        {
            MainStructure tenderdb = null;
            string folder = Current.DataFolder();
            string file = Utility.GetLatestFile(folder, "Color*.dndata*");
            if (string.IsNullOrEmpty(file)) return null;
            tenderdb = (MainStructure)Utility.LoadFile(file, typeof(MainStructure));

            return tenderdb;


            //if (tenderdb != null) UpdateTenderDB(portalid, tenderdb);
        }

        public static void SaveAmfiDB(string partnerid, string portalid, MainStructure tenderdb, string reason)
        {
            string folder =  Current.DataFolder();
            string file = folder + "Color" + System.DateTime.Now.ToString(Current.TimeStamp) + "_" + System.Text.RegularExpressions.Regex.Replace(reason, FileRegex, string.Empty) + ".dndata.gz";
            string sXML = Utility.Serialize(tenderdb, true);
            byte[] bXML = Utility.GZIP(ref sXML);
            System.IO.File.WriteAllBytes(file, bXML);
            UpdateAmfiDB(portalid, tenderdb);
        }
        private const string FileRegex = "[^A-Za-z0-9\\- æøåÆØÅ']";
    }
}