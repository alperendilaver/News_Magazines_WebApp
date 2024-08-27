using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data
{
    public class GeneralResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }

    public GeneralResponse()
    {
        Errors = new List<string>();
    }
}

}