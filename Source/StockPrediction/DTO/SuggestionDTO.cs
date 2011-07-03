using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SuggestionDTO
    {
        private string _up;
        private string _sideway;
        private string _down;

        public string Up
        {
            get { return _up; }
            set { _up = value; }
        }
        public string Down
        {
            get { return _down; }
            set { _down = value; }
        }
        public string Sideway
        {
            get { return _sideway; }
            set { _sideway = value; }
        }

        
    }
}
