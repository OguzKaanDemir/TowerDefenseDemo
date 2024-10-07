namespace Scripts.Interfaces
{
    public interface IInteractable
    {
        public bool IsClicked { get; set; }
        public void Interact();
    }
}
