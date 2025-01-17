﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GreenLight
{
    class AIController : EntityController
    {
        public List<AI> driverlist = new List<AI>();
        private static List<DriverStats> drivertypes = new List<DriverStats>();

        public override void Initialize()
        {
            Log.Write("Initializing the AIController");
        }

        public AIController()
        {

        }

        public void AddDriver(Vehicle v, DriverStats _stats = null)
        {
            if (_stats == null)
            {
                _stats = getRandomStats();
            }
        }

        private DriverStats getRandomStats()
        {
            return null;
        }

        static private void initDriverStats()
        {
            try
            {
                string file = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\GreenLight\\src\\Driver\\DriverType.json";

                using (StreamReader sr = new StreamReader(file))
                {
                    string json = sr.ReadToEnd();
                    drivertypes = JsonConvert.DeserializeObject<List<DriverStats>>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static public void addDriverStats(string _name, int _reactionTime, float _followInterval, int _speedRelativeToLimit, float _ruleBreakingChance)
        {
            DriverStats _temp = new DriverStats(_name, _reactionTime, _followInterval, _speedRelativeToLimit, _ruleBreakingChance);

            if (drivertypes.Find(x => x == _temp) == null)
            {
                drivertypes.Add(_temp);
            }

            General_Form.Main.UserInterface.SimSDM.Selection_box.Add_Element(_temp.Name);
        }


        static public DriverStats getDriverStat(string _name)
        {
            DriverStats _temp = drivertypes.Find(x => x.Name == _name);

            if (_temp == null)
            {
                try
                {
                    _temp = drivertypes[0];
                }
                catch (Exception)
                {
                    _temp = new DriverStats("", 1, 1, 1, 1);
                }
            }

            return _temp;
        }

        static public List<string> getStringDriverStats()
        {
            if (!drivertypes.Any())
            {
                initDriverStats();
            }
            Console.WriteLine(drivertypes.Count());

            List<string> _temp = new List<string>();
            drivertypes.ForEach(x => _temp.Add(x.Name));
            return _temp;
        }

        static public void SaveJson()
        {
            string json = JsonConvert.SerializeObject(drivertypes);
            Console.WriteLine(json);

            string file = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\GreenLight\\src\\Driver\\DriverType.json";

            using (StreamWriter sr = new StreamWriter(file))
            {
                sr.Write(json);
            }
        }


    }
}
