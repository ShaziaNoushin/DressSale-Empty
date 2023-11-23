using Mid1273286.Models;

namespace Mid1273286.ViewModels
{
    public class GroupedDataPrimitve<T>
    {
        public string Key { get; set; } = default!;
        public T Data { get; set; } = default!;
    }
}
