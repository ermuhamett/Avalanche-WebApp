namespace WebApp.Models
{
    public class Region
    {
        public int RegionId { get; set; }  // Идентификатор региона, уникальный для каждого региона
        public string RegionName { get; set; }  // Название региона (лист Excel)
        public ICollection<AvalancheData> AvalancheData { get; set; }  // Коллекция данных о лавинах для региона
        // Добавляем конструктор для инициализации коллекции                                                             
        public Region()
        {
            AvalancheData = new List<AvalancheData>();
        }
    }
}
