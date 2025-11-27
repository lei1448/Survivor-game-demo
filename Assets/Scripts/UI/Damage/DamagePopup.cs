using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TMP_Text _textMesh;
    private float _disappearTimer;
    private Color _textColor;

    [SerializeField] private float DISAPPEAR_TIME_MAX = 1f;
    [SerializeField] private float MOVE_SPEED = 3f;

    public ObjectPool PoolToReturnTo
    {
        get; set;
    }


    private void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();
    }

    // 修改Setup方法，在重新启用时重置状态
    public void Setup(int damageAmount)
    {
        _textMesh.SetText(damageAmount.ToString());
        _disappearTimer = DISAPPEAR_TIME_MAX;
        _textColor = _textMesh.color;
        _textColor.a = 1;
        _textMesh.color = _textColor;
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        transform.position += Vector3.up * MOVE_SPEED * Time.deltaTime;

        _disappearTimer -= Time.deltaTime;
        if(_disappearTimer < 0)
        {
            // 时间到了，返回对象池
            // 不再播放渐隐动画，直接返回
            if(PoolToReturnTo != null)
            {
                PoolToReturnTo.ReturnToPool(this.gameObject);
            }
            else
            {
                // 如果没有池，作为后备方案，还是销毁掉
                Destroy(gameObject);
            }
        }
        else
        {
            // 淡出效果
            _textColor.a = _disappearTimer / DISAPPEAR_TIME_MAX;
            _textMesh.color = _textColor;
        }
    }
}