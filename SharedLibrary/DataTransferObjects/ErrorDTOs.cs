using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTransferObjects
{
    public class ErrorDTOs
    {
        public ErrorDTOs()
        {
            Errors = new List<string>();
        }
        public ErrorDTOs(string error, bool isShow)
        {
            Errors = new List<string>
            {
                error
            };
            IsShow = isShow;
        }
        public ErrorDTOs(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; }
    }
}
