using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEArts.Etc.CollectionLibrary
{
    public static class TaskExtends
    {
        public static Task<T> FromResult<T>(this Task<T> task, T value)
        {
            TaskCompletionSource<T> source = new TaskCompletionSource<T>();
            source.SetResult(value);
            return source.Task;
        }
    }
}
