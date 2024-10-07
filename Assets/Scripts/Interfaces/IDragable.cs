namespace Scripts.Interfaces
{
    public interface IDragable
    {
        public bool IsDragged { get; set; }
        public void Drag();
    }
}
