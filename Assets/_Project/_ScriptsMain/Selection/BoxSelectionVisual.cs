using _Project._ScriptsMain.Extensions;
using UnityEngine;

public class BoxSelectionVisual : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private PlayingInput _playingInput;
    private Rasa _rasa;
    private Selector _selector;
    private Vector3 _startPosition;
    private Camera _camera;

    public void Init(PlayingInput playingInput,Camera camera)
    {
        _camera = camera;
        _renderer.enabled = false;

        _playingInput = playingInput;
        playingInput.Mouse.LeftButton.canceled += context =>  MouseLeftButtonUp();
        playingInput.Mouse.LeftButton.started += context =>  MouseLeftButtonDown();
        playingInput.Mouse.Position.performed += context =>  ChengMousePosition();
    }

    private void ChengMousePosition()
    {
        if (!_renderer.enabled)
        {
            return;
        }

        Vector3 mouseToWorldPosition = GetMousePosition().ToWorldPosition(_camera);
        MyMethods.BoxStretching(_startPosition,mouseToWorldPosition,out Vector3 center,out Vector3 scale);
        transform.position = center;
        transform.localScale = scale;
    }

    private void MouseLeftButtonUp()
    {
        _renderer.enabled = false;
        transform.localScale = Vector3.zero;
    }

    private void MouseLeftButtonDown()
    {
        _renderer.enabled = true;
        Vector2 mousePosition = GetMousePosition();
        _startPosition = mousePosition.ToWorldPosition(_camera);
    }

    private Vector2 GetMousePosition()
    {
        return _playingInput.Mouse.Position.ReadValue<Vector2>();
    }
}