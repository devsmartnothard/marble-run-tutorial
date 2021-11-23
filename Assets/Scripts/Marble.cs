using UnityEngine;

public class Marble : MonoBehaviour, ICheckpointUser
{
    private MaterialPropertyBlock _materialPropertyBlock;
    private Renderer _renderer;
    private int _color = Shader.PropertyToID("_Color");

    public string Name { get; set; }

    public Color Color { get; set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void ChangeColor(Color color)
    {
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor(_color, color);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
