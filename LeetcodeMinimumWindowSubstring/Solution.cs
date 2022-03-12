using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetcodeMinimumWindowSubstring
{
    public class Solution
    {
        struct WAYPOINT
        {
            public WAYPOINT(int startIndex, IDictionary<char,int> source)
            {
                this.StartIndex = startIndex;
                this.CharCount = new Dictionary<char, int>(source);
                this.MatchedChars = 0;
            }


            public int StartIndex;
            public Dictionary<char, int> CharCount;
            public int MatchedChars { get; set; }

        }


        public string MinWindow(string s, string t)
        {
            if (t.Length > s.Length)
                return "";

            HashSet<char> presence = new();
            Dictionary<char, int> charCount = new();

            for (int i = 0; i < t.Length; i++)
            {
                var ch = t[i];
                presence.Add(ch);

                charCount[ch]  = charCount.ContainsKey(ch) ? charCount[ch]+1 : 1;
            }

            bool haveAtLeastOneMatch = false;
            int shortestDistance = int.MaxValue;
            int shortestStringStartIndex = 0;
            int shortestStringLastIndex = 0;

            List<WAYPOINT> waypoints= new();

            for (int i = 0; i < s.Length; i++)
            {
                var ch = s[i];
                if (presence.Contains(ch))
                {
                    waypoints.Add(new WAYPOINT(i, charCount));

                    for (int j = 0; j<waypoints.Count; j++)
                    {
                        bool remove = false;
                        var waypoint = waypoints[j];
                        if (waypoint.CharCount[ch] > 0)
                        {
                            waypoint.CharCount[ch] -= 1;
                            waypoint.MatchedChars+=1;
                            if (waypoint.MatchedChars == t.Length)
                            {
                                haveAtLeastOneMatch = true;

                                var distance = i - waypoint.StartIndex;
                                if (distance < shortestDistance)
                                {
                                    shortestDistance = distance;
                                    shortestStringStartIndex = waypoint.StartIndex;
                                    shortestStringLastIndex = i;
                                }

                                remove = true;
                            }

                            if (!remove)
                                waypoints[j] = waypoint;
                            else
                            {
                                waypoints.RemoveAt(j);
                            }
                        }
                    }

                    
                }
            }

            if (haveAtLeastOneMatch)
                return s.Substring(shortestStringStartIndex, (shortestStringLastIndex - shortestStringStartIndex)+1);

            return "";
        }
    }
}
