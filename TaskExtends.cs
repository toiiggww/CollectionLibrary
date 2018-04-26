using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEArts.Etc.CollectionLibrary
{
    public static class TaskExtends
    {
        public static async Task<T> FromResult<T>(T value)
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            source.SetResult(value);
            return await source.Task;
        }
    }
}
