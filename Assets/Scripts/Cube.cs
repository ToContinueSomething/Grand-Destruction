using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private bool _isDestroyEntity;
    
    private bool _detouched;

    public bool IsDestroyEntity => _isDestroyEntity;

    public int Id { get; set; }
    public bool Detouched => _detouched;

    [ContextMenu("Detouch cube")]
    public void Detouch()
    {
        if (_detouched)
            return;

        _detouched = true;
        GetComponentInParent<Entity>().DetouchCube(this);
        GetComponent<ColorCube>().ApplyDetouchColor();
    }

    public void Destroy()
    {
        Detouch();

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.DOScale(0, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}