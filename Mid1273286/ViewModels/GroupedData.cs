using Mid1273286.Models;

namespace Mid1273286.ViewModels
{
    public class GroupedData
    {
        public string Key { get; set; } = default!;
        public IEnumerable<Sale> Data { get; set; } = default!;
    }
}
