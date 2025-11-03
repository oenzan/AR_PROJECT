public interface IActions
{
    bool CanMove       { get; }
    bool CanAnimate    { get; }
    bool CanPlayMedia  { get; }
    void ToggleMove();
    void ToggleAnimation();
    void TogglePlayPause();  // video veya ses için
}