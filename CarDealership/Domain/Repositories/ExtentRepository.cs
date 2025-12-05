using System.Text.Json;

namespace CarDealership.Repositories
{
    public class ExtentRepository<T> where T : class
    {
        private List<T> _extent = [];
        private string _filePath;

        public ExtentRepository(string filePath)
        {
            _filePath = filePath;
            EnsureDirectoryExists();
        }

        public ExtentRepository()
        {
            _filePath = Path.Combine("extents", $"{typeof(T).Name}.json");
            EnsureDirectoryExists();
        }

        private void EnsureDirectoryExists()
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public IReadOnlyList<T> Collection => _extent.AsReadOnly();

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), $"{typeof(T).Name} cannot be null");
            }
            _extent.Add(item);
        }

        public void Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), $"{typeof(T).Name} cannot be null");
            }
            _extent.Remove(item);
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
