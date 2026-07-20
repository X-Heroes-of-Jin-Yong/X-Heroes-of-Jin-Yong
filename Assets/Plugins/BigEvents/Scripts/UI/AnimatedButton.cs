using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace BigEvents.UI
{
    /// <summary>
    /// 带动画的按钮组件，基于 PrimeTween 和 OdinInspector。
    /// 提供悬停缩放/变色和点击回弹效果，零 GC 分配。
    /// </summary>
    [HideMonoScript]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        // ==================== 外观 ====================

        [FoldoutGroup("外观", expanded: true, Order = 0)]
        [ShowInInspector]
        [PreviewField(80, ObjectFieldAlignment.Left)]
        [AssetsOnly]
        [LabelText("按钮图片")]
        [HorizontalGroup("外观/图片行")]
        private Sprite ButtonSprite
        {
            get => GetComponent<Image>()?.sprite;
            set
            {
                var img = GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = value;
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        UnityEditor.EditorUtility.SetDirty(img);
                        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(img);
                    }
#endif
                }
            }
        }

        [FoldoutGroup("外观", Order = 0)]
        [ShowInInspector]
        [LabelText("按钮颜色")]
        [HorizontalGroup("外观/图片行", Width = 120)]
        private Color ButtonColor
        {
            get => GetComponent<Image>()?.color ?? Color.white;
            set
            {
                var img = GetComponent<Image>();
                if (img != null)
                {
                    img.color = value;
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        UnityEditor.EditorUtility.SetDirty(img);
                        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(img);
                    }
#endif
                }
            }
        }

        [FoldoutGroup("外观", Order = 0)]
        [ShowInInspector]
        [LabelText("按钮文本")]
        [HorizontalGroup("外观/文本行")]
        private string ButtonText
        {
            get => GetComponentInChildren<TextMeshProUGUI>()?.text ?? string.Empty;
            set
            {
                var tmp = GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null) tmp.text = value;
            }
        }

        [FoldoutGroup("外观", Order = 0)]
        [ShowInInspector]
        [PropertyRange(8, 72)]
        [LabelText("字体大小")]
        [HorizontalGroup("外观/文本行", Width = 80)]
        private float ButtonFontSize
        {
            get => GetComponentInChildren<TextMeshProUGUI>()?.fontSize ?? 24;
            set
            {
                var tmp = GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null) tmp.fontSize = value;
            }
        }

        [FoldoutGroup("外观", Order = 0)]
        [ShowInInspector]
        [LabelText("字体颜色")]
        [HorizontalGroup("外观/文本行", Width = 100)]
        private Color ButtonTextColor
        {
            get => GetComponentInChildren<TextMeshProUGUI>()?.color ?? Color.black;
            set
            {
                var tmp = GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null) tmp.color = value;
            }
        }

        // ==================== 点击动画 ====================

        [FoldoutGroup("点击动画", expanded: true, Order = 1)]
        [PropertyRange(0.7f, 1f)]
        [LabelText("缩放比例")]
        [SerializeField] private float pressScale = 0.92f;

        [FoldoutGroup("点击动画", Order = 1)]
        [SuffixLabel("秒", Overlay = true)]
        [PropertyRange(0.02f, 0.3f)]
        [LabelText("动画时长")]
        [SerializeField] private float pressDuration = 0.08f;

        [FoldoutGroup("点击动画", Order = 1)]
        [LabelText("缓动曲线")]
        [SerializeField] private Ease pressEase = Ease.OutQuad;

        // ==================== 悬停动画 ====================

        [FoldoutGroup("悬停动画", expanded: true, Order = 2)]
        [PropertyRange(1f, 1.3f)]
        [LabelText("缩放比例")]
        [SerializeField] private float hoverScale = 1.08f;

        [FoldoutGroup("悬停动画", Order = 2)]
        [SuffixLabel("秒", Overlay = true)]
        [PropertyRange(0.05f, 0.5f)]
        [LabelText("动画时长")]
        [SerializeField] private float hoverDuration = 0.15f;

        [FoldoutGroup("悬停动画", Order = 2)]
        [LabelText("高亮颜色")]
        [SerializeField] private Color hoverColor = new Color(0.95f, 0.95f, 0.95f, 1f);

        [FoldoutGroup("悬停动画", Order = 2)]
        [LabelText("缓动曲线")]
        [SerializeField] private Ease hoverEase = Ease.OutCubic;

        // ==================== 释放动画 ====================

        [FoldoutGroup("释放动画", expanded: true, Order = 3)]
        [SuffixLabel("秒", Overlay = true)]
        [PropertyRange(0.1f, 0.5f)]
        [LabelText("回弹时长")]
        [SerializeField] private float releaseDuration = 0.2f;

        [FoldoutGroup("释放动画", Order = 3)]
        [LabelText("缓动曲线")]
        [SerializeField] private Ease releaseEase = Ease.OutBack;

        [Title("调试信息")]
        [ShowInInspector, ReadOnly, FoldoutGroup("调试信息", VisibleIf = "@false")]
        private bool _isHovered;

        [ShowInInspector, ReadOnly, FoldoutGroup("调试信息", VisibleIf = "@false")]
        private bool _isPressed;

        private Image _image;
        private Button _button;
        private Color _originalColor;

        // ==================== 生命周期 ====================

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _originalColor = _image.color;
        }

        // ==================== 交互事件 ====================

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_button.interactable) return;
            _isHovered = true;
            PlayHoverAnimation();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            if (!_isPressed)
                PlayNormalAnimation();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_button.interactable) return;
            _isPressed = true;
            PlayPressAnimation();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            var targetScale = _isHovered ? hoverScale : 1f;
            var targetColor = _isHovered ? hoverColor : _originalColor;
            PlayReleaseAnimation(targetScale, targetColor);
        }

        // ==================== 动画播放 ====================

        /// <summary>停止当前的 scale 和 color 动画</summary>
        private void StopAnimation()
        {
            Tween.StopAll(transform);
            Tween.StopAll(_image);
        }

        /// <summary>悬停：放大 + 高亮颜色</summary>
        private void PlayHoverAnimation()
        {
            StopAnimation();
            Tween.Scale(transform, hoverScale, hoverDuration, hoverEase);
            Tween.Color(_image, hoverColor, hoverDuration, hoverEase);
        }

        /// <summary>按下：缩小 + 恢复原色</summary>
        private void PlayPressAnimation()
        {
            StopAnimation();
            Tween.Scale(transform, pressScale, pressDuration, pressEase);
            Tween.Color(_image, _originalColor, pressDuration, pressEase);
        }

        /// <summary>释放：回弹到目标 scale 和颜色</summary>
        private void PlayReleaseAnimation(float targetScale, Color targetColor)
        {
            StopAnimation();
            Tween.Scale(transform, targetScale, releaseDuration, releaseEase);
            Tween.Color(_image, targetColor, releaseDuration, releaseEase);
        }

        /// <summary>恢复：回到原始 scale 和颜色</summary>
        private void PlayNormalAnimation()
        {
            StopAnimation();
            Tween.Scale(transform, 1f, hoverDuration, hoverEase);
            Tween.Color(_image, _originalColor, hoverDuration, hoverEase);
        }

        private void OnDisable()
        {
            // 立即停止所有动画并重置状态
            Tween.StopAll(transform);
            transform.localScale = Vector3.one;
            if (_image != null)
                _image.color = _originalColor;
            _isPressed = false;
            _isHovered = false;
        }
    }
}
