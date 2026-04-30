using UnityEngine;
using UnityEngine.UI;

public class ImageToggler : MonoBehaviour {
    public Image targetImage;
    public Sprite imageOne;
    public Sprite imageTwo;
    private bool isFirstImage = true;

    public void ToggleImage() {
        isFirstImage = !isFirstImage;
        targetImage.sprite = isFirstImage ? imageOne : imageTwo;
    }
}