using System.Text.Json;

namespace CarDealership.Repositories
{
    internal class ExtentRepository<T> where T : class
    {
        private List<T> _extent = [];
        private string _filePath;

        public ExtentRepository(string filePath)
        {
            _filePath = filePath;
        }

        public ExtentRepository()
        {
            _filePath = $"extents\\{typeof(T).Name}.json";
        }

        public IReadOnlyList<T> Extent => _extent.AsReadOnly();

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), $"{typeof(T).Name} cannot be null");
            }
            _extent.Add(item);
        }

        public void SaveExtent()
        {
            var json = JsonSerializer.Serialize(_extent);
            File.WriteAllText(_filePath, json);
        }

        public bool LoadExtent()
        {
            _extent.Clear();

            if (!File.Exists(_filePath))
            {
                return false;
            }

            var json = File.ReadAllText(_filePath);

            try 
            { 
                var deserialized = JsonSerializer.Deserialize<List<T>>(json);
                if (deserialized != null)
                {
                    _extent = deserialized;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void DeleteExtent()
        {
            _extent.Clear();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}
