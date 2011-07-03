using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using DTO;

namespace BUS
{
    public class SuggestionBUS
    {
        private List<SuggestionDTO> _sugHaves;
        private List<SuggestionDTO> _sugNotHaves;

        public void LoadData(string strSuggestionHaveFile, string strSuggestionNotHaveFile )
        {
            StreamReader reader = null;
            _sugHaves = new List<SuggestionDTO>();
            _sugNotHaves = new List<SuggestionDTO>();

            try
            {
                reader = new StreamReader(strSuggestionHaveFile);
                for (int i = 0; i < 10; i++)
                {
                    reader.ReadLine();
                    SuggestionDTO sug = new SuggestionDTO();
                    sug.Up = reader.ReadLine().Trim();
                    sug.Sideway = reader.ReadLine().Trim();
                    sug.Down = reader.ReadLine().Trim();
                    _sugHaves.Add(sug);
                }
                reader.Close();
                reader.Dispose();

                reader = new StreamReader(strSuggestionNotHaveFile);
                for (int i = 0; i < 10; i++)
                {
                    reader.ReadLine();
                    SuggestionDTO sug = new SuggestionDTO();
                    sug.Up = reader.ReadLine().Trim();
                    sug.Sideway = reader.ReadLine().Trim();
                    sug.Down = reader.ReadLine().Trim();
                    _sugNotHaves.Add(sug);
                }
                reader.Close();
                reader.Dispose();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetSuggestion(int numUp, int numDown, int predict, int type)
        {
            string[] suggestion = new string[2];
            int index = 0;
            switch (type)
            {
                case 1:
                    switch (numUp)
                    {
                        case 1:
                            index = 0;
                            break;
                        case 2:
                            index = 1;
                            break;
                        case 3:
                            index = 2;
                            break;
                    }
                    break;
                case -1:
                    switch (numDown)
                    {
                        case 1:
                            index = 3;
                            break;
                        case 2:
                            index = 4;
                            break;
                        case 3:
                            index = 5;
                            break;
                    }
                    break;
                case 0:
                    if (numUp == 2)
                    {
                        index = 6;
                    }
                    else if (numDown == 2)
                    {
                        index = 7;
                    }
                    else if (numUp == 1 && numDown == 1)
                    {
                        index = 8;
                    }
                    else
                    {
                        index = 9;
                    }
                    break;
            }

            if (predict == 1)
            {
                suggestion[0] = _sugHaves[index].Up;
                suggestion[1] = _sugNotHaves[index].Up;
            }
            else if (predict == 0)
            {
                suggestion[0] = _sugHaves[index].Sideway;
                suggestion[1] = _sugNotHaves[index].Sideway;
            }
            else
            {
                suggestion[0] = _sugHaves[index].Down;
                suggestion[1] = _sugNotHaves[index].Down;
            }
            return suggestion;
        }
    }
}
